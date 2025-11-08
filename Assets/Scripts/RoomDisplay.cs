using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomDisplay : MonoBehaviour
{
    public Room Room;
    public TMP_Text Label;
    public TMP_Text TaskLabel;
    public TMP_Text TimerLabel;
    public Button ExecuteButton;

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

        Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);
        if (task != null && task.IsExecuted)
        {
            TimerLabel.text = Mathf.RoundToInt(task.SuccessTimer) + "/" + task.Duration;
        } else
        {
            TimerLabel.text = string.Empty;
        }
    }

    public virtual void Refresh()
    {
        Label.text = Room.name + ": " + Mathf.RoundToInt(Room.Energy) + "%";

        Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);
        TaskLabel.text = string.Empty;
        ExecuteButton.gameObject.SetActive(false);
        if (task != null)
        {
            TaskLabel.text = ">" + task.NecessaryMinEnergy;
            ExecuteButton.gameObject.SetActive(!task.IsExecuted && task.AreConditionsMet());
        }
    }

    public void OnPointerDown()
    {
        pressed = true;
    }

    public void OnPointerUp()
    {
        pressed = false;
    }

    public void OnClickExecute ()
    {
        Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);
        if (task != null)
        {
            GameManager.Instance.ExecuteTask(task);
        }
    }
}
