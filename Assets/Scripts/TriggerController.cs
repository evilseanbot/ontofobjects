using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class TriggerController : MonoBehaviour
{
 
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    public delegate void TriggerControllerHandler(TriggerController triggerController);
    public event TriggerControllerHandler OnTriggerTouchDown;
    public event TriggerControllerHandler OnTriggerTouchUp;

    void Awake()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        SetCurrentDevice();
        HandleDeviceInput();
    }

    private void SetCurrentDevice()
    {
        int deviceIndex = (int)trackedObject.index;
        device = SteamVR_Controller.Input(deviceIndex);
    }

    private void HandleDeviceInput()
    {
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if(OnTriggerTouchDown != null)
            {
                OnTriggerTouchDown(this);
            }
        }
        else if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (OnTriggerTouchUp != null)
            {
                OnTriggerTouchUp(this);
            }
        }
    }

}
