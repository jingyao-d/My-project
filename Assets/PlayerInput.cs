using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool hasDash;
    private bool left;
    private bool right;

    /* [SerializeField] allows you to modify 
     * the value of speed in the Unity inspector 
     */
    [SerializeField] int speed = 5;
    [SerializeField] int dashDistance = 500;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        /* 
         * Remember, David did something very dumb with this! 
         * This causes things to fall VERY slow... recall why.
         * rb.velocity = movementVector; 
         */

        /* This is a more correct way to do movement. 
         * Note that W/A keys now do nothing.
         * Vertical movement should now be handled by jump. 
         */
        rb.velocity = new Vector2(speed * movementVector.x, rb.velocity.y);
        if (movementVector.x > 0)
        {
            right = true;
            left = false;
            Debug.Log("right");
        }
        else if (movementVector.x < 0)
        {
            right = false; 
            left = true;

            Debug.Log("left");
        }
        
    }

    /* OnMove works with the InputActions we set. 
     * The name is specific; remember that we 
     * called our WASD action binding "Move". 
     */
    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);
    }

    /* When the player collides with the ground, 
     * we set the boolean isGrounded to true.
     * When the player exits the ground, 
     * the boolean is set to false. 
     */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasDash = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    /* OnJump executes when SPACE is pressed. 
     * Note that we only jump when the player
     * is currently on the ground. 
     */
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, 400));
        }
    }

    void OnDash(InputValue value)
    {
        
        if (hasDash) 
        {
            if (right)
            {
                rb.AddForce(new Vector2(0, 200));
                //rb.AddForce(new Vector2(-400, 0));
                //rb.velocity = new Vector2(dashDistance, rb.velocity.y);
                hasDash = false;
                Debug.Log("dash");
            }
            if (left)
            {
                rb.AddForce(new Vector2(0, 200));
                //rb.velocity = new Vector2(-dashDistance, rb.velocity.y);
                hasDash = false;
                Debug.Log("dash");
            }

        }
    }
}