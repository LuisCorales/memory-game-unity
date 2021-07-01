using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Http;

public class EndMenu : MonoBehaviour
{
    // COMPONENTS
    [SerializeField] Text scoreText;
    [SerializeField] Text leaderboard;

    // INFO
    float score;
    string username;
    string email;

    [SerializeField] dreamloLeaderBoard dreamloLB;
    List<dreamloLeaderBoard.Score> scoreList;

    void Start()
    {
        score = CrossSceneInformation.TimeCount;
        username = CrossSceneInformation.Username;
        email = CrossSceneInformation.Email;

        SetScoreText(score);

        dreamloLB = dreamloLeaderBoard.GetSceneDreamloLeaderboard();
        dreamloLB.AddScore(username , -Convert.ToInt32(score), Convert.ToInt32(score), email);
        StartCoroutine(Tryer()); 
    }

    public void RestartGame()
    {
        Debug.Log("RESTART GAME");
        SceneManager.LoadScene(1); // Load the game scene to restart 
    }

    public void SetScoreText(float score)
    {
        scoreText.text = "¡Lo resolviste en " + score.ToString("F2") + " segundos!";
    }

    IEnumerator Tryer()
    {
        do
        {
            scoreList = dreamloLB.ToListHighToLow();

			if (scoreList == null) 
			{
				leaderboard.text = "(Cargando...)";
			} 
			else 
			{
                leaderboard.text = "";
				int maxToDisplay = 10;
				int count = 0;

				foreach (dreamloLeaderBoard.Score currentScore in scoreList)
				{
					count++;

                    leaderboard.text += count + ". Segundos: " + currentScore.seconds.ToString() + " - " + currentScore.playerName.Replace("+"," ") + "\n";

                    if (count >= maxToDisplay) 
                        break;
				}    
            }

            yield return new WaitForSeconds(3);

        }while(scoreList.Count==0);
    }
}
