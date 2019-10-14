using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StateMachineStuff;

public class new_ai_statemachine : MonoBehaviour
{
    //create all of the state objects
    public static EnemySM StateMachine = new EnemySM();
    public static PatrollingState Patrolling = new PatrollingState();
    public static ChasingState Chasing = new ChasingState();
    public static AttackingState Attacking = new AttackingState();

    void Awake()
    {
        //Assembles the state machine by delcaring the owners and substates for each state
        StateMachine.addSubState("Patrolling", Patrolling);
        StateMachine.addSubState("Chasing", Chasing);
        StateMachine.addSubState("Attacking", Attacking);

        Patrolling.setOwner(StateMachine);
        Chasing.setOwner(StateMachine);
        Attacking.setOwner(StateMachine);

        //connects the statemachine to the enemy's position and the player's position, so it can use them to make decisions
        StateMachine.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StateMachine.self = transform;

        //this is tacky and will be gone when I implement default substates the state class
        StateMachine.CurrentSubState = Patrolling;

        Debug.Log("Started");
    }

    void Update()
    {
        //updates the state machine, this ends up calling the update function of every active state in the tree
        StateMachine.StateUpdate();

        //moves the enemy based on the state machine's velocty vector
        transform.position += new Vector3(StateMachine.velocity.x, StateMachine.velocity.y, 0);
    }
}

//all this stuff could be in it's own namespace, could make more states and more generic one, so that any enemy state machine could be assembled out of them

//extend the State class into the top level of the enemy state machine
public class EnemySM : State<EnemySM>
{
    //velocity is what the AI eventually looks at to decide how to move
    public Vector2 velocity;

    //member variables that the bottom level states can use
    public Transform player;
    public Transform self;
    public float distance;
    public float speed;
    public float attackingRadius;
    public float lookingRadius;

    //state specific functions, don't do much since most of the core state machine functionality is set up in the state class
    public override void EnterState() { }
    public override string RunState()
    {
        UpdateDistance();
        return null;
    }
    public override void ExitState() { }

    //member functions that the bottom states can use
    public void DisplayAttackMessage()
    {
        Debug.Log("Attacking the Player!");
    }

    public void DealDamage()
    {
        Debug.Log("Damage in progess..");
    }
    public void UpdateDistance()
    {
        distance = Mathf.Abs(Vector3.Distance(self.position, player.position));
    }
    public bool WithinAttackingRadius()
    {
        return (distance <= attackingRadius);
    }

    public bool WithinLookingRadius()
    {
        return (distance <= lookingRadius);
    }

}

//Enemy is looking for the player, starts chasing the player if they get close enough
public class PatrollingState : State<EnemySM>
{
    public override void EnterState()
    {
        Debug.Log("Entered Patrolling State");
    }

    //move to the chasing state when the enemy sees the player
    public override string RunState()
    {
        if (Owner.WithinLookingRadius())
        {
            return "Chasing";
        }
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Exited Patrolling State");
    }
}

//Enemy actively pursues the player, currently flys which is nice
public class ChasingState : State<EnemySM>
{
    public override void EnterState()
    {
        Debug.Log("Entered Chasing State");
    }

    //Chases the player, returns to patrolling if they get too far away, and starts attacking if they get close enough
    public override string RunState()
    {
        if (!Owner.WithinAttackingRadius() && Owner.WithinLookingRadius())        // Enemy can see the player but not within attacking radius, then keep chasing player until enemy is within attacking radius
        {
            Owner.velocity = (Owner.player.position - Owner.self.position).normalized * Mathf.Max(Owner.distance, Owner.speed);        // Update targets position
            //Owner.velocity = new Vector2(1, 0);
        }
        else if (!Owner.WithinLookingRadius())                              // Enemy is not able to see the player, cannot chase after anymore
        {
            return "Patrolling";
        }
        else                                                         // Enemy can see player and is within attacking radius
        {
            return "Attacking";
        }
        return null;
    }
    public override void ExitState()
    {
        Debug.Log("Exited the Chasing State");
    }
}

//attacks the player
public class AttackingState : State<EnemySM>
{
    //runs the attack function while the player's in range
    public override void EnterState()
    {
        Debug.Log("Entered the Attacking State");
    }

    public override string RunState()
    {
        if (Owner.WithinAttackingRadius())                                // Attack the player if enemy is within the attacking radius
        {
            Owner.DisplayAttackMessage();
        }
        else
        {
            return "Chasing";
        }

        return null;
    }

    public override void ExitState()
    {
        Debug.Log("Exited the Attacking State");
    }
}