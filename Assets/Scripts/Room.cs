using UnityEngine;
using System;
using TMPro;

public class Room : MonoBehaviour
{
    public string Name;
    public float Energy;
    public TMP_Text Label;

    public void Refresh ()
    {
        Label.text = Name + ": " + Energy + "%";
    }

    public void TransferEnergy()
    {
        GameManager.Instance.TransferEnergy(this);
    }
}
