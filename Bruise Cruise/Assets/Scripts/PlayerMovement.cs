using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb2d;
    private Vector2 moveVector;
    [SerializeField] private float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        moveVector = moveInput.normalized * speed;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) {
            rb2d.velocity = Vector2.up * jumpForce;
        }
    }

    void FixedUpdate ()
    {
        rb2d.MovePosition(rb2d.position + moveVector * Time.fixedDeltaTime);
    }
}
