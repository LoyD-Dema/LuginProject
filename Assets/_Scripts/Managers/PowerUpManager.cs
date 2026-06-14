using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private GameObject powerUpCanvas;
    [SerializeField] private PowerUpCard[] cardUIList;

    [Header("PowerUps")]
    [SerializeField] private List<PowerUpData> allPowerUp;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private LevelBar levelBar;
    private HealthComponent playerHealth;
    private MovementComponent playerMovement;
    private PlayerMagazineSystem playerMagazine;

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
        Time.timeScale = 0.0f;
        powerUpCanvas.SetActive(true);

        List<PowerUpData> availablePowerUp = new List<PowerUpData>();

        //GESTIONE DEI POTENZIAMENTI DISPONIBILI
        foreach (PowerUpData powerUp in allPowerUp)
        {
            StatType stat = powerUp.Modifier.statType;
            float currentProgress = currentModifiers[stat];
            float levelCap = powerUp.Modifier.CapPercentage;


            //Level cap
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
            CloseMenu();
            return;
        }

        foreach (PowerUpCard card in cardUIList)
        {
            if (availablePowerUp.Count == 0) break;

            int selection = Random.Range(0, availablePowerUp.Count);
            PowerUpData data = availablePowerUp[selection];
            card.SetupCard(data);
            availablePowerUp.RemoveAt(selection);
        }

    }

    public void ApplyPowerUp(PowerUpData data, float value)
    {
        StatType stat = data.Modifier.statType;
        if (stat == StatType.Bullet)
        {
            bulletsObtained++;
            //Gestire il menu del bullet
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

        pendingLevelUp--;
        if (pendingLevelUp > 0) OpenPowerUpMenu();
        else CloseMenu();
    }

    private void UpgradePlayer(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.Bullet:
                // logica del bullet
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

    public void OnPowerUpSelected()
    {
        pendingLevelUp--;

        if (pendingLevelUp > 0) OpenPowerUpMenu();
        else
        {
            powerUpCanvas.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    private void CloseMenu()
    {
        powerUpCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }



}
