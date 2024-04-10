using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggersScript : MonoBehaviour
{

    private CharacterController _characterController;
    private Label _displayMessageLabel;
    private Label _instructionMessageLabel;
    private string _previousText;
    private void Start()
    {
        Debug.Log("trig start");
        // Get the root.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        UIDocument instructionUI = FindObjectOfType<UIDocument>();
        VisualElement iroot = instructionUI.rootVisualElement;

        if (instructionUI == null) Debug.Log("No instruction UI");
        _characterController = GetComponent<CharacterController>();
        _displayMessageLabel = root.Q<Label>("Dialogue");

        _instructionMessageLabel = iroot.Q<Label>("Dialogue");
        _displayMessageLabel.text = "";

        Debug.Log(_instructionMessageLabel.text);
        _instructionMessageLabel.text = "naka";
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name.Equals("TvTrigger"))
        {
            Debug.Log("Should display message");
            //_displayMessageLabel.text = _previousText;
            _displayMessageLabel.text = "How reminiscent. My final times were shown on a television...wait, where am I?";
            StartCoroutine(HideTextAfterDelay(3f)); // Start coroutine to hide text after 3 seconds
        }

        if (other.name.Contains("Portal"))
        {
            _characterController.enabled = false; // Disable the character controller temporarily
            _characterController.transform.position = new Vector3(450, 1, 27); // Set the position
            _characterController.enabled = true;
            // _displayMessageLabel.text = "Justice...Two stories...Condemnation is in your hands";
        }

        if (other.name.Contains("Story 1 A"))
        {
            _displayMessageLabel.text = "A painting of a weary man, cradling his ailing child in his arms, a look of concern etched on his face.";

        }

    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        _displayMessageLabel.text = ""; // Set text to empty string to hide it
    }
}
