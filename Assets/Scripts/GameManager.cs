using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float EnergyTransferRate;
    public float TaskCheckTime;
    public List<Room> Rooms;
    public List<Task> Tasks;
    public TMP_Text SubtitleLabel;

    public UnityAction OnEnergyChanged;
    public UnityAction OnTaskAdded;
    public UnityAction OnTaskChanged;

    private float taskCheckTimer;
    public List<Task> CurrentTasks;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
        taskCheckTimer = 9f;
    }

    private void Update()
    {
        taskCheckTimer += Time.deltaTime;
        if (taskCheckTimer >= TaskCheckTime)
        {
            taskCheckTimer = 0f;
            CheckNextTask();
        }

        foreach (var task in CurrentTasks)
        {
            CheckTaskConditions(task);
        }
    } 

    private void Init()
    {
        float energyPerRoom = 100f / Rooms.Count;
        foreach (Room room in Rooms)
        {
            room.Energy = energyPerRoom;
        }
        OnEnergyChanged?.Invoke();
    }

    public void TransferEnergy(Room currentRoom)
    {
        if (currentRoom.Energy >= 100)
            return;
        
        currentRoom.Energy += EnergyTransferRate * Time.deltaTime;

        var activeRoomCounter = Rooms.Count(r => r != currentRoom && r.Energy > 0);

        foreach (Room room in Rooms)
        {
            if (room == currentRoom) continue;
            if (room.Energy <= 0) continue;

            room.Energy -= EnergyTransferRate * Time.deltaTime / activeRoomCounter;
            room.Energy = Mathf.Clamp(room.Energy, 0, 100);
        }

        OnEnergyChanged?.Invoke();
    }

    private void CheckNextTask()
    {
        foreach (var task in Tasks)
        {
            if (Random.Range(0f, 100f) > task.Chance) continue;

            CurrentTasks.Add(task);
            SubtitleLabel.text = task.Subtitles;

            OnTaskAdded?.Invoke();
            break;
        }
    }

    private void CheckTaskConditions (Task task)
    {
        if (!task.AreConditionsMet ())
        {
            task.IsExecuted = false;
            OnTaskChanged?.Invoke();
        }
    }
    
    public void ExecuteTask (Task task)
    {
        task.IsExecuted = true;
        OnTaskChanged?.Invoke();
    }
}
