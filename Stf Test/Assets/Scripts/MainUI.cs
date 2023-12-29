
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    
    public Text dropNumberText;
    public Text dropsPerSecondText;
    public Text bucketUpgradeText;
    public Text rainText;
    public Text cloudText;
    public Text collectText;
    public TMP_Text levelText;
    public Text LevelUpRequirement;

    public Main main;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateUI() //made it public, it only had void before
    {
        dropNumberText.text = " " + Math.Floor(main.drops);
        dropsPerSecondText.text = main.rainPower + "/sec";
        bucketUpgradeText.text = "Bucket Upgrade\n" + main.bucketUpgradePower + " / tap" + "\n Level: " + main.bucketUpgradePowerUpLevel;
        rainText.text = "Rain\n" + main.rainPower + " / sec" + "\n Level: " + main.rainPowerUpLevel;
        cloudText.text = "Cloud Drops" + "\nLimit: " + main.CloudDropLimit  + "\nRate: " + main.CloudDropRate  +"\n Level: " + main.cloudDropsPowerUpLevel;
        //collectText.text = "Collect:\n" + cloudDrops;
        collectText.text = "Collect:\n" + Math.Floor(main.CloudDrops).ToString();

        levelText.text = "Lv " + main.playerLevel; // Update the level text
        LevelUpRequirement.text = "FIRE! FILL UNTIL\n" + main.dropsRequiredForLevelUp;

        // Check power-up levels and update button interactability
        //bucketUpgradeButton.interactable = (playerLevel >= 2 && bucketUpgradePowerUpLevel >= 1);
        //rainButton.interactable = (playerLevel >= 3 && rainPowerUpLevel >= 1);
        //cloudButton.interactable = (playerLevel >= 4 && cloudDropsPowerUpLevel >= 1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("MainUI enabled and listening to sceneLoaded.");
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only proceed if we're in the Main scene
        if (scene.name == "Main")
        {
            // Find and update UI elements here as they exist in this scene
            // Find and update the drop number text
            Debug.Log("dropNumberText: " + dropNumberText);
            dropNumberText = GameObject.Find("DropNumber").GetComponent<Text>();
            dropNumberText.text = " " + Math.Floor(Main.Instance.drops);

            // Find and update the drops per second text
            Debug.Log("dropsPerSecondText: " + dropsPerSecondText);
            dropsPerSecondText = GameObject.Find("DropsPerSecondText").GetComponent<Text>();
            dropsPerSecondText.text = Main.Instance.rainPower + "/sec";

            // Find and update the bucket upgrade text
            Debug.Log("bucketUpgradeText: " + bucketUpgradeText);
            bucketUpgradeText = GameObject.Find("BucketUpgradeText").GetComponent<Text>();
            bucketUpgradeText.text = "Bucket Upgrade\n" + Main.Instance.bucketUpgradePower + " / tap" + "\n Level: " + Main.Instance.bucketUpgradePowerUpLevel;

            // Find and update the rain text
            Debug.Log("rainText: " + rainText);
            rainText = GameObject.Find("RainText").GetComponent<Text>();
            rainText.text = "Rain\n" + Main.Instance.rainPower + " / sec" + "\n Level: " + Main.Instance.rainPowerUpLevel;

            // Find and update the cloud text
            Debug.Log("cloudText: " + cloudText);
            cloudText = GameObject.Find("CloudText").GetComponent<Text>();
            cloudText.text = "Cloud Drops" + "\nLimit: " + Main.Instance.CloudDropLimit + "\nRate: " + Main.Instance.CloudDropRate + "\n Level: " + Main.Instance.cloudDropsPowerUpLevel;

            // Find and update the collect text
            Debug.Log("collectText: " + collectText);
            collectText = GameObject.Find("CollectText").GetComponent<Text>();
            collectText.text = "Collect:\n" + Math.Floor(Main.Instance.CloudDrops).ToString();

            // Find and update the level text
            Debug.Log("levelText: " + levelText);
            levelText = GameObject.Find("Text Level(TMP)").GetComponent<TMP_Text>();
            levelText.text = "Lv " + Main.Instance.playerLevel;

            // Find and update the level up requirement text
            Debug.Log("LevelUpRequirement: " + LevelUpRequirement);
            LevelUpRequirement = GameObject.Find("LevelUpRequirement").GetComponent<Text>();
            LevelUpRequirement.text = "FIRE! FILL UNTIL\n" + Main.Instance.dropsRequiredForLevelUp;

            //UpdateUI(); // Now call UpdateUI to refresh the UI elements
        }
    }

}
