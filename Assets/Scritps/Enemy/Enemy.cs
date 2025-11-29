using UnityEngine;
using UnityEngine.UI;

public enum BossState
{
    MovingLeft,
    MovingUp,
    MovingDown
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float rateOfFire = 1f;
    [SerializeField] private Transform[] shotPosition;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private AudioClip explosionAudioResource;
    [SerializeField] private GameObject explosion;
    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject[] itemPowerUps;
    private Vector2 clampLimits = new Vector2(4.7f, 7.0f);
    private BossState bossState = BossState.MovingLeft;
    private int bossHit = 0;
    private Slider bossSliderLife;

    private AudioSource audioSource;
    private float startY;
    private float time;
    private float timerRoF;
    private int currentBullet = 0;

    public float Amplitude
    {
        get { return amplitude; }
        set { amplitude = value; }
    }

    public bool IsBoss
    {
        get { return isBoss; }
        set { isBoss = value; }
    }

    public Slider BossSliderLife
    {
        get { return bossSliderLife; }
        set { bossSliderLife = value; }
    }


    void Start()
    {
        startY = transform.position.y;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        move();
        fire();
        cleanUp();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer"))
        {
            Destroy(collision.gameObject);

            if (isBoss && bossHit < 10)
            {
                bossHit++;
                bossSliderLife.value = 1.0f - ((float)bossHit / 10.0f);
                return;
            }

            if (isBoss)
            {
                Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().youWin();
            }

            // create power up
            if (Random.value >= 0.8f)
            {
                Random.Range(0, itemPowerUps.Length);
                Instantiate(itemPowerUps[Random.Range(0, itemPowerUps.Length)], transform.position, Quaternion.identity);
            }

            // use this to unlock new shots
            PlayerPrefs.SetInt("EnemiesDestroy", PlayerPrefs.GetInt("EnemiesDestroy") + 1);

            audioSource.PlayOneShot(explosionAudioResource);
            Destroy(gameObject);
        }
    }

    private void move()
    {
        if (isBoss)
        {
            switch (bossState)
            {
                case BossState.MovingLeft:
                    // Move Left
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

                    // Check Left Limit
                    if (transform.position.x <= clampLimits.y)
                    {
                        // Once the limit is hit, start moving up
                        bossState = BossState.MovingUp;
                    }
                    break;

                case BossState.MovingUp:
                    // Move Up
                    transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

                    // Check Top Limit
                    if (transform.position.y >= clampLimits.x)
                    {
                        // Once the top is hit, start moving down
                        bossState = BossState.MovingDown;
                    }
                    break;

                case BossState.MovingDown:
                    // Move Down
                    transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

                    // Check Bottom Limit
                    if (transform.position.y <= -clampLimits.x)
                    {
                        bossState = BossState.MovingUp;
                    }
                    break;
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            // Vertical Oscillation (Sine Wave)
            // Increment time based on the game time
            time += Time.deltaTime * frequency;

            // Calculate the new Y position using a sine wave
            // Mathf.Sin(time) returns a value between -1 and 1
            // Multiplying by amplitude controls the height of the movement
            float newY = startY + (Mathf.Sin(time) * amplitude);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z); // Apply the new position. We keep the current X and Z, and use the new Y.
        }
    }

    private void fire()
    {
        timerRoF += Time.deltaTime;

        if (timerRoF < rateOfFire)
            return;

        foreach (Transform pos in shotPosition)
        {
            GameObject bulletObject = Instantiate(bullet[currentBullet], pos.transform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().GoesLeft = true;
            bulletObject.tag += tag;
        }
        timerRoF = 0;
    }

    private void cleanUp()
    {
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
