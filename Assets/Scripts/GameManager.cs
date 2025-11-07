using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public float EnergyTransferRate;
    public float TaskCheckTime;
    public List<Room> Rooms;
    public List<Task> Tasks;

    private float taskCheckTimer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        taskCheckTimer += Time.deltaTime;
        if (taskCheckTimer >= TaskCheckTime)
        {
            taskCheckTimer = 0f;
            CheckTask();
        }
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

    public void TransferEnergy(Room currentRoom)
    {
        currentRoom.Energy += EnergyTransferRate * Time.deltaTime;
        currentRoom.Refresh();
        foreach (Room room in Rooms)
        {
            if (room == currentRoom) continue;
            room.Energy -= EnergyTransferRate * Time.deltaTime / (Rooms.Count - 1);
            room.Refresh();
        }
    }
    
    public void CheckTask ()
    {
        
    }
}
