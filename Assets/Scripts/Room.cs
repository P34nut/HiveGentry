using UnityEngine;


[CreateAssetMenu(fileName = "Room", menuName = "Room")]
public class Room : ScriptableObject
{
    public float Energy;

    public void TransferEnergy()
    {
        GameManager.Instance.TransferEnergy(this);
    }
}
