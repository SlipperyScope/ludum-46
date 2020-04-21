using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoMainScene()
    {
        SceneManager.LoadScene("Adam");
    }
    public void GotoInstScene()
    {
        SceneManager.LoadScene("Inst");
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}