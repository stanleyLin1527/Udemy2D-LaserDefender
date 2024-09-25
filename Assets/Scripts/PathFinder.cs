using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> wayPoints;
    int wayPointIdx = 0;

    void Awake() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIdx].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath() {
        if(wayPointIdx < wayPoints.Count) { 
            Vector3 targetPos = wayPoints[wayPointIdx].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if(transform.position == targetPos) { wayPointIdx++; }
        }
        else { Destroy(gameObject); }
    }
}
