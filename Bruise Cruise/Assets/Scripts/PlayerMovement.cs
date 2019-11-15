using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private float fallMultiplier = 10f;
    private float lowJumpMultiplier = 5f;
    private Rigidbody2D rb2d;
    private Vector2 moveVector;
    private bool jumping = false;
    private bool falling = false;
    private Animator anim;
    private Transform tf;
    private float jumpTimer;
    public float jumpLength;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
    }

    void Update() {
        
        if (rb2d.velocity.y < 0)
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        if(rb2d.velocity.y >= 0)
        {
            anim.SetBool("Falling", false);
        }

        // Y movement
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) && rb2d.velocity.y == 0) { 
            jumping = true;
            anim.SetBool("Jumping", true);
            jumpTimer = jumpLength;
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
        }
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp("joystick button 0")))
        {
            jumping = false;
        }

        if (jumping && (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick button 0")))
        {

            if (jumpTimer > 0)
            {
                rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
                jumpTimer -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }


        // X movement
        if (Input.GetAxis("Horizontal") > 0) {
            tf.eulerAngles = new Vector3(0,0,0);
        } else if (Input.GetAxis("Horizontal") < 0) {
            tf.eulerAngles = new Vector3(0,180,0);
        }

        if (rb2d.velocity.x != 0) {
            anim.SetBool("Walking",true);
        } else {
            anim.SetBool("Walking",false);
        }
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);

    }
}
