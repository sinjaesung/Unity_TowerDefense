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
        Debug.Log("WaveSpawner Awakeù awake����");
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
        //�����ϴ� Enemy��ü���� �ִٸ� �׵��ȿ� ��� ����κ��� �����ϰ�,��ü���� 0���Ϸ� �Ȱ�쿡�� ����κ�ó��.
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;//countdown�� 0���Ϸ� �Ǿ����� �ٽ� �� �ʱ�ȭ�ϰ�,�� ���� ����.
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
