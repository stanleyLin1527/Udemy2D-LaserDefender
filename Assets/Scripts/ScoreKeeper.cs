using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreKeeper : MonoBehaviour
{
    int score;
    static ScoreKeeper instance;

    void Awake() {
        ManageSingleTon();
    }

    void ManageSingleTon() {
        if (instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() { return score; }
    public void AddScore(int score) { 
        this.score += score;
        Mathf.Clamp(score, 0, int.MaxValue);
    }
    public void ResetScore() { score = 0; }
}
