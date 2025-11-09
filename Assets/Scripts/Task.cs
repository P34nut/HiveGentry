using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "Task")]
public class Task : ScriptableObject
{
    [Header("Conditions")]
    public Room AffectedRoom;
    public Character AffectedCharacter;
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
    [TextArea]
    public string LastReminderSubtitles;
    public AudioClip LastReminderClip;

    [Header("Runtime")]
    public bool IsExecuted;
    public float FailTimer;
    public int FailStrikes;
    public float SuccessTimer;

    public void Init ()
    {
        IsExecuted = false;
        FailTimer = 0f;
        FailStrikes = 0;
        SuccessTimer = 0f;
    }

    public bool AreConditionsMet()
    {
        if (!IsTaskActive ()) return false;
        return AffectedRoom.Energy >= NecessaryMinEnergy;
    }
    
    public bool IsTaskActive ()
    {
        return GameManager.Instance.CurrentTasks.Contains(this);
    }
}