using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float EnergyTransferRate;
    public float TaskCheckTime;
    public float TaskReminderTime;
    public int MaxTaskFails = 3;
    public List<Room> Rooms;
    public List<Task> Tasks;
    public TMP_Text SubtitleLabel;
    public AudioSource VoiceSource;

    [Header ("Runtime")]
    public List<Task> CurrentTasks;
    public float GameTimer;

    public UnityAction OnEnergyChanged;
    public UnityAction OnTaskAdded;
    public UnityAction OnTaskChanged;

    private float taskCheckTimer;
    private List<IEnumerator> progress = new();

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
        GameTimer += Time.deltaTime;
        taskCheckTimer += Time.deltaTime;
        if (taskCheckTimer >= TaskCheckTime)
        {
            taskCheckTimer = 0f;
            CheckNextTask();
        }

        if (progress.Count > 0)
        {
            if (!progress[0].MoveNext())
            {
                progress.RemoveAt(0);
            }
        }

        for (int i = CurrentTasks.Count - 1; i >= 0; i--)
        {
            Task task = CurrentTasks[i];
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

        foreach (Task task in Tasks)
        {
            task.Init();
        }
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
        List<Task> randomizedTasks = RandomizeList(Tasks);
        foreach (var task in randomizedTasks)
        {
            if (CurrentTasks.Contains(task)) continue;
            if (task.MinGameTime > GameTimer) continue;
            if (Random.Range(0f, 100f) > task.Chance) continue;
            IEnumerator enumerator = NewTaskEnumerator(task);
            progress.Add(enumerator);
            break;
        }
    }
    
    private List<T> RandomizeList<T>(List<T> originalList)
    {
        List<T> newList = new(originalList);
        for (int i = newList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            T temp = newList[i];
            newList[i] = newList[randomIndex];
            newList[randomIndex] = temp;
        }
        return newList;
    }

    private void CheckTaskConditions (Task task)
    {
        if (task.IsExecuted)
        {
            task.SuccessTimer += Time.deltaTime;
            if (task.SuccessTimer > task.Duration)
            {
                CurrentTasks.Remove(task);
                OnTaskChanged?.Invoke();
            }

            if (!task.AreConditionsMet())
            {
                task.IsExecuted = false;

                IEnumerator enumerator = RemindTaskEnumerator(task);
                progress.Add(enumerator);
                task.FailTimer = 0f;

                OnTaskChanged?.Invoke();
            }
        }
        else
        {
            task.FailTimer += Time.deltaTime;
            if (task.FailTimer > TaskReminderTime)
            {
                IEnumerator enumerator = RemindTaskEnumerator(task);
                progress.Add(enumerator);
                task.FailTimer = 0f;
            }
        }
    }

    public void ExecuteTask(Task task)
    {
        task.IsExecuted = true;
        OnTaskChanged?.Invoke();
    }
    
    private IEnumerator NewTaskEnumerator(Task task)
    {
        yield return null;

        int maxTasks = Mathf.Clamp(Mathf.CeilToInt(GameTimer * 0.05f), 1, 4);
        if (CurrentTasks.Count >= maxTasks) yield break;

        float requiredOverallEnergy = 0f;
        foreach (Task currentTask in CurrentTasks)
        {
            requiredOverallEnergy += currentTask.NecessaryMinEnergy;
        }
        if (requiredOverallEnergy + task.NecessaryMinEnergy > 50 + GameTimer * 0.25f) yield break;
        if (CurrentTasks.Exists(obj => obj.AffectedRoom == task.AffectedRoom)) yield break;
        if (CurrentTasks.Exists(obj => obj.AffectedCharacter == task.AffectedCharacter)) yield break;

        task.Init();
        CurrentTasks.Add(task);
        SubtitleLabel.text = task.StartSubtitles;
        if (task.StartClip != null) VoiceSource.PlayOneShot(task.StartClip, task.AffectedCharacter.SpeakVolume);

        OnTaskAdded?.Invoke();

        float waitTimer = 0f;
        while (waitTimer <= 4f)
        {
            waitTimer += Time.deltaTime;
            yield return null;
        }

        SubtitleLabel.text = string.Empty;
    }
    
    private IEnumerator RemindTaskEnumerator (Task task)
    {
        yield return null;
        if (task.IsExecuted || !CurrentTasks.Contains(task)) yield break;

        task.FailStrikes++;
        if (task.FailStrikes >= MaxTaskFails)
        {
            SubtitleLabel.text = task.AffectedCharacter.GameOverSubtitles;
            VoiceSource.PlayOneShot(task.AffectedCharacter.GameOverClip, task.AffectedCharacter.SpeakVolume);
        }
        else if (task.FailStrikes == MaxTaskFails - 1)
        {
            SubtitleLabel.text = task.ReminderSubtitles;
            if (task.ReminderClip != null) VoiceSource.PlayOneShot(task.ReminderClip, task.AffectedCharacter.SpeakVolume);
        }
        else
        {
            SubtitleLabel.text = task.ReminderSubtitles;
            if (task.ReminderClip != null) VoiceSource.PlayOneShot(task.ReminderClip, task.AffectedCharacter.SpeakVolume);
        }
        
        float waitTimer = 0f;
        while (waitTimer <= 4f)
        {
            waitTimer += Time.deltaTime;
            yield return null;
        }

        SubtitleLabel.text = string.Empty;
        
        if (task.FailStrikes >= MaxTaskFails)
        {
        waitTimer = 0f;
        while (waitTimer <= 3f)
        {
            waitTimer += Time.deltaTime;
            yield return null;
        }
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }
}
