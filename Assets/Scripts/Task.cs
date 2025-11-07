using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
public class Task : ScriptableObject
{
    [Header("Conditions")]
    public float MinGameTime;
    [Range (0f, 100f)]
    public float Chance;

    [Header ("Progress")]
    public float Duration = 30f;
    [TextArea]
    public string Subtitles;

    [Header ("Completion")]
    [Range(0f, 100f)]
    public float NecessaryMinEnergy;
}
