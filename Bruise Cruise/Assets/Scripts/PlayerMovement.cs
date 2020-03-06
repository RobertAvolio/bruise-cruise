using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask groundLayer;

    private float fallMultiplier = 10f;
    private float lowJumpMultiplier = 5f;
    private Rigidbody2D rb2d;
    private Vector2 moveVector;
    private bool isGrounded;
    // private bool jumping = false;
    // private bool falling = false;
    private Animator anim;
    private Transform tf;
    private float jumpTimer;
    public float jumpLength;
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tf = GetComponent<Transform>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, 0.5f, groundLayer);
        anim.SetBool(IsGrounded, isGrounded);

        print($"isGrounded: {isGrounded}");
        
        // Y movement
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) && isGrounded) {
            anim.SetBool(IsGrounded, true);
            jumpTimer = jumpLength;
            rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
        }

        // X movement
        if (Input.GetAxis("Horizontal") > 0) {
            tf.eulerAngles = new Vector3(0,0,0);
        } else if (Input.GetAxis("Horizontal") < 0) {
            tf.eulerAngles = new Vector3(0,180,0);
        }

        if (rb2d.velocity.x < -0.01 || rb2d.velocity.x > 0.01) {
            anim.SetBool(Walking,true);
        } else {
            anim.SetBool(Walking,false);
        }
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);

    }
}
