using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // COMPONENTS
    [SerializeField] Text scoreText;

    void Start()
    {
        SetScoreText(CrossSceneInformation.TimeCount);
    }

    public void RestartGame()
    {
        Debug.Log("RESTART GAME");
        SceneManager.LoadScene(1); // Load the game scene to restart (TODO: actually restart the game)
    }

    public void SetScoreText(string score)
    {
        scoreText.text = score + " segundos!";
    }
}
