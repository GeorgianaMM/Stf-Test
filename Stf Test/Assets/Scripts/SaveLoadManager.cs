using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(Main main)
    {
        PlayerPrefs.SetString("drops", main.drops.ToString());
        PlayerPrefs.SetString("rainPower", main.rainPower.ToString());
        PlayerPrefs.SetString("bucketUpgradePower", main.bucketUpgradePower.ToString());
        PlayerPrefs.SetInt("playerLevel", main.playerLevel);
        PlayerPrefs.SetInt("bucketUpgradePowerUpLevel", main.bucketUpgradePowerUpLevel);
        PlayerPrefs.SetInt("rainPowerUpLevel", main.rainPowerUpLevel);
        PlayerPrefs.SetInt("cloudDropsPowerUpLevel", main.cloudDropsPowerUpLevel);
        PlayerPrefs.SetInt("totalPowerUpsUpgradedInLevel", main.TotalPowerUpsUpgradedInLevel);
        PlayerPrefs.SetInt("initialDropsRequired", main.initialDropsRequired);
        PlayerPrefs.SetString("cloudDrops", main.CloudDrops.ToString());
        PlayerPrefs.SetString("cloudDropLimit", main.CloudDropLimit.ToString());
        PlayerPrefs.SetString("cloudDropRate", main.CloudDropRate.ToString());
        PlayerPrefs.SetString("timeSinceLastGathering", main.TimeSinceLastGathering.ToString());
        PlayerPrefs.SetString("lastOnlineTimestamp", main.LastOnlineTimestamp.ToString());

        PlayerPrefs.Save();
    }

    public void LoadGame(Main main)
    {
        
            main.drops = double.Parse(PlayerPrefs.GetString("drops", "0"));
            main.rainPower = double.Parse(PlayerPrefs.GetString("rainPower", "0"));
            main.bucketUpgradePower = double.Parse(PlayerPrefs.GetString("bucketUpgradePower", "1"));
            main.playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
            main.bucketUpgradePowerUpLevel = PlayerPrefs.GetInt("bucketUpgradePowerUpLevel", 0);
            main.rainPowerUpLevel = PlayerPrefs.GetInt("rainPowerUpLevel", 0);
            main.cloudDropsPowerUpLevel = PlayerPrefs.GetInt("cloudDropsPowerUpLevel", 0);
            main.TotalPowerUpsUpgradedInLevel = PlayerPrefs.GetInt("totalPowerUpsUpgradedInLevel", 0);
            main.initialDropsRequired = PlayerPrefs.GetInt("initialDropsRequired", 15); // CHANGE HERE BACK TO 85 AFTER YOU ARE DONE TESTING
            main.CloudDrops = double.Parse(PlayerPrefs.GetString("cloudDrops", "0"));
            main.CloudDropLimit = int.Parse(PlayerPrefs.GetString("cloudDropLimit", "100"));
            main.CloudDropRate = int.Parse(PlayerPrefs.GetString("cloudDropRate", "1"));
            main.TimeSinceLastGathering = double.Parse(PlayerPrefs.GetString("timeSinceLastGathering", "0"));
            main.LastOnlineTimestamp = double.Parse(PlayerPrefs.GetString("lastOnlineTimestamp", DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString()));

    }

}
