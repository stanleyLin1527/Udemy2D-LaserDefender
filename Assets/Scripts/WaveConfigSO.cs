using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed;
    [SerializeField] float enemySpawningInterval;
    [SerializeField] float spawnTimeVariant;
    [SerializeField] float minSpawnTime;

    public int GetEnemyCount() { return enemyPrefabs.Count; }

    public GameObject GetEnemyPrefab(int idx) {return enemyPrefabs[idx]; }

    public Transform GetStartingWayPoint() { return pathPrefab.GetChild(0); }

    public List<Transform> GetWayPoints() {
        List<Transform> wayPoints = new();
        foreach (Transform child in pathPrefab) { wayPoints.Add(child); }
        return wayPoints;
    }

    public float GetMoveSpeed() { return moveSpeed; }

    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(enemySpawningInterval - spawnTimeVariant, 
                                        enemySpawningInterval + spawnTimeVariant);
        // Fix the spawn time between min and max
        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }
}
