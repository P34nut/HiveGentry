using TMPro;
using UnityEngine;

public class RoomDisplay : MonoBehaviour
{
    public Room Room;
    public Bar ProgressBar;

    protected bool pressed;

    void OnEnable()
    {
        GameManager.Instance.OnEnergyChanged += Refresh;
        GameManager.Instance.OnTaskAdded += Refresh;
        GameManager.Instance.OnTaskChanged += Refresh;
    }

    void OnDisable()
    {
        GameManager.Instance.OnEnergyChanged -= Refresh;
        GameManager.Instance.OnTaskAdded -= Refresh;
        GameManager.Instance.OnTaskChanged -= Refresh;
    }

    private void Update()
    {
        if (pressed)
            Room.TransferEnergy();

        /*Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);
        if (task != null && task.IsExecuted)
        {
            TimerLabel.text = Mathf.RoundToInt(task.SuccessTimer) + "/" + task.Duration;
        }
        else
        {
            TimerLabel.text = string.Empty;
        }*/
    }

    protected virtual void Refresh() { }
}