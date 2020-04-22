using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool cannot_move = false;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isSick;

    private Rigidbody2D rb2d;
    private bool isGrounded;
    private Animator anim;
    private Transform tf;
    
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    { 
        /*
         *  REMOVE THIS! ONLY FOR TESTING! VOMIT!
         */
        if (Input.GetKey(KeyCode.P))
        {
            Vomit();
        }
        
        if (!cannot_move)
        {
            
            Jump();
            
            if (Input.GetAxis("Horizontal") > 0)
            {
                tf.eulerAngles = new Vector3(0,0,0);
            } 
            else if (Input.GetAxis("Horizontal") < 0)
            {
                tf.eulerAngles = new Vector3(0,180,0);
            }
            
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                anim.SetBool(Walking, true);
            }
            else
            {
                anim.SetBool(Walking, false);
            }
        }
    }


    private void FixedUpdate()
    {
        if (!cannot_move)
        {
            // X Movement
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);

            isGrounded = Physics2D.OverlapCircle(feetPosition.position, 1f, groundLayer);
            anim.SetBool(Grounded, isGrounded);

            if (isGrounded)
            {
                if (rb2d.velocity.y <= 0f)
                {
                    anim.SetBool(Jumping, false);
                }
            }
            else
            {
                anim.SetBool(Jumping, true);
            }
        }
        else
        {
            // Debug.Log("Reached");
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
    }
    
    private void Jump()
    {
        // Y movement
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) && isGrounded)
        {
            anim.SetTrigger("StartJump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feetPosition.position, 1f);
    }

    public void Vomit()
    {
        cannot_move = true;
        anim.SetTrigger("StartVomit");
    }

    public void EndVomit()
    {
        cannot_move = false;
        anim.SetBool("Sick", false);
    }
}
