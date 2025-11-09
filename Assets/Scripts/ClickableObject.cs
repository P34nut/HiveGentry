using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public List<Task> Tasks;
    public MeshCollider _collider;
    public Animation Animation;

    [HideInInspector] public float XRotation;
    [HideInInspector] public bool isAnimating;

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
                StartCoroutine(DoAnimation(task));
                GameManager.Instance.ExecuteTask(task);
                return;
            }
        }
    }
    
    public virtual IEnumerator DoAnimation(Task task)
    {
        if (Animation != null)
        {
            Animation.Play();
            yield return new WaitForSeconds(task.Duration);
            Animation.Stop();
        }
        
        yield return null;
    }

    private void Update()
    {
        if (isAnimating)
        {
            transform.localEulerAngles = new Vector3(XRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}