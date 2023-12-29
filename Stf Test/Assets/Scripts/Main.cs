using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Main : MonoBehaviour
{
    //UI Text
    

    //UI Buttons
    public Button bucketUpgradeButton;
    public Button rainButton;
    public Button cloudButton;
    public Button collectButton;

    //Game variables
    public double drops;
    public double rainPower; // dropsPerSeconds
    public double bucketUpgradePower; 

    // Cloud Drops variables
    private double cloudDrops;
    private int cloudDropLimit = 100; // Initial limit for drops in the cloud // I changed it from int to double
    private int cloudDropRate = 1;   // Initial rate at which drops are gathered per second // I changed it from int to double
    private double gatheringInterval = 1; // Time interval for gathering in seconds
    
    // Levels
    public int bucketUpgradePowerUpLevel = 0;
    public int rainPowerUpLevel = 0;
    public int cloudDropsPowerUpLevel = 0;
    public int totalPowerUpsUpgradedInLevel = 0;
    public int playerLevel = 1;
    public int initialDropsRequired = 15; // CHANGE HERE BACK TO 85 AFTER YOU ARE DONE TESTING
    public int levelIncreaseAmount = 20;
    public int dropsRequiredForLevelUp;

    public bool isRainActive = false;
    private bool hasCalculatedOfflineProgress = false; // for collecting drops offline
    private double timeSinceLastGathering; // Time since the last gathering
    private double lastOnlineTimestamp;
    private Vector2 initialSwipePos;
    
    // Other
    //public string levelUpSceneName = "LevelUpAnimation"; //name of animation scene
    //public OfflineGatheringManager offlineGatheringManager;
    public SceneLoader sceneLoader;
    public MainUI mainUI;
    public LevelUpUIManager levelUpUIManager;

    // Public properties
    public int TotalPowerUpsUpgradedInLevel
    {
        get { return totalPowerUpsUpgradedInLevel; }
        set { totalPowerUpsUpgradedInLevel = value; }
    }

    public double CloudDrops
    {
        get {return cloudDrops; }
        set {cloudDrops = value; }
    }

    public int CloudDropLimit
    {
        get {return cloudDropLimit; }
        set {cloudDropLimit = value; }
    }

    public int CloudDropRate
    {
        get {return cloudDropRate; }
        set {cloudDropRate = value; }
    }

    public double TimeSinceLastGathering
    {
        get {return timeSinceLastGathering; }
        set {timeSinceLastGathering = value; }
    }

    public double LastOnlineTimestamp
    {
        get {return lastOnlineTimestamp; }
        set {lastOnlineTimestamp = value; }
    }

    public bool HasCalculatedOfflineProgress
    {
        get {return hasCalculatedOfflineProgress; }
        set {hasCalculatedOfflineProgress = value; }
    }



//===================================================================================================================
//===================================================================================================================


    public static Main Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("Main Awake is being called.");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Main Instance is set and will not be destroyed on load.");
        }
        else if (Instance != this)
        {
            Debug.Log("Another instance of Main found, which will be destroyed.");
            Destroy(gameObject);
        }
    }



//===================================================================================================================
//===================================================================================================================
    void Start()
    {   
        //PlayerPrefs.DeleteAll();
        //Debug.Log("global initialDropsRequired: " + initialDropsRequired);


        initialDropsRequired = 15;    // CHANGE HERE BACK TO 85 AFTER YOU ARE DONE TESTING
        InvokeRepeating("IncrementDrops", 1.0f, 1.0f); // Calls IncrementDrops every 1 second.
        InvokeRepeating("Save", 1.0f, 1.0f); // Calls IncrementDrops every 1 second.
        SaveLoadManager.Instance.LoadGame(this); //Load
        CalculateOfflineProgress(); // Calculate any offline progress
        mainUI.UpdateUI();
        //levelUpUIManager.UpdateLevelUpOptionsUI();
    }

    public void Save()
    {
        SaveLoadManager.Instance.SaveGame(this);
        
    }
    //for collecting drops offline
    void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus) // Game is being paused
        {
            SaveLastOnlineTimestamp();
        }
    }

    void OnApplicationQuit()
    {
        SaveLastOnlineTimestamp();
    }

//===================================================================================================================
//===================================================================================================================

    public void CalculateOfflineProgress()
    {
        if (!hasCalculatedOfflineProgress && lastOnlineTimestamp > 0 && playerLevel >= 4 && cloudDropsPowerUpLevel >= 1)
        {
            double currentTime = GetTimestamp();            
            double offlineDuration = currentTime - lastOnlineTimestamp;
            double offlineGatheredDrops = cloudDropRate * offlineDuration;

            double availableSpace = cloudDropLimit - cloudDrops;
            double dropsToAdd = Math.Min(offlineGatheredDrops, availableSpace);

            cloudDrops += dropsToAdd; 
            SaveLastOnlineTimestamp(); // Update the last online timestamp to the current time
            hasCalculatedOfflineProgress = true; // Set the flag so this doesn't run again
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
   
//===================================================================================================================
//===================================================================================================================
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll(); // This clears all PlayerPrefs data.
        
        drops = 0;
        rainPower = 0;
        bucketUpgradePower = 1;

        initialDropsRequired = 15; // CHANGE HERE BACK TO 85 AFTER YOU ARE DONE TESTING
        playerLevel = 1;
        bucketUpgradePowerUpLevel = 0;
        rainPowerUpLevel = 0;
        cloudDropsPowerUpLevel = 0;
        totalPowerUpsUpgradedInLevel = 0;

        cloudDrops = 0;
        cloudDropLimit = 100;
        cloudDropRate = 1;
        timeSinceLastGathering = 0;

        Save();
        AdjustCloudDropsPowerUp();
       
    }
/*
    void UpdateUI() //made it public, it only had void before
    {
        dropsRequiredForLevelUp = initialDropsRequired + (levelIncreaseAmount * (playerLevel - 1));
        //dropsRequiredForLevelUp = Mathf.RoundToInt(initialDropsRequired * Mathf.Pow(levelIncreaseAmount, playerLevel - 1));
        //Debug.Log("initialDropsRequired: " + initialDropsRequired + "\ndropsRequiredForLevelUp: " + dropsRequiredForLevelUp);

        dropNumberText.text = " " + Math.Floor(drops);
        dropsPerSecondText.text = rainPower + "/sec";
        bucketUpgradeText.text = "Bucket Upgrade\n" + bucketUpgradePower + " / tap" + "\n Level: " + bucketUpgradePowerUpLevel;
        rainText.text = "Rain\n" + rainPower + " / sec" + "\n Level: " + rainPowerUpLevel;
        cloudText.text = "Cloud Drops" + "\nLimit: " + cloudDropLimit  + "\nRate: " + cloudDropRate  +"\n Level: " + cloudDropsPowerUpLevel;
        //collectText.text = "Collect:\n" + cloudDrops;
        collectText.text = "Collect:\n" + Math.Floor(cloudDrops).ToString();

        levelText.text = "Lv " + playerLevel; // Update the level text
        LevelUpRequirement.text = "FIRE! FILL UNTIL\n" + dropsRequiredForLevelUp;

        // Check power-up levels and update button interactability
        //bucketUpgradeButton.interactable = (playerLevel >= 2 && bucketUpgradePowerUpLevel >= 1);
        //rainButton.interactable = (playerLevel >= 3 && rainPowerUpLevel >= 1);
        //cloudButton.interactable = (playerLevel >= 4 && cloudDropsPowerUpLevel >= 1);
    }
*/
    void Update()
    {
        dropsRequiredForLevelUp = initialDropsRequired + (levelIncreaseAmount * (playerLevel - 1));
        //dropsRequiredForLevelUp = Mathf.RoundToInt(initialDropsRequired * Mathf.Pow(levelIncreaseAmount, playerLevel - 1));
        //Debug.Log("initialDropsRequired: " + initialDropsRequired + "\ndropsRequiredForLevelUp: " + dropsRequiredForLevelUp);
        mainUI.UpdateUI();

        if (Input.GetMouseButtonDown(0))
        {
            initialSwipePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 swipeDelta = initialSwipePos - (Vector2)Input.mousePosition;

            // Check if the swipe is downwards and has a minimum distance
            if (swipeDelta.y > 50 && Mathf.Abs(swipeDelta.x) < 50)
            {
                TryLevelUp();
            }
        }

        if (playerLevel >= 4 && cloudDropsPowerUpLevel >= 1)
        {
            // Update the time since the last gathering
            timeSinceLastGathering += Time.deltaTime;

            // Check if it's time to gather drops
            if (timeSinceLastGathering >= gatheringInterval)
            {
                GatherDrops();
                timeSinceLastGathering = 0; // Reset the timer
            }
        }
        
    }

    void TryLevelUp() //made it public, it only had void before
    {
        if (drops >= dropsRequiredForLevelUp)
        {
            drops -= dropsRequiredForLevelUp;
            playerLevel++;

            mainUI.UpdateUI(); // Update the UI after leveling up
            StartCoroutine(LevelUpAnimation());
        }
        // You can add an else statement here for additional feedback if the player doesn't have enough drops
    }

    // Call this before loading a new scene
    public void DisableMainUI()
    {
        if (mainUI != null)
        {
            mainUI.enabled = false;  // This assumes mainUI is a component that can be enabled/disabled.
        }
    }

    // Call this when returning to the main scene
    public void EnableMainUI()
    {
        if (mainUI != null)
        {
            mainUI.enabled = true;
            mainUI.UpdateUI();  // Update the UI now that we're back in the main scene.
        }
    }

    public IEnumerator LevelUpAnimation()
    {
        // Disable the MainUI before loading the level up animation scene
        DisableMainUI();

        // Load the level-up scene
        SceneManager.LoadScene("LevelUpAnimation", LoadSceneMode.Additive);

        // Wait for the animation to complete
        yield return new WaitForSeconds(1.55f);

        // Load the level-up upgrade scene
        SceneManager.LoadScene("LevelUpUpgrade");
    }

    public void OnUpgradeChoiceMade()
    {
        // Save state if necessary
        SaveLoadManager.Instance.SaveGame(this);

        // Load the main scene and enable MainUI
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name == "Main")
            {
                EnableMainUI();
            }
        };
    }
    


//===================================================================================================================
//===================================================================================================================
    //Main Bucket button
    public void Clicked()
    {
        drops += bucketUpgradePower;
    }

    //Level Up Bucket Upgrade power-up
    public void BucketUpgradeClicked()
    {
        
    }

    //Level Up Rain power-up
    public void RainClicked()
    {
        
    }
    //For auto-incrementation per seconds in Start()
    //Connected to Rain power-up
    private void IncrementDrops()
    {
        drops += rainPower;
    }

    //Level Up Cloud power-up
    public void CloudClicked()
    {
        
    }

    
    // For Cloud Drop functionality; All below
    //For the button that collects the drops in Cloud
    public void CollectFromCloud()
    {
        // Collect all drops in the cloud
        drops += cloudDrops;
        cloudDrops = 0; // Reset the drops in the cloud after collection  
    }
//===================================================================================================================
//===================================================================================================================

    void GatherDrops()
    {
        //Debug.Log("Gathering drops. Power-up level: " + cloudDropsPowerUpLevel);
        if (cloudDropsPowerUpLevel >= 1)
        {
            double gatheredDrops = cloudDropRate * gatheringInterval;
            cloudDrops = Math.Min(cloudDrops + gatheredDrops, cloudDropLimit);
            //cloudDrops = Math.Floor(cloudDrops);
        }

    }

    public void AdjustCloudDropsPowerUp() // made public
    {
        // Define base values and growth factors
        double baseLimit = 100; // Initial limit   // I changed it from int to double
        double baseRate = 1;    // Initial rate    // I changed it from int to double
        double growthFactor = 1.5; // Adjust as needed

        // Use the formula to calculate new values
        cloudDropLimit = (int)(baseLimit * Math.Pow(growthFactor, cloudDropsPowerUpLevel));
        cloudDropRate = (int)(baseRate * Math.Pow(growthFactor, cloudDropsPowerUpLevel));
    } 
}
