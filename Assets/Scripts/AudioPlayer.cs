using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField] [Range(0f, 1f)] float shootingVolume;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0f, 1f)] float explosionVolume;

    // Make the audio player only instantiate once in whole app
    static AudioPlayer instance;

    void Awake() {
        // Manage singleton pattern
        ManageSingleTon();
    }

    void ManageSingleTon() {
        // If an audio player has already exist
        if(instance != null) {
            // Delete all the other audio player come up
            gameObject.SetActive(false);
            Destroy(gameObject); 
        }
        else { 
            // Set this object aas the one and only audio player
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void PlayShootingSFX() { PlayClip(shootingSFX, shootingVolume); }

    public void PlayExplosionSFX() { PlayClip(explosionSFX, explosionVolume); }

    void PlayClip(AudioClip audioClip, float volume) {
        if(audioClip != null) { AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume); }
    }
}
