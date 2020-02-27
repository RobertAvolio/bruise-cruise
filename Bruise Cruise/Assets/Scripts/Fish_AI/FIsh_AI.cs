using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachineStuff;

public class Fish_AI : MonoBehaviour
{
    //AI state machine relate stuff
    public enum action
    {
        pos1,
        pos2,
        attack
    }
    FishSM fishSM = new FishSM();
    SMContainer<FishSM> fishSMContainer;

    //user-defined side of the AI
    [SerializeField]
    public Vector3 position1, position2;
    public List<action> actionSequence;
    public float fishSpeed = 1;
    public float gravity = 1;

    //dealing with the fish's transform
    Rigidbody2D rb2d;
    Vector3 selfPosition;

    void Awake()
    {
        #region assemble FishSM
        fishSMContainer = new SMContainer<FishSM>("fishSM", fishSM, new List<SMContainer<FishSM>>()
        {
            new SMContainer<FishSM>("move", new MoveState(), null),
            new SMContainer<FishSM>("attack", new AttackState(), null)
        });

        foreach(SMContainer<FishSM> container in fishSMContainer.ReturnAllSubStates())
        {
            container.State.setOwner(fishSM);
        }

        fishSMContainer.assemble();

        //initialize the sequence
        fishSM.sequence = actionSequence;
        fishSM.speed = fishSpeed;
        fishSM.gravity = gravity;
        #endregion
    }

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.position = position1;

        fishSMContainer.State.EnterState();
    }

    private void Update()
    {
        //update all the sm values
        fishSM.position = transform.position;
        fishSM.pos1 = new Vector2(position1.x, position1.y);
        fishSM.pos2 = new Vector2(position2.x, position2.y);

        fishSM.StateUpdate();

        //update velocity based on SM
        rb2d.velocity = new Vector3(fishSM.velocity.x, fishSM.velocity.y, 0);
    }

    #region ThotSM
    //just cycles through the list of actions
    class FishSM : State<FishSM>
    {
        public float posThreshold = 1;
        public float speed;
        public float gravity;

        public Vector2 position;
        public Vector2 velocity;

        public Vector2 pos1;
        public Vector2 pos2;

        public List<action> sequence;
        //index of the current action in the action list
        private int currActionIndex;

        action currAction;

        public override void EnterState()
        {
            currActionIndex = 0;
            base.EnterState();
        }

        public override string RunState()
        {
            return null;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
        
        public action nextAction()
        {
            currActionIndex = (currActionIndex + 1) % sequence.Count;
            currAction = sequence[currActionIndex];

            return currAction;
        }

        public Vector2 getTargetPos()
        {
            switch (currAction)
            {
                case action.pos1:
                    return pos1;
                case action.pos2:
                    return pos2;
                default:
                    return new Vector2(0, 0);
            }
        }
    }

    class MoveState : State<FishSM>
    {
        public override void EnterState()
        {
            Owner.velocity.y = 10;
            base.EnterState();
        }

        public override string RunState()
        {
            Vector2 targetPos = Owner.getTargetPos();

            if (Mathf.Sign(Owner.pos1.x - Owner.position.x) != Mathf.Sign(Owner.pos2.x - Owner.position.x) || targetPos.y < Owner.position.x)
            {
                Owner.velocity.x = Mathf.Sign(targetPos.x - Owner.position.x) * Owner.speed;
            }
            if(targetPos.y >= Owner.position.y && Mathf.Abs(targetPos.x - Owner.position.x) < Owner.posThreshold)
            if(targetPos.y >= Owner.position.y && Mathf.Abs(targetPos.x - Owner.position.x) < Owner.posThreshold)
            {
                print("HIT");
                action nextAction = Owner.nextAction();

                if (nextAction == action.attack)
                {
                    return "attack";
                }
                if (nextAction == action.pos1 || nextAction == action.pos2)
                {
                    Owner.velocity.y = 10;
                    return "move";
                }
            }

            Owner.velocity.y -= Owner.gravity * Time.deltaTime;

            return null;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }

    class AttackState : State<FishSM>
    {
        public override void EnterState()
        {
            base.EnterState();
        }

        public override string RunState()
        {
            return null;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
    #endregion
}
