using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionTestVR : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // 1
    public SteamVR_Action_Boolean teleportAction; // 2
    public SteamVR_Action_Boolean grabAction; // 3
    public SteamVR_Action_Boolean interactUI;



    // Update is called once per frame
    void Update()
    {
        if (GetTeleportDown())
        {
            print("Teleport " + handType);
        }

        if (GetGrab())
        {
            print("Grab " + handType);
        }
        if (GetInteractUI()) {
            print("Interact UI"+handType);
        }
    }

    public bool GetTeleportDown() // 1
    {
        return teleportAction.GetStateDown(handType);
    }

    public bool GetInteractUI() {
        return interactUI.GetState(handType);
    }
    public bool GetGrab() // 2
    {
        return grabAction.GetState(handType);
    }
}
