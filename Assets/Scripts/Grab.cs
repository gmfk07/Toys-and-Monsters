using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum Hand { Left, Right }

public class GrabController : MonoBehaviour
{
    public Transform leftTransform;
    public Transform rightTransform;

    public GameObject collidingObjectLeftHand;
    public GameObject collidingObjectRightHand;

    private GameObject objectInLeftHand;
    private GameObject objectInRightHand;

    private List<InputDevice> leftInputDevices = new List<InputDevice>();
    private List<InputDevice> rightInputDevices = new List<InputDevice>();

    public void Start()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftInputDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightInputDevices);

        foreach (var device in leftInputDevices)
        {
            Debug.Log(string.Format("Left-handed device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }

        foreach (var device in rightInputDevices)
        {
            Debug.Log(string.Format("Right-handed device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));
        }
    }

    public void Update()
    {
        float leftGrip, rightGrip;
        leftInputDevices[0].TryGetFeatureValue(CommonUsages.grip, out leftGrip);
        rightInputDevices[0].TryGetFeatureValue(CommonUsages.grip, out rightGrip);

        if (leftInputDevices[0].isValid && leftGrip > 0.2f && collidingObjectLeftHand)
        {
            GrabObject(Hand.Left);
        }

        if (rightInputDevices[0].isValid && rightGrip > 0.2f && collidingObjectRightHand)
        {
            GrabObject(Hand.Right);
        }

        if (leftInputDevices[0].isValid && leftGrip < 0.1f && collidingObjectLeftHand)
        {
            GrabObject(Hand.Left);
        }

        if (rightInputDevices[0].isValid && rightGrip < 0.1f && collidingObjectRightHand)
        {
            GrabObject(Hand.Right);
        }
    }

    private void GrabObject(Hand hand)
    {
        if (hand == Hand.Left)
        {
            objectInLeftHand = collidingObjectLeftHand;
            collidingObjectLeftHand.transform.SetParent(leftTransform);
        }
        if (hand == Hand.Right)
        {
            objectInRightHand = collidingObjectRightHand;
            collidingObjectRightHand.transform.SetParent(rightTransform);
        }
    }

    private void ReleaseObject(Hand hand)
    {
        if (hand == Hand.Left)
        {
            objectInLeftHand = null;
            collidingObjectLeftHand.transform.SetParent(null);
        }
        if (hand == Hand.Right)
        {
            objectInRightHand = null;
            collidingObjectRightHand.transform.SetParent(null);
        }
    }
}
