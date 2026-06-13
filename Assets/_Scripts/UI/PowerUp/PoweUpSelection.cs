using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoweUpSelection : MonoBehaviour
{
    [SerializeField] private PowerUpCard[] cardUIList;
    [SerializeField] private List<PowerUpData> allPowerUp;

    private void OnEnable()
    {
        List<PowerUpData> availablePowerUp = new List<PowerUpData>(allPowerUp);

        foreach (PowerUpCard card in cardUIList)
        {
            int selection = Random.Range(0, availablePowerUp.Count);
            PowerUpData data = availablePowerUp[selection];
            card.SetupCard(data);
            availablePowerUp.RemoveAt(selection);
        }
    }
}
