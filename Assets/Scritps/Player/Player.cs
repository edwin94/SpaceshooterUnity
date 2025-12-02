using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // input
    private Vector2 m_moveAmt;
    private bool m_attackAmt;


    // configs
    [SerializeField] private float speed;
    [SerializeField] private float rateOfFire;
    [SerializeField] private Vector2 clampLimits = new Vector2(4.7f, 8.3f);
    [SerializeField] private Transform[] shotPosition;
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private GameObject lifesUi;
    [SerializeField] private AudioClip explosionAudioResource;
    [SerializeField] private GameObject explosion;

    // handlings
    private Vector3 initialPosition;
    private int currentBullet = 0;
    private float timerRoF = 0;
    private int lifes = 3;
    private float life = 100;
    private bool canBeDamaged = true;
    private bool canMove = true;
    private AudioSource audioSource;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public int Lifes
    {
        get { return lifes; }
        set { lifes = value; }
    }

    public int CurrentBullet
    {
        get { return currentBullet; }
        set { currentBullet = value; }
    }

    void Start()
    {
        initialPosition = transform.position;

        // get components
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        move();
        fire();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletEnemy") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            updateLife();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        m_moveAmt = ctx.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        // Attack logic here: The button is pressed.
        if (ctx.performed)
        {
            m_attackAmt = true;
        }

        // Reset or stop the attack logic here: The button was released.
        if (ctx.canceled)
        {
            m_attackAmt = false;
        }
    }

    // my methods
    private void updateLife()
    {
        if (!canBeDamaged)
            return;

        life -= 20;
        sliderLife.value = life / 100.0f;

        if (life <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            audioSource.PlayOneShot(explosionAudioResource);

            // remove life
            var icons = lifesUi.GetComponentsInChildren<Image>();
            icons[lifes - 1].enabled = false;
            lifes--;

            // reset player
            StartCoroutine(resetPlayer());
        }
    }

    public void increaseLifes()
    {
        lifes++;

        var icons = lifesUi.GetComponentsInChildren<Image>();
        icons[lifes - 1].enabled = true;
    }

    // move player and limit the translation to the size of the screen
    private void move()
    {
        if (!canMove)
            return;

        transform.Translate(m_moveAmt.normalized * speed * Time.deltaTime);
        float xClamp = Mathf.Clamp(transform.position.x, -clampLimits.x, clampLimits.x);
        float yClamp = Mathf.Clamp(transform.position.y, -clampLimits.y, clampLimits.y);
        transform.position = new Vector3(xClamp, yClamp, 0);
    }

    // fire bullets
    private void fire()
    {
        timerRoF += Time.deltaTime;

        if (!m_attackAmt || timerRoF < rateOfFire)
            return;

        foreach (Transform pos in shotPosition)
        {
            GameObject bulletObject = Instantiate(bullet[currentBullet], pos.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -90f));
            bulletObject.tag += tag;
        }
        timerRoF = 0;
    }

    IEnumerator resetPlayer()
    {
        // restore player to initial position
        life = 100;
        sliderLife.value = life;
        transform.position = initialPosition;

        canBeDamaged = false;
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.4f);

        if (lifes <= 0)
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().gameOver();
            Destroy(gameObject);
        }

        yield return new WaitForSeconds(5);
        canBeDamaged = true;
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 1.0f);
    }

    public void increaseRoFFunc()
    {
        StartCoroutine(increaseRoF());
    }

    IEnumerator increaseRoF()
    {
        float defaultRateOfFire = 0.5f;
        rateOfFire = 0.1f;
        yield return new WaitForSeconds(5);
        rateOfFire = defaultRateOfFire;
    }

    public void increaseSpeedFunc()
    {
        StartCoroutine(increaseSpeed());
    }

    IEnumerator increaseSpeed()
    {
        float defaultSpeed = 5.0f;
        speed = 10.0f;
        yield return new WaitForSeconds(5);
        speed = defaultSpeed;
    }

}
