using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHand : MonoBehaviour
{
    public Hand hand;
    public GrabController grabController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            if (hand == Hand.Left)
            {
                grabController.collidingObjectLeftHand = other.gameObject;
            }
            else
            {
                grabController.collidingObjectRightHand = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grabbable")
        {
            if (hand == Hand.Left)
            {
                grabController.collidingObjectLeftHand = null;
            }
            else
            {
                grabController.collidingObjectRightHand = null;
            }
        }
    }
}
