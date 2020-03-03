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

    private FishSM fishSM = new FishSM();
    private SMContainer<FishSM> fishSMContainer;
    private Vector3 position1, position2;

    //user-defined side of the AI
    [SerializeField]
    public Vector3 jumpOffset;
    public List<action> actionSequence;
    public float fishSpeed = 1f, gravity = 1f, agroDist = 5f;
    public float attackJumpForce = 3f, attackAnticipation = 0.5f;
    public Rect attackHitbox = new Rect(-4f, -1f, 8f, 2f);

    private Rigidbody2D rb2d;
    private Animator anim;
    private Vector3 selfPosition;
    private Transform player;

    void Awake()
    {
        #region assemble FishSM
        fishSMContainer = new SMContainer<FishSM>("fishSM", fishSM, new List<SMContainer<FishSM>>()
        {
            new SMContainer<FishSM>("idle", new IdleState(), null),
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        position1 = rb2d.position;
        position2 = position1 + jumpOffset;

        fishSM.self = transform;
        fishSM.player = player;
        fishSM.animator = anim;
        fishSM.agroDist = agroDist;
        fishSM.pos1 = new Vector2(transform.position.x, transform.position.y);
        fishSM.pos2 = new Vector2(transform.position.x + jumpOffset.x, transform.position.y + jumpOffset.y);
        fishSM.attackJumpForce = attackJumpForce;
        fishSM.attackAnticipation = attackAnticipation;
        fishSM.attackHitbox = attackHitbox;

        fishSMContainer.State.EnterState();
    }

    private void Update()
    {
        fishSM.StateUpdate();

        //update velocity based on SM
        rb2d.velocity = new Vector3(fishSM.velocity.x, fishSM.velocity.y, 0);
    }

    #region FishSM
    //just cycles through the list of actions
    class FishSM : State<FishSM>
    {
        public float posThreshold = 1, speed, gravity, agroDist, attackJumpForce, attackAnticipation, leapAnticipation = 0.65f, animSpeed;
        public Rect attackHitbox;

        public bool isAgro;

        public Animator animator;
        public Transform player, self;
        public Vector2 velocity;

        public Vector2 pos1, pos2;
        public List<action> sequence;

        //index of the current action in the action list
        private int currActionIndex;

        action currAction;

        public override void EnterState()
        {
            //set the animation speeds
            AnimationClip clip = null;
            foreach(AnimationClip animClip in animator.runtimeAnimatorController.animationClips)
            {
                if (animClip.name == "Armature|LeapLeft_Stationary")
                {
                    clip = animClip;
                    break;
                }
            }
            animSpeed = (clip.length - leapAnticipation) / (Mathf.Abs(pos1.x - pos2.x) / speed);
            leapAnticipation /= animSpeed;

            isAgro = false;
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

        public action getNextAction()
        {
            return sequence[(currActionIndex + 1) % sequence.Count];
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

        //calculate necessary jump force to get to target position
        public float getJumpForce()
        {
            Vector2 offset = getTargetPos() - new Vector2(self.position.x, self.position.y);
            offset.x = Mathf.Abs(offset.x);
            if (offset.x == 0)
            {
                return 0;
            }
            float jumpForce = (offset.y * speed / offset.x) + (0.5f * gravity * offset.x / speed);
            return jumpForce;
        }
    }

    class IdleState : State<FishSM>
    {
        private float timer;
        public override void EnterState()
        {
            if (Owner.isAgro)
            {
                //start the animation for the next move
                action nextAction = Owner.getNextAction();
                switch (nextAction)
                {
                    case action.pos1:
                        Owner.animator.Play("LeapLeft_Stationary", 0, 0);
                        Owner.animator.speed = Owner.animSpeed;
                        print("HIT");
                        break;
                    case action.pos2:
                        Owner.animator.Play("LeapRight_Stationary", 0, 0);
                        Owner.animator.speed = Owner.animSpeed;
                        print("HIT");
                        break;
                    case action.attack:
                        Owner.animator.Play("LeapUpAttack_Stationary", 0, 0);
                        Owner.animator.speed = 1f;
                        print("HIT");
                        break;
                }
            }
            else
                Owner.animator.Play("Idle");
            timer = 0;
            base.EnterState();
        }

        public override string RunState()
        {

            Owner.velocity = new Vector2(0, 0);
            if ((Owner.player.position - Owner.self.position).magnitude < Owner.agroDist || Owner.isAgro)
            {
                print((Owner.player.position - Owner.self.position).magnitude);
                timer += Time.deltaTime;
                if(timer > Owner.leapAnticipation)
                {
                    //go to the next action in the sequence
                    action nextAction = Owner.nextAction();
                    switch (nextAction)
                    {
                        case action.pos1:
                            return "move";
                        case action.pos2:
                            return "move";
                        case action.attack:
                            return "attack";
                    }
                }
            }
            return null;
        }

        public override void ExitState()
        {
            Owner.isAgro = true;
            base.ExitState();
        }
    }

    class MoveState : State<FishSM>
    {
        private float timer;
        public override void EnterState()
        {
            Owner.velocity.y = Owner.getJumpForce();
            timer = Mathf.Abs(Owner.pos1.x - Owner.pos2.x) / Owner.speed;
            base.EnterState();
        }

        public override string RunState()
        {
            timer -= Time.deltaTime;

            Vector2 targetPos = Owner.getTargetPos();

            if (Mathf.Sign(Owner.pos1.x - Owner.self.position.x) != Mathf.Sign(Owner.pos2.x - Owner.self.position.x) || targetPos.y < Owner.self.position.x)
            {
                Owner.velocity.x = Mathf.Sign(targetPos.x - Owner.self.position.x) * Owner.speed;
            }

            if(targetPos.y >= Owner.self.position.y && Mathf.Abs(targetPos.x - Owner.self.position.x) < Owner.posThreshold)
            {
                return "idle";
            }

            //snap to target position if something didn't go right
            if (timer <= 0)
            {
                Owner.self.position = new Vector2(targetPos.x, targetPos.y);
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
        private float timer;
        private float startHeight;
        private Rect hitbox;
        public override void EnterState()
        {
            Owner.velocity.y = Owner.attackJumpForce;
            Owner.velocity.x = 0;
            startHeight = Owner.self.position.y;
            timer = 0;
            hitbox = new Rect(0, 0, 0, 0);
            base.EnterState();
        }

        public override string RunState()
        {
            if(timer != -1) { }
                timer += Time.deltaTime;
            if(timer > Owner.attackAnticipation)
            {
                timer = -1;
                Owner.velocity.y = 5f;
                hitbox = Owner.attackHitbox;
            }
            if(Owner.self.position.y < startHeight)
            {
                return "idle";
            }

            //check that the hitbox even exists anymore before running OverlapBoxAll()
            if (!hitbox.Equals(new Rect(0, 0, 0, 0)))
            {
                Vector2 pos = hitbox.center + new Vector2(Owner.self.position.x, Owner.self.position.y);
                Collider2D[] thingsHit = Physics2D.OverlapBoxAll(pos, hitbox.size, 0f);
                foreach (Collider2D thing in thingsHit)
                {
                    if (thing.tag == "Player")
                    {
                        //clear the hitbox to prevent damage every frame
                        hitbox = new Rect(0, 0, 0, 0);

                        thing.GetComponent<PlayerStats>()?.TakeDamage();
                    }
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
    #endregion
}
