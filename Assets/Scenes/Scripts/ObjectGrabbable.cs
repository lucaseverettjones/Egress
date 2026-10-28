using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;

    void Start()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (objectGrabPointTransform != null) 
        {
            float lerpSpeed = 10.0f;
            Vector3 targetPosition = objectGrabPointTransform.position;
            Vector3 direction = targetPosition - transform.position;
            float distance = direction.magnitude;

            // If the object is far enough from the target, apply a force to move towards it
            if (distance > 0.1f)
            {
                Vector3 moveDirection = direction.normalized * lerpSpeed;
                objectRigidbody.velocity = moveDirection;
            }
            else
            {
                // If the object is close enough, directly set its position to the target
                objectRigidbody.MovePosition(targetPosition);
                objectRigidbody.velocity = Vector3.zero;
            }
            }
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        objectRigidbody.freezeRotation = true;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.freezeRotation = false;
    }
}
