using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class ReturnScript : NetworkBehaviour
{
    // Start is called before the first frame update
    private Label instruction;

    public GameObject objToReturn;

    [SyncVar(hook = nameof(OnObjIsReturned))]

    private bool itemHasReturned = false;
    void Start()
    {
        // Get the root.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Store elements.
        instruction = root.Q<Label>("Instruction");
        instruction.text = "In the lingering shadows of our past exploits, we stand once more at the scene of our last heist, where memories echo and secrets lie dormant...";
        StartCoroutine(HideTextAfterDelay(5f));
    }

    [Command(requiresAuthority = false)]
    private void OnObjIsReturned(bool oldValue, bool newValue)
    {
        objToReturn.SetActive(true);
        instruction.text = "Well done...Find the door that will reveal your fate.";
        Debug.Log("oBJ has been returnd ");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("ReturnTrigger"))
        {
            itemHasReturned = true;
        }

        if (other.name.Equals("OpenDoorTrigger") && itemHasReturned)
        {
            instruction.text = "Well done...Find the door that will reveal your fate.";
            HideTextAfterDelay2(5f);
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        instruction.text = "In the silent whispers of the night, trace the stolen object's journey to its rightful place, and in its return, find the redemption that awaits"; // Set text to empty string to hide it
    }

    private IEnumerator HideTextAfterDelay2(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        instruction.text = "";
    }
}
