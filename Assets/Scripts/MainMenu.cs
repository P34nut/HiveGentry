using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
# else
        Application.Quit ();
#endif
    }
    
    public void OnStartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
