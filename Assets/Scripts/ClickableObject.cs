using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public List<Task> Tasks;
    public Collider _collider;
    public Animation Animation;

    [HideInInspector] public float XRotation;
    [HideInInspector] public bool isAnimating;

    private GameObject highlighter;

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
        _collider = GetComponent<Collider>();
    }

    void Start ()
    {
        highlighter = GameObject.CreatePrimitive(PrimitiveType.Cube);
        highlighter.transform.localScale = Vector3.one * 0.3f;
        highlighter.transform.position = transform.position + Vector3.up * 1.5f;
        highlighter.SetActive(false);
    }

    public void Refresh()
    {
        foreach (Task task in Tasks)
        {
            if (task.IsTaskActive ())
            {
                _collider.enabled = !task.IsExecuted && task.AreConditionsMet();
                highlighter.SetActive(_collider.enabled);

                if (!task.AreConditionsMet())
                {
                    StopAnimation(task);
                }
                return;
            }
        }
        _collider.enabled = false;
    }

    public virtual void StopAnimation(Task task)
    {
        if (isAnimating)
        {
            StopCoroutine(DoAnimation(task));
            Animation.Stop();
            isAnimating = false;
        }
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


        if (highlighter != null)
        {
            float speed = 2f * Mathf.PI;
            float amplitude = 1f;

            float offsetY = Mathf.Sin(Time.time * speed) * amplitude;

            Vector3 basePos = transform.position + Vector3.up * 1.5f;
            highlighter.transform.position = basePos + new Vector3(0, offsetY, 0);
        }
    }
}