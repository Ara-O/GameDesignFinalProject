using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggersScript : MonoBehaviour
{

    private CharacterController _characterController;
    private Label _displayMessageLabel;
    private Label _instructionMessageLabel;
    private Label _trialsLabel;
    private Label _manLabel;
    private Label _judgeLabel;
    private string _previousText;

    private Trials trialsdata;
    private void Start()
    {
        Debug.Log("Trigger script initialized");
        // Get the root.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        _characterController = GetComponent<CharacterController>();
        _displayMessageLabel = root.Q<Label>("Dialogue");
        _instructionMessageLabel = root.Q<Label>("Instruction");
        _trialsLabel = root.Q<Label>("Trials");
        _manLabel = root.Q<Label>("Man");
        _judgeLabel = root.Q<Label>("Judge");

        _displayMessageLabel.text = "";


        // trialsdata.IncrementTrialsScore();
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
            _characterController.transform.position = new Vector3(450, 30, 27); // Set the position
            _characterController.enabled = true;
            _instructionMessageLabel.text = "Justice...Impartiality...Two stories...Condemnation is in your hands";
            trialsdata = FindObjectOfType<Trials>();

            if (trialsdata == null) Debug.Log("No trials data found");
            _trialsLabel.text = "Trials " + trialsdata.trials;
            StartCoroutine(HideTextAfterDelay(5f));
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Story 1 A"))
        {
            Debug.Log("Viewing story 1");
            _instructionMessageLabel.text = "A painting of a weary man, cradling his ailing child in his arms, a look of concern etched on his face.";
        }

        if (other.name.Contains("ConvictMan"))
        {
            _instructionMessageLabel.text = "Press M to convict the man";
        }

        if (other.name.Equals("Convict"))
        {
            _instructionMessageLabel.text = "Press C to Convict. Your fate lies on your decision...You have three trials.";
        }

        if (other.name.Contains("ConvictJudge"))
        {
            _instructionMessageLabel.text = "Press J to convict the judge";
        }

        if (other.name.Contains("Story 2 A"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "The man is seen in a supermarket, hovering over a pile of watermelons.";
        }


        if (other.name.Contains("Story 3 A"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "Unaware of the guard next to him, the man grabs the watermelon and starts running.";

        }

        if (other.name.Contains("Story 4 A"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "The man is shown as injured, a broken watermalon lies on the floor. He has been caught.";

        }

        if (other.name.Contains("Story 1 B"))
        {
            Debug.Log("Viewing story 1");
            _instructionMessageLabel.text = "A painting of a judge, presiding over a jury.";

        }

        if (other.name.Contains("Story 2 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "The judge meets the man, and we see the man try to explain his situation.";
        }


        if (other.name.Contains("Story 3 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "The judge taking the mans daughter for himself.";

        }

        if (other.name.Contains("Story 4 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "The judge sentences the man.";

        }
        StartCoroutine(HideTextAfterDelay(3f));
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            trialsdata.convictMan();
            _manLabel.text = "The man has been convicted";
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            trialsdata.convictJudge();
            _judgeLabel.text = "The judge has been convicted";

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            trialsdata.reduceTrialsScore();
            _trialsLabel.text = "Trials " + trialsdata.trials;

            if (trialsdata.judge_convicted && trialsdata.man_convicted)
            {
                _instructionMessageLabel.text = "Good job...You have realized the inerrancy in the civil system...";
                // SceneManager.LoadScene(3);
            }
            else
            {
                _instructionMessageLabel.text = "You have chosen wrong...Try again";
                trialsdata.judge_convicted = false;
                trialsdata.man_convicted = false;
                _manLabel.text = "";
                _judgeLabel.text = "";

                if (trialsdata.trials == 0)
                {
                    Time.timeScale = 0f;
                }
            }
        }
    }


    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        _displayMessageLabel.text = ""; // Set text to empty string to hide it
    }
}
