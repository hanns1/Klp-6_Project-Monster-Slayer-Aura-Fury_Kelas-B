using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemyCount;
    }
    public List<Wave> waves;
    public int waveNumber;
    public Transform minPos;
    public Transform maxPos;

    void Update()
    {
        if (PlayerControler.Instance.gameObject.activeSelf) // Dikomentari: Cek PlayerController
        {
            waves[waveNumber].spawnTimer += Time.deltaTime; // Biarkan logika spawn jika ingin musuh muncul
            if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval){
                waves[waveNumber].spawnTimer = 0;
                SpawnEnemy();
            }
            if (waves[waveNumber].spawnedEnemyCount >= waves[waveNumber].enemiesPerWave){
                waves[waveNumber].spawnedEnemyCount = 0;
                if (waves[waveNumber].spawnInterval > 0.15f){
                    waves[waveNumber].spawnInterval *= 0.8f;
                }
                waveNumber++;
            }
            if (waveNumber >= waves.Count){
                waveNumber = 0;
            }

            // Untuk sementara, agar tidak error, kita bisa hanya melakukan spawn dasar tanpa kompleksitas wave
            // Ini akan memerlukan penyesuaian nilai 'spawnInterval' langsung di Inspector
            // jika Anda ingin spawn bekerja saat ini.
            // waves[0].spawnTimer += Time.deltaTime; // Asumsi selalu wave 0 jika waveNumber dikomentari
            // if (waves[0].spawnTimer >= waves[0].spawnInterval){
            //     waves[0].spawnTimer = 0;
            //     SpawnEnemy();
            // }
         }
    }

    private void SpawnEnemy(){
        Instantiate(waves[waveNumber].enemyPrefab, RandomSpawnPoint(), transform.rotation);
        // waves[waveNumber].spawnedEnemyCount++; // Dikomentari
    }

    private Vector2 RandomSpawnPoint(){
        Vector2 spawnPoint;
        if (Random.Range(0f, 1f) > 0.5){
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            if (Random.Range(0f, 1f) > 0.5){
                spawnPoint.y = minPos.position.y;
            } else {
                spawnPoint.y = maxPos.position.y;
            }
        } else {
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            if (Random.Range(0f, 1f) > 0.5){
                spawnPoint.x = minPos.position.x;
            } else {
                spawnPoint.x = maxPos.position.x;
            }
        }
        return spawnPoint;
    }
}