using Mirror;
using UnityEngine;

public class PlayerInteraction : NetworkBehaviour
{
    public PickUpObject pickUpObject;

    [SyncVar(hook = nameof(OnViewObjectActiveChanged))]
    public bool isViewObjectActive;

    [SyncVar(hook = nameof(OnPickedUpChanged))]
    private bool isPickedUp = false;

    public GameObject viewObject;
    GameObject doorObject = null;

    [SyncVar(hook = nameof(OnDoorStateChanged))]
    private bool isOpen = true;

    private Trials trialsdata;

    [Command(requiresAuthority = false)]
    public void CmdChangeDoorState(bool newState)
    {
        Debug.Log("Door is bring pried open");
        isOpen = newState;
    }

    private void OnDoorStateChanged(bool oldValue, bool newValue)
    {
        doorObject = GameObject.Find("Final_door");
        if (doorObject != null)
        {

            doorObject.SetActive(false); // Enable or disable the door object based on the new state
            Debug.Log("remove le door");
        }
        else
        {
            Debug.Log("le door es null");
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdInteractWithObject();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("L3Trigger"))
        {
            Debug.Log("store final door");
            doorObject = GameObject.Find("Final_door");

            if (doorObject == null) Debug.Log("door no found");
        }

        if (other.name.Equals("OpenGate"))
        {

            if (isPickedUp)
            {
                Debug.Log("Key is picked up");
                CmdChangeDoorState(false);
            }
            else
            {
                Debug.Log("Key no picked up");
            }
        }
    }

    [Command(requiresAuthority = false)]
    private void OnPickedUpChanged(bool oldValue, bool newValue)
    {
        if (newValue)
        {
            Debug.Log("The key hath been picked up");
        }
    }

    [Command(requiresAuthority = false)]
    void CmdInteractWithObject()
    {
        isPickedUp = true;

        PickUpObject stuf = FindObjectOfType<PickUpObject>();
        stuf.CmdPickUpObject();

        if (viewObject == null)
            Debug.Log("Viewed object is null");

        // Toggle the state of the viewObject on the server
        isViewObjectActive = !isViewObjectActive;
    }

    // Called when the SyncVar 'isViewObjectActive' changes
    private void OnViewObjectActiveChanged(bool oldValue, bool newValue)
    {
        if (viewObject != null)
        {
            viewObject.SetActive(newValue);
        }
    }
}