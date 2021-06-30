using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // TABLE AND CARDS
    [SerializeField] int totalCards = 24;
    int matches;

    [SerializeField] List<GameObject> cardList;
    [SerializeField] Transform table;
    [SerializeField] GameObject card;
    [SerializeField] List<Sprite> cardSprites;
    [SerializeField] Sprite backSprite;

    // MANAGING
    List<GameObject> flippedCards = new List<GameObject>();
    bool startedGame;
    int tryCount;
    bool firstTry;

    // COMPONENTS
    [SerializeField] Text matchesText;

    // TIMER
    float timeCount;
    [SerializeField] Text timerText;
    [SerializeField] bool isPlaying; // Turn to true when clicking the first card

    // PROPERTIES
    [SerializeField] GameObject blockPanel;

    void Start()
    {
        SetCards(totalCards);
        timerText.text = timeCount.ToString("F2");
        matches = totalCards / 2;
    }

    void Update()
    {
        if (firstTry && timeCount == 0)
        {
            StartGame();
        }

        if (isPlaying)
        {
            HandleTimer();
        }

        matchesText.text = "Pares faltantes: " + matches;

        if (matches < 1)
        {
            isPlaying = false;
        }

        if (!isPlaying && timeCount > 0)
        {
            StartCoroutine(EndGame());
        }
    }

    void StartGame()
    {
        isPlaying = true;
    }

    IEnumerator EndGame()
    {
        CrossSceneInformation.TimeCount = timeCount;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(2);
    }

    void HandleTimer()
    {
        timeCount += Time.deltaTime;
        timerText.text = timeCount.ToString("F2");
    }

    public void CheckFlippedCards()
    {
        if(!firstTry)
        {
            firstTry = true;
        }

        tryCount++;

        if(tryCount == 2)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                if (cardList[i].GetComponent<Card>().IsFlipped)
                {
                    flippedCards.Add(cardList[i]);
                }
            }

            StartCoroutine(CheckMatches(flippedCards[0], flippedCards[1]));
        }
    }

    IEnumerator CheckMatches(GameObject flippedCard1, GameObject flippedCard2)
    {
        blockPanel.SetActive(true);

        if (flippedCard1.transform.GetChild(1).GetComponent<Image>().sprite.name == flippedCard2.transform.GetChild(1).GetComponent<Image>().sprite.name)
        {
            matches--;
            
            yield return new WaitForSeconds(0.5f);

            flippedCard1.transform.GetChild(0).GetComponent<Image>().enabled = false;
            flippedCard1.transform.GetChild(1).GetComponent<Image>().enabled = false;

            flippedCard2.transform.GetChild(0).GetComponent<Image>().enabled = false;
            flippedCard2.transform.GetChild(1).GetComponent<Image>().enabled = false;

            flippedCard1.GetComponent<Card>().IsFlipped = false;
            flippedCard2.GetComponent<Card>().IsFlipped = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            flippedCard1.GetComponent<Card>().FlipCard();
            flippedCard2.GetComponent<Card>().FlipCard();
        }

        tryCount = 0;

        flippedCards.Clear();
        blockPanel.SetActive(false);

        yield return new WaitForSeconds(0.5f);
    }

    void SetCards(int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            var rand = UnityEngine.Random.Range(0, cardSprites.Count);

            // Create card and assign a number as ID
            GameObject cardInField = Instantiate(card);
            cardInField.name = "" + i;

            // Set back and front sprite of card
            cardInField.transform.GetChild(0).GetComponent<Image>().sprite = backSprite;
            cardInField.transform.GetChild(1).GetComponent<Image>().sprite = cardSprites[rand];

            // Set card to table as child
            cardInField.transform.SetParent(table, false);

            // Remove the selected front sprite to avoid not getting pairs
            cardSprites.RemoveAt(rand);

            // Add card gameobject to list
            cardList.Add(cardInField);
        }
    }
}