using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void selectPlayer(int player)
    {
        PlayerPrefs.SetInt("playerSpriteIndex", player);
    }

    public void startGame(string level)
    {
        if (level.Equals(""))
        {
            level = "Level 01";
        }

        SceneManager.LoadScene(level);
    }
}
