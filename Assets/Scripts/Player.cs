using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // jumpfrc unused
    private Rigidbody rb;
    private Vector3 movement;
    public float moveSpd = 5f;
    public float jumpFrc = 8f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");

        movement = new Vector3(xInput, 0f, yInput).normalized;

        if (jumpInput && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpFrc, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * moveSpd, rb.velocity.y, movement.z * moveSpd);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float rayLength = 0.1f;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(rayStart, Vector3.down, out hit, rayLength))
        {
            return true;
        }

        return false;
    }
}