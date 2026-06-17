using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private GameObject powerUpCanvas;
    [SerializeField] private PowerUpCard[] cardUIList;

    [Header("UI Panels")]
    [SerializeField] private GameObject powerUpCardsPanel;
    [SerializeField] private GameObject chooseChamberPanel;
    [SerializeField] private ChooseChamber chooseChamberScript;

    [Header("PowerUps")]
    [SerializeField] private List<PowerUpData> allPowerUp;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private LevelBar levelBar;
    private HealthComponent playerHealth;
    private MovementComponent playerMovement;
    private PlayerMagazineSystem playerMagazine;
    private PlayerInput playerInput;

    private List<StatType> passivesUnlocked = new List<StatType>();
    private int bulletsObtained = 0;

    private Dictionary<StatType, float> currentModifiers = new Dictionary<StatType, float>();
    private int pendingLevelUp = 0;

    private void Awake()
    {
        foreach (StatType type in System.Enum.GetValues(typeof(StatType)))
        {
            currentModifiers[type] = 0;
        }

        playerHealth = player.GetComponent<HealthComponent>();
        playerMovement = player.GetComponent<MovementComponent>();
        playerMagazine = player.GetComponent<PlayerMagazineSystem>();
        playerInput = player.GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        if (levelBar != null) levelBar.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        if (levelBar != null) levelBar.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp()
    {
        pendingLevelUp++;

        if (pendingLevelUp == 1) OpenPowerUpMenu();
    }

    private void OpenPowerUpMenu()
    {
        playerInput.SwitchCurrentActionMap("UI");
        powerUpCardsPanel.SetActive(true);
        chooseChamberPanel.SetActive(false);

        List<PowerUpData> availablePowerUp = new List<PowerUpData>();

        //GESTIONE DEI POTENZIAMENTI DISPONIBILI
        foreach (PowerUpData powerUp in allPowerUp)
        {
            StatType stat = powerUp.Modifier.statType;
            float currentProgress = currentModifiers[stat];
            float levelCap = powerUp.Modifier.CapPercentage;

            //Level cap -> continuo subito se il powerUp ha raggiunto il massimo livello
            if (currentProgress >= levelCap) continue;

            //Bullets
            if (stat == StatType.Bullet)
            {
                if (bulletsObtained < 6)
                {
                    availablePowerUp.Add(powerUp);
                }
            }
            else
            {
                //PowerUp ottenuti
                if (passivesUnlocked.Count >= 3 && !passivesUnlocked.Contains(stat))
                {
                    continue;
                }

                availablePowerUp.Add(powerUp);
            }
        }

        if (availablePowerUp.Count == 0)
        {
            pendingLevelUp = 0;
            CloseMenu();
            return;
        }

        Time.timeScale = 0.0f;
        powerUpCanvas.SetActive(true);

        foreach (PowerUpCard card in cardUIList)
        {
            if (availablePowerUp.Count > 0)
            {
                card.gameObject.SetActive(true);

                int selection = Random.Range(0, availablePowerUp.Count);
                PowerUpData data = availablePowerUp[selection];
                card.SetupCard(data);
                availablePowerUp.RemoveAt(selection);
            }
            else
            {
                card.gameObject.SetActive(false);
            }
        }
    }

    public void ApplyPowerUp(PowerUpData data, float value)
    {
        StatType stat = data.Modifier.statType;
        if (stat == StatType.Bullet)
        {
            powerUpCardsPanel.SetActive(false);
            chooseChamberPanel.SetActive(true);

            chooseChamberScript.SetupMagazineMenu(data, playerMagazine, this);
            UpgradePlayer(stat, value);

            return;
        }
        else
        {
            if (!passivesUnlocked.Contains(stat))
            {
                passivesUnlocked.Add(stat);
            }


            float levelCap = data.Modifier.CapPercentage;
            float oldModifier = currentModifiers[stat];
            float newModifier = oldModifier + value;

            if (newModifier > levelCap)
            {
                value = levelCap - oldModifier;
                newModifier = levelCap;
            }
            currentModifiers[stat] = newModifier;
            UpgradePlayer(stat, value);
        }

        CheckPendingLevels();
    }

    private void CheckPendingLevels()
    {
        pendingLevelUp--;
        if (pendingLevelUp > 0)
        {
            powerUpCardsPanel.SetActive(true);
            chooseChamberPanel.SetActive(false);
            OpenPowerUpMenu();
        }
        else CloseMenu();
    }

    private void UpgradePlayer(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.Bullet:

                break;
            case StatType.Health:
                float bonusHealth = playerHealth.MaxHealth * value;
                playerHealth.SetMaxHealth(playerHealth.MaxHealth + bonusHealth);
                break;
            case StatType.MoveSpeed:
                float bonusSpeed = playerMovement.SpeedMultiplayer * value;
                playerMovement.SpeedMultiplayer = playerMovement.SpeedMultiplayer + bonusSpeed;
                break;
            case StatType.Damage:
                playerMagazine.UpgradeDamageMultiplier(value);
                break;

        }
    }

    public void OnChamberSelectionConfirmed()
    {
        bulletsObtained++;
        CheckPendingLevels();
    }

    private void CloseMenu()
    {
        playerInput.SwitchCurrentActionMap("Player");
        powerUpCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }



}
