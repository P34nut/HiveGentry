using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [TextArea]
    public string GameOverSubtitles;
    public AudioClip GameOverClip;
}
