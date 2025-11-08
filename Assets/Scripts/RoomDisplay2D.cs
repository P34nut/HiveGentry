using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomDisplay2D : RoomDisplay
{
    public TMP_Text TaskLabel;
    public Button ExecuteButton;
    
    protected override void Refresh()
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
