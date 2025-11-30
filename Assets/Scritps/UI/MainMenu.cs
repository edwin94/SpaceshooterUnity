using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI textForMainMenu;
    public GameObject panelEntry;
    public GameObject panelInstruction;

    public void selectPlayer(int playerPosition)
    {
        int value = PlayerPrefs.GetInt("EnemiesDestroy") >= 10 * playerPosition ? 1 : 0;
        if (value == 0)
        {
            textForMainMenu.text = "to use this ammo you need to kill " + 10 * playerPosition + " enemies";
            return;
        }

        textForMainMenu.text = "Player selected: " + (playerPosition + 1);
        PlayerPrefs.SetInt("playerSpriteIndex", playerPosition);
    }

    public void selectAmmo(int ammoPosition)
    {
        int value = PlayerPrefs.GetInt("EnemiesDestroy") >= 10 * ammoPosition ? 1 : 0;
        if (value == 0)
        {
            textForMainMenu.text = "to use this ammo you need to kill " + 10 * ammoPosition + " enemies";
            return;
        }

        textForMainMenu.text = "Ammo selected: " + (ammoPosition + 1);
        PlayerPrefs.SetInt("ammoSpriteIndex", ammoPosition);
    }

    public void startGame(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene(level);
    }

    public void openInstructions()
    {
        panelInstruction.SetActive(true);
        panelEntry.SetActive(false);
    }

    public void closeInstructions()
    {
        panelEntry.SetActive(true);
        panelInstruction.SetActive(false);
    }

    public void resetProgress()
    {
        PlayerPrefs.SetInt("EnemiesDestroy", 0);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("playerSpriteIndex", 0);
        PlayerPrefs.SetInt("ammoSpriteIndex", 0);
    }
}
