using System;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float moveSpeed = 3.0f;
    private Vector2 clampLimits = new Vector2(4.7f, 6.3f);

    // Update is called once per frame
    void Update()
    {
        move();
    }

    // detect trigger on the player
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            selectPowerUp(player);
            Destroy(gameObject);
        }
    }

    // Move to the left screen
    private void move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        float xClamp = Mathf.Clamp(transform.position.x, -clampLimits.x, clampLimits.x);
        float yClamp = Mathf.Clamp(transform.position.y, -clampLimits.y, clampLimits.y);
        transform.position = new Vector3(xClamp, yClamp, 0);
    }

    private void selectPowerUp(Player player)
    {
        if (gameObject.name.StartsWith("PowerUpLife"))
        {
            increaseLife(player);
        }
        else if (gameObject.name.StartsWith("PowerUpRoF"))
        {
            increaseRoF(player);
        }
        else if (gameObject.name.StartsWith("PowerUpSpeed"))
        {
            increaseSpeed(player);
        }
        else if (gameObject.name.StartsWith("PowerUpDestroy"))
        {
            destroyAllEnemies();
        }
    }

    private void increaseRoF(Player player)
    {
        player.increaseRoFFunc();
    }

    private void increaseSpeed(Player player)
    {
        player.increaseSpeedFunc();
    }

    private void increaseLife(Player player)
    {
        if (player.Lifes < 3)
        {
            player.increaseLifes();
        }
    }

    private void destroyAllEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
