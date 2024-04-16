using Mirror;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    public PickUpObject pickUpObject;
    public GameObject viewObject;

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // if (pickUpObject != null)
            // {
            CmdInteractWithObject();
            // }
            // else
            // {
            //     Debug.Log("is null");
            // }
        }
    }

    [Command(requiresAuthority = false)]
    void CmdInteractWithObject()
    {
        PickUpObject stuf = FindObjectOfType<PickUpObject>();
        stuf.CmdPickUpObject();

        if (viewObject == null) Debug.Log("Viewed object is null");
        viewObject.SetActive(true);
    }
}