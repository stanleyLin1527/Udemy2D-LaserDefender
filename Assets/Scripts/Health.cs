using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health;
    [SerializeField] int score;
    [SerializeField] ParticleSystem hitFX;
    [SerializeField] bool applyShaking;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D otherOBJ) {
        // TryGetComponent() can GetComponent() and check if object is null or not at the same time
        if (otherOBJ.TryGetComponent<DamageDealer>(out var damageDealer)) {
            TakeDamage(damageDealer.GetDamage());
            PlayHitFX();
            ShakeCamera();
            // damage dealer get hit and destroyed
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage) {
        health -= damage;
        audioPlayer.PlayExplosionSFX();
        if(health <= 0) { 
            if(!isPlayer) { scoreKeeper.AddScore(score); }
            else { levelManager.LoadGameOver(); }
            Destroy(gameObject); 
        }
    }

    void PlayHitFX() {
        if(hitFX != null) {
            // Particle system needs to be "Play on awake" when using this method
            ParticleSystem instance = Instantiate(hitFX, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() { if(cameraShake != null && applyShaking) { cameraShake.Play(); } }

    public int GetHealth() { return health; }
}
