using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 movement;
    float speed = 10f;

    const int IGNORE_RAYCAST_LAYER = 2;

    void Update()
    {
        movement.z = Input.GetAxis("Vertical");
        movement.x = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        rb.position = rb.position + movement * Time.fixedDeltaTime * speed;

        RaycastHit hit;
        int layerMask = 1 << IGNORE_RAYCAST_LAYER;
        layerMask = ~layerMask;


        if (Physics.Raycast(rb.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            rb.position = rb.position + hit.distance * Vector3.down;
            Debug.DrawRay(rb.position, Vector3.down * hit.distance, Color.blue);
        }
        else
        {
            Debug.DrawRay(rb.position, Vector3.down * 1000, Color.red);
        }
    }
}
