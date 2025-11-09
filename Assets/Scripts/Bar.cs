using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [Header("UI Elemente")]
    public RectTransform requiredBar;
    public RectTransform currentBar;

    [Header("Energieeinstellungen")]
    [Range(0, 100)] public float requiredEnergyPercent = 50f;
    [Range(0, 100)] public float currentEnergyPercent = 0f;
    public bool showRequiredEnergy;
    public bool showExecuted;
    public bool showTask;
    public float maxWidth = 200f; // Maximalbreite der Balken in Pixeln

    [Header("Zielobjekt (wird aktiviert/deaktiviert)")]
    public GameObject targetObject; // z. B. ein Portal oder Generator
    public GameObject executedObject;
    public GameObject taskObject;

    private float timer;

    void Update()
    {
        // Balkenbreiten berechnen
        float requiredWidth = (requiredEnergyPercent / 100f) * maxWidth;
        float currentWidth = (currentEnergyPercent / 100f) * maxWidth;

        // Balken anpassen (X-Skalierung)
        requiredBar.gameObject.SetActive(showRequiredEnergy);

        if (requiredBar != null)
            requiredBar.sizeDelta = new Vector2(requiredWidth, requiredBar.sizeDelta.y);

        if (currentBar != null)
            currentBar.sizeDelta = new Vector2(currentWidth, currentBar.sizeDelta.y);

        // Check, ob genügend Energie vorhanden ist
        if (targetObject != null)
        {
            bool enoughEnergy = currentEnergyPercent >= requiredEnergyPercent;
            targetObject.SetActive(enoughEnergy);
        }

        executedObject.SetActive(showExecuted);
        
        if (showRequiredEnergy)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0f;
                taskObject.SetActive(!taskObject.activeSelf);
            }
        } else
        {
            taskObject.SetActive(false);
        }
    }
}
