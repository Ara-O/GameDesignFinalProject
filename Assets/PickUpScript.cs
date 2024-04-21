using Mirror;
using UnityEngine;

public class PickUpObject : NetworkBehaviour
{
    // Reference to the Cube object
    public GameObject cubeObject;

    // Bool to track if the object is currently picked up
    [SyncVar(hook = nameof(OnIsPickedUpChanged))]
    public bool isPickedUp = false;

    private void OnIsPickedUpChanged(bool oldIsPickedUp, bool newIsPickedUp)
    {
        // Set the visibility of the cube object based on the new value of isPickedUp
        cubeObject.SetActive(!newIsPickedUp);
    }

    // Function to handle object pickup
    [Command(requiresAuthority = false)]
    public void CmdPickUpObject()
    {

        isPickedUp = true;
        cubeObject.SetActive(false);

        Debug.Log("obj is pickedup");
    }
}