using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public double gravity = -9.81;
    float turnSmoothVelocity;

    const int IGNORE_RAYCAST_LAYER = 2;

    void Update()
    {
        // Move horizontally and vertically
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        Vector3 move;
        if (direction.magnitude < 0.1f) {
            move = Vector3.zero;
        } else {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            move = moveDir.normalized * speed * Time.deltaTime;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        double g = gravity * Time.deltaTime;
        if (controller.isGrounded) g = 0;
        controller.Move(new Vector3(move.x, (float) gravity, move.z));

    }

    private void FixedUpdate()
    {
        
    }
}
