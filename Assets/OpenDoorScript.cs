using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenDoorScript : NetworkBehaviour
{
    // Start is called before the first frame update
    private Label instruction;

    public GameObject objToReturn;

    [SyncVar(hook = nameof(OnObjIsReturned))]
    public bool itemHasReturned = false;
    void Start()
    {
        // Get the root.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Store elements.
        instruction = root.Q<Label>("Instruction");
        instruction.text = "In the lingering shadows of our past exploits, we stand once more at the scene of our last heist, where memories echo and secrets lie dormant...";
        StartCoroutine(HideTextAfterDelay(15f));
    }

    [Command(requiresAuthority = false)]
    private void OnObjIsReturned(bool oldValue, bool newValue)
    {
        objToReturn.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemHasReturned) return;
        itemHasReturned = true;


        if (itemHasReturned)
        {
            instruction.text = "Well done....";
            HideTextAfterDelay2(2f);

            GameManager.LoadEndGameLevel("Level 2");

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
