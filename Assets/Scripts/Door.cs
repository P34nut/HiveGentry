using System;
using System.Collections;
using UnityEngine;

public class Door : ClickableObject
{
   public Animation Animation;

   public float YPosition;
   public bool isAnimating;
   
   public override IEnumerator DoAnimation(Task task)
   {
      Debug.Log("Door Open");
      Animation.Play("DoorOpen");

      yield return new WaitForSeconds(task.Duration);
      
      Debug.Log("Door Closed");
      Animation.Play("DoorClosed");
   }

   private void Update()
   {
      if (isAnimating)
      {
         transform.position = new Vector3(transform.position.x, YPosition, transform.position.z);
      }
   }
}
