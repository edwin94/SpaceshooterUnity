using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip audioClip;

    private float ttl = 3.0f;
    private bool goesLeft;

    public bool GoesLeft
    {
        get { return goesLeft; }
        set { goesLeft = value; }
    }

    void Start()
    {
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, ttl);
    }

    void Update()
    {
        Vector2 direction = goesLeft ? Vector2.left : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
