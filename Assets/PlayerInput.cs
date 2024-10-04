using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Vector2 movementVector;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = movementVector;
        rb.velocity = new Vector2(0 * movementVector.x, rb.velocity.y);
        Debug.Log(0 * movementVector.x);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        Debug.Log(movementVector);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        rb.AddForce(movement * 5);
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("MyTag"))
        {
            Debug.Log("do something");
        }
    }
}
