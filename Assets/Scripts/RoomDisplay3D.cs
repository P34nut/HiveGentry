using System;
using UnityEngine;

public class RoomDisplay3D : RoomDisplay
{
    [SerializeField] private MeshCollider _collider;
    public MeshCollider _doorCollider;
    protected override void Refresh()
    {
        ProgressBar.currentEnergyPercent = Room.Energy;
        //Label.text = Mathf.RoundToInt(Room.Energy) + "%";
        Task task = GameManager.Instance.CurrentTasks.Find(obj => obj.AffectedRoom == Room);

        ProgressBar.showRequiredEnergy = task != null;
        if (task != null)
        {
            ProgressBar.requiredEnergyPercent = task.NecessaryMinEnergy;
            ProgressBar.showExecuted = task.IsExecuted;
            //Label.text = Mathf.RoundToInt(Room.Energy) + "/" + task.NecessaryMinEnergy + "%";
            //ExecuteButton.gameObject.SetActive(!task.IsExecuted && task.AreConditionsMet());
        } else
        {
            ProgressBar.showExecuted = false;
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
