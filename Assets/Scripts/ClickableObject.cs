using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public List<Task> Tasks;
    public MeshCollider _collider;

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

    void Awake()
    {
        _collider = GetComponent<MeshCollider>();
    }

    public void Refresh()
    {
        foreach (Task task in Tasks)
        {
            if (task.IsTaskActive ())
            {
                _collider.enabled = !task.IsExecuted && task.AreConditionsMet();
                return;
            }
        }
        _collider.enabled = false;
    }

    public void OnMouseDown()
    {
        foreach (Task task in Tasks)
        {
            if (task.IsTaskActive () && !task.IsExecuted && task.AreConditionsMet())
            {
                GameManager.Instance.ExecuteTask(task);
                return;
            }
        }
    }
}