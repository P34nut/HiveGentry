using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnRestartClicked()
    {
        SceneManager.LoadScene(1);
    }
}
