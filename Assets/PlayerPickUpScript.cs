using Mirror;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    public PickUpObject pickUpObject;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickUpObject != null)
            {
                CmdInteractWithObject();
            }
            else
            {
                Debug.Log("is null");
            }
        }
    }

    [Command]
    void CmdInteractWithObject()
    {
        pickUpObject.CmdPickUpObject();
    }
}