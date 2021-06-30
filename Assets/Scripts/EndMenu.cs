using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System;

public class EndMenu : MonoBehaviour
{
    // COMPONENTS
    [SerializeField] Text scoreText;
    float score;
    string username;

    void Start()
    {
        score = CrossSceneInformation.TimeCount;
        username = CrossSceneInformation.Username;
        SetScoreText(score);
        SendScoreToScoreBoard(username, score);
    }

    public void RestartGame()
    {
        Debug.Log("RESTART GAME");
        SceneManager.LoadScene(1); // Load the game scene to restart (TODO: actually restart the game)
    }

    public void SetScoreText(float score)
    {
        scoreText.text = score.ToString("F2") + " segundos!";
    }

    async void SendScoreToScoreBoard(string username, float score)
    {
        score = Convert.ToInt32(score);

        // Use dreamlo restful api to set scores
        using var client = new HttpClient();
        await client.GetStringAsync($"http://dreamlo.com/lb/xU3rq48dGkeR3dLW3EG9lgdEvNx5mfJUid5rTguPx2-g/add/{username}/{score}");
    }
}
