using System.Collections;
using UnityEngine;

public class Lamp : ClickableObject
{
    public Light Light;

    void Awake()
    {
        Light.enabled = false;
    }
    
    public override IEnumerator DoAnimation(Task task)
    {
        Light.enabled = true;
        yield return new WaitForSeconds(task.Duration);
        Light.enabled = false;
    }

    public override void StopAnimation(Task task)
    {
        Light.enabled = false;
    }
}
