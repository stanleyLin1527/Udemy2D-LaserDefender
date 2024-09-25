using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] float lifetime;
    [SerializeField] float baseFiringRate;
    [SerializeField] float firingRateVar;
    [SerializeField] float minFiringRate;

    // Shooter is used by enemy or not
    [Header("AI")]
    [SerializeField] bool useAI;

    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;

    AudioPlayer audioPlayer;

    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start() {
        if(useAI) { isFiring = true; }
    }

    void Update() {
        Fire();
    }

    void Fire() {
        if(isFiring && fireCoroutine == null) { fireCoroutine = StartCoroutine(FireContinuously()); }
        else if(!isFiring && fireCoroutine != null) { 
            StopCoroutine(fireCoroutine); 
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinuously(){
        while(true) {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // For player, "up" is upward, but for enemies, "up" is downward
            // TryGetComponent() can GetComponent() and check if object is null or not at the same time
            if(instance.TryGetComponent<Rigidbody2D>(out var rigidbody)) { rigidbody.velocity = transform.up * projectileSpeed; }

            // Destroy instance after its lifetime
            Destroy(instance, lifetime);
            
            float firingRate = Random.Range(baseFiringRate - firingRateVar, baseFiringRate + firingRateVar);

            audioPlayer.PlayShootingSFX();

            // Wait a few second to operate next line
            yield return new WaitForSeconds(Mathf.Clamp(firingRate, minFiringRate, float.MaxValue));
        }
    }
}
