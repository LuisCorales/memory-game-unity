using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // COMPONENTS
    Animator animator;
    GameController controller;

    // PROPERTIES
    public bool IsFlipped { get; set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameController>();
    }

    public void FlipCard()
    {
        IsFlipped = !IsFlipped;
        animator.SetBool("IsFlipped", IsFlipped);

        controller.TryCount++;

        if (controller.TryCount == 2)
        {
            controller.CheckFlippedCards();
        }
    }
}
