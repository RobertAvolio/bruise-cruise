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
    private Animator anim;
    private Transform tf;
    private float jumpTimer;
    public float jumpLength;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
        //rb2d.gravityScale = 1f;
    }

    void Update() {
        
        // Y movement
        if (Input.GetKeyDown(KeyCode.Space) && rb2d.velocity.y == 0) { 
            jumping = true;
            jumpTimer = jumpLength;
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }

        if (jumping && Input.GetKey(KeyCode.Space))
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
