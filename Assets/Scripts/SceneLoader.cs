using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string m_dungeonScene = "Dungeon";
    private string m_mainScene = "Main";

    public void LoadDungeonScene()
    {
        SceneManager.LoadScene(m_dungeonScene);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(m_mainScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
