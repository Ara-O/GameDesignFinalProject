using System.Collections;
using System.Collections.Generic;
using Mirror;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggersScript : NetworkBehaviour
{

    private CharacterController _characterController;
    private Label _displayMessageLabel;
    private Label _instructionMessageLabel;
    private Label _trialsLabel;
    private Label _manLabel;
    private Label _judgeLabel;
    private string _previousText;
    private bool L2Started;
    GameObject doorObject = null;

    [SyncVar(hook = nameof(OnDoorStateChanged))]
    private bool isOpen = true;

    private Trials trialsdata;

    [Command(requiresAuthority = false)]
    public void CmdChangeDoorState(bool newState)
    {
        isOpen = newState;
    }

    private void OnDoorStateChanged(bool oldValue, bool newValue)
    {
        if (doorObject != null)
        {
            Debug.Log("remove le door");
            doorObject.SetActive(newValue); // Enable or disable the door object based on the new state
        }
    }

    private void Start()
    {
        doorObject = GameObject.Find("L2/NextLevelDoor");
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

        if (other.name.Equals("DedTrigger"))
        {
            _characterController.enabled = false; // Disable the character controller temporarily
            _characterController.transform.position = new Vector3(-55.5f, 180f, -1.8f); // Set the position
            _characterController.enabled = true;
        }

        if (other.name.Contains("Portal"))
        {
            _characterController.enabled = false; // Disable the character controller temporarily
            _characterController.transform.position = new Vector3(450, 30, 27); // Set the position
            _characterController.enabled = true;
            _instructionMessageLabel.text = "Justice...Impartiality...Two lives...Condemnation lies in your hands";
            _displayMessageLabel.text = "There are paintings on the wall that seem to tell a story";
            trialsdata = FindObjectOfType<Trials>();

            if (trialsdata == null) Debug.Log("No trials data found");
            _trialsLabel.text = "Trials " + trialsdata.trials;
            StartCoroutine(HideTextAfterDelay(5f));
        }

        if (other.name.Equals("TvTrigger"))
        {
            _displayMessageLabel.text = "A television...How strange...";
            StartCoroutine(ShowNextText());
        }

        if (other.name.Equals("L1StartTrigger"))
        {
            _displayMessageLabel.text = "We have to get to the end";
            _instructionMessageLabel.text = "Free yourselves of temptation and greed...Steady your resolve";
            StartCoroutine(HideTextAfterDelay(5f));
        }

        if (other.name.Equals("L2StartTrigger"))
        {
            L2Started = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting trigger");
        if (other.name.Equals("BookTrigger"))
        {
            StartCoroutine(HideTextAfterDelay(1f));
        }

        if (other.name.Equals("MemoryTrigger"))
        {
            _displayMessageLabel.text = "Well, I'll be damned!";
            StartCoroutine(HideTextAfterDelay(3f));

        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Equals("MemoryTrigger"))
        {
            // _displayMessageLabel.text = "You hear a voice...'Your presence here whispers of a past cloaked in darkness, of deeds that echo through the halls of eternity, beckoning for redemption to soothe restless souls'";
            _displayMessageLabel.text = "Interesting...'Even in death's embrace, the echoes of your former lives as thieves whisper secrets of remorse and redemption, guiding you through trials of redemption to find eternal rest...'";
        }

        if (other.name.Equals("BookTrigger"))
        {
            // _displayMessageLabel.text = "Weird, this book just says: Integrity, Justice, Compassion...Redemption. What, happened to us?";
            _displayMessageLabel.text = "This says: 'Even in death, your shadows still linger with the weight of past sins.' Are we...dead?";
        }


        if (other.name.Contains("Story 1 A"))
        {
            Debug.Log("Viewing story 1");
            _instructionMessageLabel.text = "In the labyrinth of the city's shadows, a band of thieves plots their destiny, their ambitions tempered by the allure of forbidden treasures.";
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
            _instructionMessageLabel.text = "With whispered promises of wealth, they weave intricate webs of deceit, their hearts consumed by the fire of ambition.";
        }


        if (other.name.Contains("Story 3 A"))
        {
            _instructionMessageLabel.text = "As the night unfolds, their illusions crumble like fragile glass, shattered by the piercing gaze of truth.";

        }

        if (other.name.Contains("Story 4 A"))
        {
            _instructionMessageLabel.text = "In the cold embrace of dawn, they face the reckoning of their deeds, their souls weighed by the heavy hand of fate, as justice claims its toll.";

        }

        if (other.name.Contains("Story 1 B"))
        {
            Debug.Log("Viewing story 1");
            _instructionMessageLabel.text = "From the marble halls of power, corrupt officials weave a tangled web of greed and deceit, their hearts hardened by the allure of unchecked authority.";

        }

        if (other.name.Contains("Story 2 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "With each stroke of the pen, they line their pockets at the expense of the people, their conscience silenced by the clink of ill-gotten gains.";
        }


        if (other.name.Contains("Story 3 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "But as the city streets grow darker with discontent, their iron grip begins to falter, the whispers of dissent growing louder with each passing day.";

        }

        if (other.name.Contains("Story 4 B"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "And in the face of rising unrest, they cling to their power with desperate hands, blinded by their own hubris to the inevitable reckoning that awaits.";
        }

        if (other.name.Contains("L3Trigger"))
        {
            Debug.Log("Viewing story 2");
            _instructionMessageLabel.text = "Look within yourself. There your treasure lies";

        }
    }


    void Update()
    {
        if (L2Started)
        {

            if (Input.GetKeyDown(KeyCode.M))
            {
                trialsdata.convictMan();
                _manLabel.text = "Convicted: Thieves";
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                trialsdata.convictJudge();
                _judgeLabel.text = "Convicted: Officials";

            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                trialsdata.reduceTrialsScore();
                _trialsLabel.text = "Trials " + (trialsdata.trials - 1);

                if (trialsdata.judge_convicted && trialsdata.man_convicted)
                {
                    _instructionMessageLabel.text = "In their triumph, the players come to grasp the unyielding principle: justice knows no bias.";

                    CmdChangeDoorState(false);
                }
                else
                {
                    _instructionMessageLabel.text = "You have chosen wrong...Try again";
                    trialsdata.judge_convicted = false;
                    trialsdata.man_convicted = false;
                    _manLabel.text = "";
                    _judgeLabel.text = "";


                }
            }
        }
    }


    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        _displayMessageLabel.text = ""; // Set text to empty string to hide it
    }

    private IEnumerator ShowNextText()
    {
        yield return new WaitForSeconds(3f); // Wait for the specified delay
        _displayMessageLabel.text = "Wait...where are we?"; // Set text to empty string to hide it
        StartCoroutine(ShowNextText2());
    }

    private IEnumerator ShowNextText2()
    {
        yield return new WaitForSeconds(2f); // Wait for the specified delay
        _displayMessageLabel.text = ""; // Set text to empty string to hide it
    }
}
