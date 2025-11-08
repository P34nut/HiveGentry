using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
public class Task : ScriptableObject
{
    [Header("Conditions")]
    public Room AffectedRoom;
    public float MinGameTime;
    [Range(0f, 100f)]
    public float Chance;
    [TextArea]
    public string StartSubtitles;
    public AudioClip StartClip;

    [Header("Progress")]
    public float Duration = 30f;
    [Range(0f, 100f)]
    public float NecessaryMinEnergy;
    [TextArea]
    public string ReminderSubtitles;
    public AudioClip ReminderClip;

    [Header("Runtime")]
    public bool IsExecuted;
    public float FailTimer;
    public float SuccessTimer;

    public void Init ()
    {
        IsExecuted = false;
        FailTimer = 0f;
        SuccessTimer = 0f;
    }

    public bool AreConditionsMet ()
    {
        return AffectedRoom.Energy >= NecessaryMinEnergy;
    }
}