using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;


    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() {
        // Set max value as player's initial health
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    void Update() {
        healthSlider.value = playerHealth.GetHealth();
        // The string in ToString() is to specify the format we want, 
        // here it shows the score with 8 0-digits at first and update these digits
        scoreText.text = scoreKeeper.GetScore().ToString("00000000");
    }
}
