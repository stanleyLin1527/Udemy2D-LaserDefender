using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    WaveConfigSO currentWave;
    [SerializeField] float wavesInterval;
    [SerializeField] bool isLooping = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() { return currentWave; }

    IEnumerator SpawnEnemyWaves() {
        do{
            foreach(WaveConfigSO wave in waveConfigs) {
                currentWave = wave;

                for(int j = 0; j < currentWave.GetEnemyCount(); j++) {
                    Instantiate(
                    // Which enemy
                    currentWave.GetEnemyPrefab(j),
                    // Spawn at where
                    currentWave.GetStartingWayPoint().position,
                    // Because we want enemy's "up" is downward, we rotate the sprite in unity and rotate the object here
                    Quaternion.Euler(0, 0, 180),
                    // This object's parent is who
                    transform);
                    
                    // Wait a few second to spawn next enemy
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                // Wait a few second to generate next wave
                yield return new WaitForSeconds(wavesInterval);
            }
        }while(isLooping);
    }
}
