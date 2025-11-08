using UnityEngine;

public class CamMovement : MonoBehaviour
{
    void Update()
    {
        float movement = Time.deltaTime * 5f;

        if (Input.mousePosition.x <= 10f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + movement);
        }
        else if (Input.mousePosition.x >= Screen.width - 10f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - movement);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - movement);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + movement);
        }

        if (transform.localPosition.z >= 10f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 10f);
        }
        
        if (transform.localPosition.z <= -5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -5f);
        }
    }
}
