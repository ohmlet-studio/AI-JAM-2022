using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    const int IGNORE_RAYCAST_LAYER = 2;

    void Update()
    {
        // Move horizontally and vertically
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Adapt height to the ground
        Rigidbody rb = controller.attachedRigidbody;
        RaycastHit hit;
        int layerMask = 1 << IGNORE_RAYCAST_LAYER;
        layerMask = ~layerMask;


        if (Physics.Raycast(rb.position + Vector3.up * 1000f, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
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

    private void FixedUpdate()
    {
        
    }
}
