using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _ambientSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _ambientClip;

    [System.Serializable]
    public class RoomStem
    {
        public Room room;
        public AudioClip stemClip;
        public float energyThreshold = 50f;
        [HideInInspector] public AudioSource stemSource;
        [HideInInspector] public bool isPlaying;
    }

    [Header("Room Stems")]
    public List<RoomStem> roomStems = new List<RoomStem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeAudio();
    }

    private void Update()
    {
        UpdateStems();
    }

    private void InitializeAudio()
    {
        // Setup ambient audio source
        if (_ambientSource == null)
        {
            _ambientSource = gameObject.AddComponent<AudioSource>();
        }

        _ambientSource.clip = _ambientClip;
        _ambientSource.loop = true;
        _ambientSource.volume = 1f;
        _ambientSource.Play();

        // Setup stem audio sources
        foreach (var roomStem in roomStems)
        {
            roomStem.stemSource = gameObject.AddComponent<AudioSource>();
            roomStem.stemSource.clip = roomStem.stemClip;
            roomStem.stemSource.loop = true;
            roomStem.stemSource.volume = 0f;
            roomStem.stemSource.Play();
            roomStem.isPlaying = false;
        }

        // Sync all audio sources
        SyncAudioSources();
    }

    private void SyncAudioSources()
    {
        if (_ambientSource == null || !_ambientSource.isPlaying)
            return;

        float currentTime = _ambientSource.time;

        foreach (var roomStem in roomStems)
        {
            if (roomStem.stemSource != null)
            {
                roomStem.stemSource.time = currentTime;
            }
        }
    }

    private void UpdateStems()
    {
        foreach (var roomStem in roomStems)
        {
            if (roomStem.room == null || roomStem.stemSource == null)
                continue;

            bool shouldPlay = roomStem.room.Energy >= roomStem.energyThreshold;

            if (shouldPlay && !roomStem.isPlaying)
            {
                // Fade in stem
                roomStem.isPlaying = true;
                StartCoroutine(FadeVolume(roomStem.stemSource, 0f, 1f, 0.5f));
            }
            else if (!shouldPlay && roomStem.isPlaying)
            {
                // Fade out stem
                roomStem.isPlaying = false;
                StartCoroutine(FadeVolume(roomStem.stemSource, 1f, 0f, 0.5f));
            }
        }
    }

    private System.Collections.IEnumerator FadeVolume(AudioSource source, float startVolume, float endVolume, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            yield return null;
        }

        source.volume = endVolume;
    }
}
