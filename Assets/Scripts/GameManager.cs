using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    private void Awake ()
    {
        Instance = this;
    }
}
