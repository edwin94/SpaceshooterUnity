using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float widthImage;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float rest = (speed * Time.time) % widthImage;  // remaining way to get a new cycle
        transform.position = initialPosition + direction * rest;    // initial position adds the rest of the widht image remaining in wanted direction
    }
}
