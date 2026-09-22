using System;
using UnityEngine;

public class DoorHingeScript : MonoBehaviour
{

    [SerializeField] private HingeJoint2D hingeJointReference;
    [SerializeField] private GameObject doorObject;
    private bool _isColliding = false;

    void Update()
    {
        if (_isColliding == false)
        {
            if (doorObject.transform.localRotation.z > 1)
            {
                JointMotor2D motor = hingeJointReference.motor;
                motor.motorSpeed = 20;
                hingeJointReference.motor = motor;
            }

            if (doorObject.transform.localRotation.z < 1)
            {
                JointMotor2D motor = hingeJointReference.motor;
                motor.motorSpeed = -20;
                hingeJointReference.motor = motor;
            }

            if (doorObject.transform.localRotation.z == 0)
            {
                JointMotor2D motor = hingeJointReference.motor;
                motor.motorSpeed = 0;
                hingeJointReference.motor = motor;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isColliding = false;
    }
}
