using System;
using UnityEngine;

public class DoorHingeScript : MonoBehaviour
{

    [SerializeField] private HingeJoint2D hingeJointReference;
    [SerializeField] private GameObject doorObject;
    [SerializeField] private LayerMask mask;
    private bool _isColliding = false;

    void FixedUpdate()
    {
        float currentAngle = hingeJointReference.jointAngle;
        float targetAngle = 0f;
        float tolerance = 2.0f; // Dead zone to prevent jittering/overshooting

        JointMotor2D motor = hingeJointReference.motor;
        
        if (!_isColliding)
        {
            if (currentAngle > targetAngle + tolerance)
            {
                motor.motorSpeed = -20f;
            }
            else if (currentAngle < targetAngle - tolerance)
            {
                motor.motorSpeed = 20f;
            }
            else
            {
                motor.motorSpeed = 0f;
            }

            hingeJointReference.motor = motor;
        }
        else if (_isColliding)
        {
            motor.motorSpeed = 0;
            hingeJointReference.motor = motor;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter: "  + other.gameObject.name);
        if ((1<<(other.gameObject.layer) & mask.value) != 0 )
        {
            Debug.Log("is colliding");
            _isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Trigger exit");
        if ((1<<(other.gameObject.layer) & mask.value) != 0 )
        {
            Debug.Log("is not colliding");
            _isColliding = false;
        }
    }
    
}
