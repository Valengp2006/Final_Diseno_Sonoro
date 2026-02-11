using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;
public class ChangeScene : MonoBehaviour
{
    public void SceneName(string sceneName)
    {
        MMSceneLoadingManager.LoadScene(sceneName);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
