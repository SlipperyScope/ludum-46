using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Adam");
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}