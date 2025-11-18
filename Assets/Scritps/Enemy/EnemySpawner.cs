using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textForSpawner;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] enemyBossPrefabs;
    [SerializeField] private float spawnYLimit = 2.27f;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private AudioClip audioClip;
    
    private float rateOfSpawn;
    private int amountOfEnemies;
    private float rateOfSpawnSets;
    private int amountOfEnemySets;
    private float rateOfLevels;
    private int amountOfLevels;

    public float RateOfSpawn
    {
        get { return rateOfSpawn; }
        set { rateOfSpawn = value; }
    }

    public int AmountOfEnemies
    {
        get { return amountOfEnemies; }
        set { amountOfEnemies = value; }
    }

    public float RateOfSpawnSets
    {
        get { return rateOfSpawnSets; }
        set { rateOfSpawnSets = value; }
    }

    public int AmountOfEnemySets
    {
        get { return amountOfEnemySets; }
        set { amountOfEnemySets = value; }
    }

    public float RateOfLevels
    {
        get { return rateOfLevels; }
        set { rateOfLevels = value; }
    }

    public int AmountOfLevels
    {
        get { return amountOfLevels; }
        set { amountOfLevels = value; }
    }

    public IEnumerator enemySpawnerSetEnumarator()
    {
        for (int k = 0; k < amountOfLevels; k++)
        {
            // Spawn of ordes
            for (int j = 0; j < amountOfEnemySets; j++)
            {
                // set info for the player
                textForSpawner.text = "Level " + (k + 1) + "/" + amountOfLevels + "\n" + "Set " + (j + 1) + "/" + amountOfEnemySets;


                // Create new position where the enemies should begin to move
                Vector3 position = new Vector3(transform.position.x, Random.Range(-spawnYLimit, spawnYLimit), transform.position.z);
                float amplitude = 2.0f;
                if (position.y < 0)
                {
                    amplitude = 4.0f + Mathf.Abs(position.y);
                }

                // Spawn enemy here
                for (int i = 0; i < amountOfEnemies; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[0], position, Quaternion.identity);
                    enemy.GetComponent<Enemy>().Amplitude = amplitude;
                    yield return new WaitForSeconds(rateOfSpawn);
                }

                // wait for the next set of enemies to spawn
                yield return new WaitForSeconds(rateOfSpawnSets);
            }

            // wait for the next level of enemies to spawn
            yield return new WaitForSeconds(rateOfLevels);
        }

        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>().clip = audioClip;
        GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(rateOfLevels);

        sliderLife.gameObject.SetActive(true);
        GameObject bossEnemy = Instantiate(enemyBossPrefabs[0], transform.position, Quaternion.identity);
        bossEnemy.GetComponent<Enemy>().IsBoss = true;
        bossEnemy.GetComponent<Enemy>().BossSliderLife = sliderLife;
    }
}
