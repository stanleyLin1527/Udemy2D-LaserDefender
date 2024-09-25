using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    float SceneLoadDelay = 2;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadMainMenu() { SceneManager.LoadScene(0); }
    public void LoadGame() { 
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1); 
    }
    public void LoadGameOver() { StartCoroutine(WaitAndLoad(2, SceneLoadDelay)); }
    public void Quit() {
        Debug.Log("Quitting Game.......");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(int SceneIdx, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneIdx);
    }
}
