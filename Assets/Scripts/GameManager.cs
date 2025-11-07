using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float EnergyTransferRate;
    public List<Room> Rooms;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        float energyPerRoom = 100f / Rooms.Count;
        foreach (Room room in Rooms)
        {
            room.Energy = energyPerRoom;
            room.Refresh();
        }
    }
    
    public void TransferEnergy (Room currentRoom)
    {
        currentRoom.Energy += EnergyTransferRate * Time.deltaTime;
        foreach (Room room in Rooms)
        {
            if (room == currentRoom) continue;
            room.Energy -= EnergyTransferRate * Time.deltaTime / (Rooms.Count - 1);
        }
    }
}
