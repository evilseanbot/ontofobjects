using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PhysicsFollower))]
[RequireComponent(typeof(Rigidbody))]
public class Pickup : MonoBehaviour {
    
    public HandAnimationType handAnimationType = HandAnimationType.OnGeneric;

    public static HashSet<Pickup> activePickups = new HashSet<Pickup>();

    public delegate void PickupHandler(Pickup pickup);
    public event PickupHandler OnAttached;
    public event PickupHandler OnDetached;

    private static void RegisterPickup(Pickup pickup)
    {
        activePickups.Add(pickup);
    }

    private static void UnregisterPickup(Pickup pickup)
    {
        activePickups.Remove(pickup);
    }

    public void OnEnable()
    {
        RegisterPickup(this);
    }

    public void OnDisable()
    {
        UnregisterPickup(this);
    }

    private PhysicsFollower physicsFollower;
    new private Rigidbody rigidbody;

    public void Start()
    {
        physicsFollower = GetComponent<PhysicsFollower>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void AttachToTransform(Transform transform)
    {
        rigidbody.transform.position = transform.position;
        rigidbody.transform.rotation = transform.rotation;
        physicsFollower.targetTransform = transform;

        if (OnAttached != null)
        {
            OnAttached(this);
        }
    }

    public void DetachFromTransform(Transform targetTransform = null)
    {
        if (targetTransform == null || targetTransform == physicsFollower.targetTransform)
        {
            physicsFollower.targetTransform = null;

            if (OnDetached != null)
            {
                OnDetached(this);
            }
        } 
    }

    public bool isAttached
    {
        get
        {
            return physicsFollower.targetTransform != null;
        }
    }

}
