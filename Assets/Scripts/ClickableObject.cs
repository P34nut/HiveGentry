using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public Task task;
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
        _collider.enabled = !task.IsExecuted && task.AreConditionsMet();
    }

    public void OnMouseDown()
    {
        if (task != null)
        {
            GameManager.Instance.ExecuteTask(task);
        }
    }
}