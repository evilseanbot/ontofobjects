using UnityEngine;
using System.Collections;

public class PhysicsFollower : MonoBehaviour {

    public Transform targetTransform;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = Mathf.Infinity;
    }

    void FixedUpdate() {
        if (targetTransform == null || targetTransform.gameObject.activeInHierarchy == false) return;

        Vector3 velocity = (targetTransform.position - transform.position) / Time.fixedDeltaTime;
        rigidbody.velocity = velocity;

        Quaternion deltaRotation = targetTransform.rotation * Quaternion.Inverse(transform.rotation);

        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        deltaRotation.ToAngleAxis(out angle, out axis);

        if (angle >= 180.0f)
        {
            angle = 360.0f - angle;
            axis = -axis;
        }

        if (angle != 0.0f)
        {
            rigidbody.angularVelocity = angle * axis;
        }
 
    }
}

