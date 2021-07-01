using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] InputField usernameInput;
    [SerializeField] InputField emailInput;
    [SerializeField] Button startButton;

    public void StartGame()
    {
        Debug.Log("START GAME");
        CrossSceneInformation.Username = usernameInput.text;
        CrossSceneInformation.Email = emailInput.text;
        SceneManager.LoadScene(1); // Load the game scene
    }

    public void CheckIfInputHaveValues()
    {
        if(usernameInput.text != "" && emailInput.text != "")
        {
            startButton.enabled = true;
        }
    }
}
