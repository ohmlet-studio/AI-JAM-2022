using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 movement;
    float speed = 50f;

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
            Debug.DrawRay(rb.position, Vector3.down * hit.distance, Color.blue);
            Vector3 targetLocation = hit.point;
            targetLocation += new Vector3(0, transform.localScale.y, 0);
            rb.position = targetLocation;
        }
        else if (Physics.Raycast(rb.position + Vector3.up * 100f, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            // Debug.DrawRay(rb.position, Vector3.up * hit.distance, Color.blue);
            Vector3 targetLocation = hit.point;
            targetLocation += new Vector3(0, transform.localScale.y, 0);
            rb.position = targetLocation;
        }
        else
        {
            Debug.DrawRay(rb.position, Vector3.down * 1000, Color.red);
        }
    }
}
