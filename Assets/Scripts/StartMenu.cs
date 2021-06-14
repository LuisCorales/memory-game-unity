using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("START GAME");
        SceneManager.LoadScene(1); // Load the game scene
    }

    void SetActiveStartMenu(bool value)
    {
        this.gameObject.SetActive(value);
    }
}
