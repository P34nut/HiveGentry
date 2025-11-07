using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
public class Task : ScriptableObject
{
    public float Duration;
    [TextArea]
    public string Subtitles;

    [Header ("Completion")]
    [Range(0f, 100f)]
    public float NecessaryMinEnergy;
}
