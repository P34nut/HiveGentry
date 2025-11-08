using System;
using UnityEngine;

public class RoomDisplay3D : RoomDisplay
{
    [SerializeField] private BoxCollider _collider;
    public override void Refresh()
    {
        Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);

        if (task != null)
        {
            Label.text = Mathf.RoundToInt(Room.Energy) + "/" + task.NecessaryMinEnergy + "%";
            //ExecuteButton.gameObject.SetActive(!task.IsExecuted && task.AreConditionsMet());
        }
    }

    private void OnMouseDown()
    {
        pressed = true;
    }

    private void OnMouseUp()
    {
        pressed = false;
    }
}
