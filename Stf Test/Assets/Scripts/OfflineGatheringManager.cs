using System;
using UnityEngine;

public class OfflineGatheringManager : MonoBehaviour
{
    public Main main;
    
    public void CalculateOfflineProgress()
    {
        if (!main.HasCalculatedOfflineProgress && main.LastOnlineTimestamp > 0 && main.playerLevel >= 4 && main.cloudDropsPowerUpLevel >= 1)
        {
            double currentTime = GetTimestamp();            
            double offlineDuration = currentTime - main.LastOnlineTimestamp;
            double offlineGatheredDrops = main.CloudDropRate * offlineDuration;

            double availableSpace = main.CloudDropLimit - main.CloudDrops;
            double dropsToAdd = Math.Min(offlineGatheredDrops, availableSpace);

            main.CloudDrops += dropsToAdd; 
            SaveLastOnlineTimestamp(); // Update the last online timestamp to the current time
            main.HasCalculatedOfflineProgress = true; // Set the flag so this doesn't run again
        }
    }

    public void SaveLastOnlineTimestamp()
    {
        double timestamp = GetTimestamp();
        PlayerPrefs.SetString("lastOnlineTimestamp", timestamp.ToString());
        PlayerPrefs.Save();
    }
    //Don't need this in Utility anymore
    public static double GetTimestamp()
    {
        return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
    }
    
}
