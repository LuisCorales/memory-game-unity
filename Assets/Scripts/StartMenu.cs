using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] InputField usernameInput;

    public void StartGame()
    {
        Debug.Log("START GAME");
        CrossSceneInformation.Username = usernameInput.text;
        SceneManager.LoadScene(1); // Load the game scene
    }

    void SetActiveStartMenu(bool value)
    {
        this.gameObject.SetActive(value);
    }
}
