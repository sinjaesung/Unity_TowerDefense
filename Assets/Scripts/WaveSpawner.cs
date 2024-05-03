using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 2f;
    private float countdown = 2f;

    public TextMeshProUGUI waveCountdownText;

    public GameManager gameManager;

    private int waveIndex = 0;

    private void Awake()
    {
        Debug.Log("WaveSpawner Awake첫 awake시점");
        EnemiesAlive = 0;
    }
    private void Update()
    {
        Debug.Log("WaveSpawner| EnemiesAlive:" + EnemiesAlive);
        if(EnemiesAlive > 0)
        {
            return;
        }
        if (waveIndex >= waves.Length)
        {
            //if (EnemiesAlive <= 0)
            //{
            gameManager.WinLevel();
            this.enabled = false;
            //}
        }
        //존재하는 Enemy개체수가 있다면 그동안엔 모든 실행부분은 무시하고,개체수가 0이하로 된경우에만 실행부분처리.
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;//countdown이 0이하로 되었을때 다시 값 초기화하고,적 생성 실행.
        }

        Debug.Log("WaveSpawner|" + "waveIndex:"+ waveIndex+",countdown:" + countdown + ",EnemiesAlive:" + EnemiesAlive);
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    
    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;

        //if(waveIndex <= waves.Length-1)
        //{
        Wave wave = waves[waveIndex];

        if (wave != null)
        {
            EnemiesAlive = wave.count;

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
        //}
     
        waveIndex++;
        Debug.Log("now waveindex and EnemiesAlive:" + waveIndex + "," + EnemiesAlive);
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}
