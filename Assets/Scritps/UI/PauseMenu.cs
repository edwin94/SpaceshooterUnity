using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public void CallContinue()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().pauseCallBack();
    }

    public void CallMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void CallQuit()
    {
        Application.Quit();
    }
}
