using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private TextMeshProUGUI showInfo;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private float rateOfSpawn = 1.0f;
    [SerializeField] private int amountOfEnemies = 5;
    [SerializeField] private float rateOfSpawnSets = 10.0f;
    [SerializeField] private int amountOfEnemySets = 5;
    [SerializeField] private float rateOfLevels = 20.0f;
    [SerializeField] private int amountOfLevels = 5;
    [SerializeField] private GameObject lifesUi;


    private int playerSpriteIndex = 0;
    public bool GameFinished = false;

    void Start()
    {
        setPlayer();
        startGame();
    }

    void Update()
    {
        if (GameFinished)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.Translate(Vector2.right * 5 * Time.deltaTime);
        }
    }

    private void setPlayer()
    {
        // get sprite for player
        playerSpriteIndex = PlayerPrefs.GetInt("playerSpriteIndex", 0);
        if (playerSpriteIndex < 0 || playerSpriteIndex >= playerSprites.Length)
        {
            playerSpriteIndex = 0;
        }
        int currentBullet = PlayerPrefs.GetInt("ammoSpriteIndex", 0);

        // set sprite on player
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            player.GetComponentInChildren<SpriteRenderer>().sprite = playerSprites[playerSpriteIndex];

            Image[] uiLifesImages = lifesUi.GetComponentsInChildren<Image>();
            foreach (Image image in uiLifesImages)
            {
                image.sprite = playerSprites[playerSpriteIndex];
            }

            player.GetComponent<Player>().CurrentBullet = currentBullet;
        }
    }

    private void startGame()
    {
        EnemySpawner enemySpawnerClass = enemySpawner.GetComponent<EnemySpawner>();
        enemySpawnerClass.RateOfSpawn = rateOfSpawn;
        enemySpawnerClass.AmountOfEnemies = amountOfEnemies;
        enemySpawnerClass.RateOfSpawnSets = rateOfSpawnSets;
        enemySpawnerClass.AmountOfEnemySets = amountOfEnemySets;
        enemySpawnerClass.RateOfLevels = rateOfLevels;
        enemySpawnerClass.AmountOfLevels = amountOfLevels;
        StartCoroutine(enemySpawnerClass.enemySpawnerSetEnumarator());
    }

    public void gameOver()
    {
        showInfo.text = "You lose, game over\nreturning to menu..";
        StartCoroutine(movingToMainMenu());
    }

    public void youWin()
    {
        GameFinished = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().CanMove = false;

        StartCoroutine(movingToNextLevel());
    }

    IEnumerator movingToNextLevel()
    {
        showInfo.text = "You win, moving to next level";
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level 02");
    }

    IEnumerator movingToMainMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main Menu");
    }
}
