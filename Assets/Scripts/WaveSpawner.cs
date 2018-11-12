using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;
    
    public float timeBetweenWaves = 5f;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public Wave[] waves;

    private float countdown = 2.5f;
    private int waveIndex = 0;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!!!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
