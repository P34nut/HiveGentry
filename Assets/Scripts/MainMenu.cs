using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject Overlay;
    public TMP_Text text1;
    public TMP_Text text2;

    public void OnQuitClicked()
    {
#if UNITY_WEBGL
            
#endif
            
            
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
# else
         Application.OpenURL("about:blank");
#endif
    }

    public void OnStartGame()
    {
        StartCoroutine(StartGameEnumerator());
    }
    
    private IEnumerator StartGameEnumerator ()
    {
        Overlay.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        text1.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        text2.gameObject.SetActive(true);

        text2.text = "Hive Gentry is connected...";
yield return new WaitForSeconds(1f);
        text2.text = "Hive entryG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "ive entHryG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "evi entHryG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "evei ntHryG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "everi ntHyG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "everyi ntHG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "everytHi nG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "everytHinG is connected...";
yield return new WaitForSeconds(0.3f);
        text2.text = "everytHinG is connected.";
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
