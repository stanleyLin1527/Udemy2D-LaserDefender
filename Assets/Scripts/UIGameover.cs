using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIGameover : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        scoreText.text = "your score : \n" + scoreKeeper.GetScore().ToString("00000000");
    }
}
