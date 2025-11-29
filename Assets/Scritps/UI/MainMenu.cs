using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI textForMainMenu;

    public void selectPlayer(int playerPosition)
    {
        int value = PlayerPrefs.GetInt("EnemiesDestroy") == 10 * playerPosition ? 1 : 0;
        if (value == 0)
        {
            textForMainMenu.text = "to use this ammo you need to kill " + 10 * playerPosition + " enemies";
            return;
        }

        PlayerPrefs.SetInt("playerSpriteIndex", playerPosition);
    }

    public void startGame(string level)
    {
        if (level.Equals(""))
        {
            level = "Level 01";
        }

        SceneManager.LoadScene(level);
    }

    public void selectAmmo(int ammoPosition)
    {
        int value = PlayerPrefs.GetInt("EnemiesDestroy") == 10 * ammoPosition ? 1 : 0;
        if (value == 0)
        {
            textForMainMenu.text = "to use this ammo you need to kill " + 10 * ammoPosition + " enemies";
            return;
        }

        PlayerPrefs.SetInt("ammoSpriteIndex", ammoPosition);
    }
}
