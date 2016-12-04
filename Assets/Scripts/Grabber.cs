using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TriggerController))]
public class Grabber : MonoBehaviour {

    public float minimumDistance = 0.1f;
    public Animator handAnimator;

    private TriggerController triggerController;

    public void Start()
    {
        triggerController = GetComponent<TriggerController>();
        triggerController.OnTriggerTouchDown += OnTriggerTouchDown;
        triggerController.OnTriggerTouchUp += OnTriggerTouchUp;
        
    }

    private void OnTriggerTouchDown(TriggerController triggerController)
    {
        FindPickup();
    }

    private void OnTriggerTouchUp(TriggerController triggerController)
    {
        DetachPickup();
        //TriggerHandAnimation(HandAnimationType.OnNuetral);
        transform.FindChild("Hand").gameObject.active = true;
    }


    private void FindPickup()
    {
        Pickup closestPickup = null;
        float shortestDistance = minimumDistance;

        Vector3 thisPosition = this.gameObject.transform.position;

        foreach (Pickup pickup in Pickup.activePickups)
        {
            Vector3 pickupPosition = pickup.gameObject.transform.position;
            float distance = Vector3.Distance(thisPosition, pickupPosition);

            if (closestPickup != null)
            {
                if (distance < shortestDistance)
                {
                    closestPickup = pickup;
                    shortestDistance = distance;
                }
            }
            else if(distance < shortestDistance)
            {
                closestPickup = pickup;
                shortestDistance = distance;
            }
        }

        if (closestPickup != null)
        {
            AttachPickup(closestPickup);
        }
        else
        {
            TriggerHandAnimation(HandAnimationType.OnGeneric);
        }
    }

    private Pickup currentPickup;

    private void AttachPickup(Pickup pickup)
    {
        DetachPickup();
        currentPickup = pickup;
        currentPickup.AttachToTransform(this.gameObject.transform);
        //TriggerHandAnimation(currentPickup.handAnimationType);
        transform.FindChild("Hand").gameObject.active = false;

    }

    private void DetachPickup()
    {
        if (currentPickup != null)
        {
            currentPickup.DetachFromTransform(this.gameObject.transform);
        }
    }

    private void TriggerHandAnimation(HandAnimationType animationType)
    {
        handAnimator.SetTrigger(animationType.ToString());
    }

}
