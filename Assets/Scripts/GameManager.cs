using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float EnergyTransferRate;
    public float TaskCheckTime;
    public List<Room> Rooms;
    public List<Task> Tasks;
    public TMP_Text SubtitleLabel;

    private float taskCheckTimer;
    private Task currentTask;

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
            CheckNextTask();
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
        if (currentRoom.Energy >= 100)
            return;
        
        currentRoom.Energy += EnergyTransferRate * Time.deltaTime;
        currentRoom.Refresh();

        var activeRoomCounter = Rooms.Count(r => r != currentRoom && r.Energy > 0);
        
        foreach (Room room in Rooms)
        {
            if (room == currentRoom) continue;
            if (room.Energy <= 0) continue;
            
            room.Energy -= EnergyTransferRate * Time.deltaTime / activeRoomCounter;
            room.Energy = Mathf.Clamp(room.Energy, 0, 100);
            room.Refresh();
        }
    }
    
    public void CheckNextTask ()
    {
        foreach (var task in Tasks)
        {
            if (Random.Range(0f, 100f) > task.Chance) continue;

            currentTask = task;
            SubtitleLabel.text = currentTask.Subtitles;
            Rooms[currentTask.RoomIndex].Refresh();
            break;
        }
    }
}
