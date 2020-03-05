using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineStuff;
public class Enemy_AI_Duel_StateMachine : MonoBehaviour
{
    public Transform Hurtbox;

    MotionSM motionSM = new MotionSM();
    SMContainer<MotionSM> MotionSMContainer;

    //create AI state machine
    ThotSM thotSM = new ThotSM();
    SMContainer<ThotSM> ThotSMContainer;


    Rigidbody2D rb2d;

    private Animator anim;
    private Transform player;

    [SerializeField]
    private int health = 5;

    //enemy takes damage
    public void TakeDamage()
    {

        List<string> activeStates = MotionSMContainer.getActiveStates();
        if (!activeStates.Contains("stagger"))
        {
            //take that bitch health away
            health--;
            // if the health is zero or below, kill it
            if (health <= 0) gameObject.SetActive(false);
            anim.Play("Damage1");
            MotionSMContainer.forceState("stagger");
        }
    }

    void Awake()
    {
        //connect hurtboxes to thotSM
        thotSM.Hurtbox = Hurtbox;

        #region assemble motionSM
        //create the entire motion state machine
        MotionSMContainer = new SMContainer<MotionSM>("MotionSM", motionSM, new List<SMContainer<MotionSM>>()
        {
            new SMContainer<MotionSM>("movement", new MovementState(), new List<SMContainer<MotionSM>>()
            {
                new SMContainer<MotionSM>("running", new RunningState(), null),
                new SMContainer<MotionSM>("jumping", new JumpingState(), null),
                new SMContainer<MotionSM>("falling", new FallingState(), null)
            }),
            new SMContainer<MotionSM>("attacking", new AttackState(), null),
            new SMContainer<MotionSM>("stagger", new StaggerState(), null)
        });

        //assemble and connect the motion state machine
        foreach (SMContainer<MotionSM> container in MotionSMContainer.ReturnAllSubStates())
        {
            container.State.setOwner(motionSM);
        }
        MotionSMContainer.assemble();

        #endregion
        #region assemble thotSM
        //create the entire motion state machine
        ThotSMContainer = new SMContainer<ThotSM>("ThotSM", thotSM, new List<SMContainer<ThotSM>>
        {
            new SMContainer<ThotSM>("patrolling", new PatrollingThot(), null),
            new SMContainer<ThotSM>("chasing", new ChasingThot(), null),
            new SMContainer<ThotSM>("combat", new CombatThot(), null)
        });

        //assemble and connect the AI state machine
        foreach (SMContainer<ThotSM> container in ThotSMContainer.ReturnAllSubStates())
        {
            container.State.setOwner(thotSM);
        }
        ThotSMContainer.assemble();
        #endregion
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //not super necessary in most cases but it usually heklps with weird bugs
        MotionSMContainer.State.EnterState();
        thotSM.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 selfPosition = transform.position;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        motionSM.self = new Vector2(selfPosition.x, selfPosition.y);
        thotSM.self = new Vector2(selfPosition.x, selfPosition.y);
        thotSM.player = new Vector2(playerPosition.x, playerPosition.y);
        rb2d.velocity = new Vector3(motionSM.velocity.x, rb2d.velocity.y, 0);

        motionSM.isAttacking = thotSM.isAttacking;
        motionSM.targetPosition = thotSM.targetPosition;

        motionSM.StateUpdate();
        thotSM.StateUpdate();

        if (motionSM.velocity.x < -1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if (motionSM.velocity.x > 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}

//Decides on a target direction to pass to motionSM and decides when to attack
#region ThotSM
#region tier one state
class ThotSM : State<ThotSM>
{
    [SerializeField]
    public Transform Hurtbox;

    public Vector2 player;
    public Vector2 self;
    public Vector2 targetPosition;
    public bool isAttacking;
    protected float distanceToPlayer;
    protected float attackRadius = 3.5f;
    protected float lookingRadius = 15;

    public override void EnterState()
    {
        base.EnterState();
    }

    public override string RunState()
    {
        distanceToPlayer = Vector2.Distance(player, self);
        return null;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public bool inLookingRadius()
    {
        return distanceToPlayer < lookingRadius;
    }

    public bool inAttackRadius()
    {
        return distanceToPlayer < attackRadius;
    }
}
#endregion
#region tier two states
class CombatThot : State<ThotSM>
{
    private int combatTimer;
    public override void EnterState()
    {
        Collider2D[] thingsHit = Physics2D.OverlapBoxAll(new Vector2(Owner.Hurtbox.position.x, Owner.Hurtbox.position.y), new Vector2(7, 5), 0f);
        foreach (Collider2D thing in thingsHit)
        {
            if (thing.tag == "Player")
            {
                thing.GetComponent<PlayerStats>()?.TakeDamage();
            }
                
        }
        combatTimer = 10;
        Owner.isAttacking = true;
        base.EnterState();
    }

    public override string RunState()
    {
        combatTimer--;
        if (combatTimer <= 0)
        {
            return "chasing";
        }
        return null;
    }

    public override void ExitState()
    {
        Owner.isAttacking = false;
        base.ExitState();
    }

}

class PatrollingThot : State<ThotSM>
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override string RunState()
    {
        Owner.targetPosition = Owner.self;
        if (Owner.inLookingRadius())
        {
            return "chasing";
        }
        return null;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

class ChasingThot : State<ThotSM>
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override string RunState()
    {
        Owner.targetPosition = Owner.player;
        if (!Owner.inLookingRadius())
        {
            return "patrolling";
        }
        if (Owner.inAttackRadius())
        {
            return "combat";
        }
        return null;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
#endregion
#endregion

//gets target position <and eventually maybe attack states> from ThotSM, calculates a velocity for the enemy
//the plan is for this to be general enough for any enemy to use, any kind of AI statemachine could pass a target position into it
#region motionSM
#region tier one state
class MotionSM : State<MotionSM>
{
    public Vector2 velocity;
    public Vector2 targetPosition;
    public Vector2 self;
    public float speed;
    public float Gravity;
    public bool isAttacking;
    public bool onGround = true;
    public override void EnterState()
    {
        base.EnterState();
    }

    public override string RunState() => null;

    public override void ExitState()
    {
        base.ExitState();
    }
}
#endregion
#region tier two states
//Enemy is running or jumping towards a target position
class MovementState : State<MotionSM>
{
    public override void EnterState()
    {
        base.EnterState();
    }

    public override string RunState()
    {
        if (Owner.isAttacking)
        {
            return "attacking";
        }

        return null;
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

//Enemy is in combat and attacking the player
class AttackState : State<MotionSM>
{
    protected int combatTimer;
    public override void EnterState()
    {
        Owner.velocity = new Vector2(0, 0);
        Debug.Log("entered the attacking state");
        base.EnterState();
    }

    public override string RunState()
    {
        if (!Owner.isAttacking)
        {
            return "movement";
        }
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("exited the attacking state");
        base.ExitState();
    }
}

//enemy has been hit and can't do anything
class StaggerState : State<MotionSM>
{
    public int staggerTimer;
    public override void EnterState()
    {
        staggerTimer = 10;
        Debug.Log("entered Stagger state");
        base.EnterState();
    }

    public override string RunState()
    {
        staggerTimer--;
        if (staggerTimer <= 0)
        {
            return "movement";
        }
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("exited Stagger state");
        base.ExitState();
    }
}
#endregion
#region tier three states
//Onground movement
class RunningState : State<MotionSM>
{
    public override void EnterState()
    {
        Owner.Gravity = 10;
        Owner.speed = 2.5f;
        Debug.Log("entered running state");
        base.EnterState();
    }

    public override string RunState()
    {
        if (!Owner.onGround)
        {
            return "falling";
        }
        if (Vector2.Distance(Owner.self, Owner.targetPosition) >= 5)
        {
            Owner.velocity = new Vector2(Mathf.Sign(Owner.targetPosition.x - Owner.self.x) * Owner.speed, 0);
        }
        else
        {
            Owner.velocity = new Vector2(0, 0);
        }
        return null;
    }

    public override void ExitState()
    {
        Debug.Log("exited running state");
        base.ExitState();
    }
}

//it's fuckin jumping dude
class JumpingState : State<MotionSM>
{
    public override void EnterState()
    {
        Owner.Gravity = 7;
        Owner.speed = 2;
        Debug.Log("entered jumping state");
        base.EnterState();
    }

    public override string RunState()
    {
        if (Owner.onGround)
        {
            return "falling";
        }

        if (Vector2.Distance(Owner.self, Owner.targetPosition) <= 0.1)
        {
            Owner.velocity.x = Mathf.Sign(Owner.targetPosition.x - Owner.self.x) * Owner.speed;
        }

        return null;
    }

    public override void ExitState()
    {
        Debug.Log("exited jumping state");
        base.ExitState();
    }
}

//rounds out areial motion because in videogames upwards and downwards motion is different?
class FallingState : State<MotionSM>
{
    public override void EnterState()
    {
        Owner.speed = 1;
        Owner.Gravity = 10;
        Debug.Log("entered falling state");
        base.EnterState();
    }

    public override string RunState()
    {
        if (Owner.onGround)
        {
            return "running";
        }

        if (Vector2.Distance(Owner.self, Owner.targetPosition) <= 0.1)
        {
            Owner.velocity.x = Mathf.Sign(Owner.targetPosition.x - Owner.self.x) * Owner.speed;
        }

        return null;
    }

    public override void ExitState()
    {
        Debug.Log("exited falling state");
        base.EnterState();
    }
}
#endregion
#endregion
