﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;

    // 1 Stores the GameObject that the trigger is currently colliding with, 
    // so you have the ability to grab the object.

    private GameObject collidingObject;

    // 2 Serves as a reference to the GameObject that the player is currently grabbing.

    private GameObject objectInHand;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        // method accepts a collider as a parameter and uses its GameObject as the collidingObject 
        // for grabbing and releasing. Moreover, it:
        // 1 Doesn’t make the GameObject a potential grab target if the player is already holding 
        // something or the object has no rigidbody.
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2 Assigns the object as a potential grab target.
        collidingObject = col.gameObject;
    }

    // These methods handle what should happen when the trigger collider enters and exits another collider.

    // 1 When the trigger collider enters another, this sets up the other collider as a potential grab target.
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // 2 Similar to section one (// 1), but different because it ensures that the target is set when 
    // the player holds a controller over an object for a while. Without this, the collision may fail or become buggy.
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3 When the collider exits an object, abandoning an ungrabbed target, this code removes its target by setting it to null.
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    // to grab an object
    private void GrabObject()
    {
        // 1 Move the GameObject inside the player’s hand and remove it from the collidingObject variable.
        objectInHand = collidingObject;
        collidingObject = null;
        // 2 Add a new joint that connects the controller to the object using the AddFixedJoint() method below.
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3 Make a new fixed joint, add it to the controller, and then set it up so it doesn’t break easily. Finally, you return it.
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    // next block handles releasing the object
    private void ReleaseObject()
    {
        // 1 Make sure there’s a fixed joint attached to the controller.
        if (GetComponent<FixedJoint>())
        {
            // 2 Remove the connection to the object held by the joint and destroy the joint.
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3 Add the speed and rotation of the controller when the player releases the object, so the result is a realistic arc.
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4 Remove the reference to the formerly attached object.
        objectInHand = null;
    }

    // Update is called once per frame
    void Update () {

        // 1 When the player squeezes the trigger and there’s a potential grab target, this grabs it.
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
        }

        // 2 If the player releases the trigger and there’s an object attached to the controller, this releases it
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
