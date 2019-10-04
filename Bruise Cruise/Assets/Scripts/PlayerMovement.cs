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
    private bool wantsToJump = false;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            wantsToJump = true;
        }
    }

    void FixedUpdate() {
        rb2d.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb2d.velocity.y);

        if (wantsToJump) {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            wantsToJump = false;
        }

        if (rb2d.velocity.y < 0) {
            rb2d.gravityScale = fallMultiplier;
        } else if (rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rb2d.gravityScale = lowJumpMultiplier;
        } else {
            rb2d.gravityScale = 1f;
        }
    }
}
