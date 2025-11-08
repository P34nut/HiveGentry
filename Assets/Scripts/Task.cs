using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
public class Task : ScriptableObject
{
    [Header("Conditions")]
    public float MinGameTime;
    [Range(0f, 100f)]
    public float Chance;

    [Header("Progress")]
    public float Duration = 30f;
    [TextArea]
    public string Subtitles;

    [Header("Completion")]
    public Room AffectedRoom;
    [Range(0f, 100f)]
    public float NecessaryMinEnergy;

    [Header("Runtime")]
    public bool IsExecuted;

    public bool AreConditionsMet ()
    {
        return AffectedRoom.Energy >= NecessaryMinEnergy;
    }

}
