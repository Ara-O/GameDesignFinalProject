using Mirror;
using UnityEngine;

public class PickUpObject : NetworkBehaviour
{
    // Reference to the Cube object
    public GameObject cubeObject;

    // Bool to track if the object is currently picked up
    [SyncVar]
    private bool isPickedUp = false;

    // Function to handle object pickup
    [Command]
    public void CmdPickUpObject()
    {

        isPickedUp = true;
        Debug.Log("obj is pickedup");
    }
}