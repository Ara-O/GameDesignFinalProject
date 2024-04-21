using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class FinalScript : NetworkBehaviour
{
    public VisualTreeAsset endScreenTemplate; // Reference to the end screen UI template created in UI Builder

    [SyncVar(hook = nameof(OnGameEnded))]
    private bool isGameEnded = false;

    private VisualElement endScreen;
    private bool isGameEndScreenDisplayed = false;


    private void EndGame()
    {
        isGameEnded = true; // Set the game-ending state
    }

    private bool IsGameOver()
    {
        // Implement your game-ending condition here
        // For example:
        // return gameTime <= 0 || playerScore >= scoreThreshold;
        return false;
    }

    private void OnGameEnded(bool oldValue, bool newValue)
    {
        if (newValue && !isGameEndScreenDisplayed)
        {
            // Show end screen UI for all players
            ShowEndScreen();
            isGameEndScreenDisplayed = true;
        }
    }

    public void ShowEndScreen()
    {
        // Instantiate the end screen UI from the template
        endScreen = endScreenTemplate.CloneTree();

        // Add the end screen to the root visual element of the UI
        GetComponent<UIDocument>().rootVisualElement.Add(endScreen);

        // Additional logic to display end screen UI (e.g., showing game stats, restart button, etc.)
    }

    private void OnTriggerEnter(Collider other)
    {
        EndGame();
        Debug.Log("You have finished the game");
    }
}
