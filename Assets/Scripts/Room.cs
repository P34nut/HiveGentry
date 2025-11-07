using UnityEngine;
using System;
using TMPro;

public class Room : MonoBehaviour
{
    public string Name;
    public float Energy;
    public TMP_Text Label;

    private bool pressed;

    private void Update()
    {
        if (pressed) TransferEnergy();
    } 

    public void Refresh()
    {
        Label.text = Name + ": " + Mathf.RoundToInt(Energy) + "%";
    }

    public void OnPointerDown()
    {
        pressed = true;
    }
    
    public void OnPointerUp ()
    {
        pressed = false;
    }

    public void TransferEnergy()
    {
        GameManager.Instance.TransferEnergy(this);
        Debug.Log("Transfer energy to " + Name);
    }
}
