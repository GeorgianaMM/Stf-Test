using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpUIManager : MonoBehaviour
{
    public Button ButtonForLevelUpBucket;
    public Button ButtonForLevelUpRain;
    public Button ButtonForLevelUpCloud;
    public Main main;
    
/*
    public void UpdateLevelUpOptionsUI() //changed to public
    {
        // Assuming you have variables to track if the player has unlocked/leveled up the power-ups
        //DON"T DELETE WITHOUT CHECKING
        //bool bucketUnlocked = bucketUpgradePowerUpLevel > 0;
        //bool rainUnlocked = rainPowerUpLevel > 0;
        //bool cloudUnlocked = cloudDropsPowerUpLevel > 0;

        // Hide all buttons initially
        ButtonForLevelUpBucket.gameObject.SetActive(false);
        ButtonForLevelUpRain.gameObject.SetActive(false);
        ButtonForLevelUpCloud.gameObject.SetActive(false);

        // Logic to determine which buttons to show based on the current level and power-up status
        if (main.playerLevel == 2)
        {
            // Only the bucket upgrade button is relevant
            ButtonForLevelUpBucket.gameObject.SetActive(true);
            SetButtonPosition(ButtonForLevelUpBucket.gameObject, ButtonPosition.Bottom);
            //OnUpgradeChoiceMade();
        }
        else if (main.playerLevel == 3)
        {
            // Both bucket and rain buttons are relevant
            ButtonForLevelUpBucket.gameObject.SetActive(true);
            ButtonForLevelUpRain.gameObject.SetActive(true);
            SetButtonPosition(ButtonForLevelUpBucket.gameObject, ButtonPosition.Middle);
            SetButtonPosition(ButtonForLevelUpRain.gameObject, ButtonPosition.Bottom);
            //OnUpgradeChoiceMade();
        }
        else if (main.playerLevel >= 4)
        {
            // Based on what has been unlocked, display and position buttons
            ButtonForLevelUpBucket.gameObject.SetActive(main.bucketUpgradePowerUpLevel > 0);
            ButtonForLevelUpRain.gameObject.SetActive(main.rainPowerUpLevel > 0);
            //cloudButton.gameObject.SetActive(true); // Cloud button is always available from level 4

            // Adjust positions based on which power-ups are unlocked
            int activeButtons = (main.bucketUpgradePowerUpLevel > 0 ? 1 : 0) + (main.rainPowerUpLevel > 0 ? 1 : 0) + 1;
            if (activeButtons == 3)
            {
                SetButtonPosition(ButtonForLevelUpBucket.gameObject, ButtonPosition.Top);
                SetButtonPosition(ButtonForLevelUpRain.gameObject, ButtonPosition.Middle);
                SetButtonPosition(ButtonForLevelUpCloud.gameObject, ButtonPosition.Bottom);
            }
            else if (activeButtons == 2)
            {
                SetButtonPosition(ButtonForLevelUpBucket.gameObject, main.rainPowerUpLevel > 0 ? ButtonPosition.Top : ButtonPosition.Middle);
                SetButtonPosition(ButtonForLevelUpRain.gameObject, ButtonPosition.Middle);
                SetButtonPosition(ButtonForLevelUpCloud.gameObject, ButtonPosition.Bottom);
            }
            else
            {
                // Only the cloud button is active
                SetButtonPosition(ButtonForLevelUpCloud.gameObject, ButtonPosition.Middle);
            }
            //OnUpgradeChoiceMade();
        }
        main.OnUpgradeChoiceMade();
        // ... (Additional logic for power-up choices and player level)
    }

    private void SetButtonPosition(GameObject button, ButtonPosition position)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        switch (position)
        {
            case ButtonPosition.Top:
                rectTransform.anchoredPosition = new Vector2(0, 100); // Example position
                break;
            case ButtonPosition.Middle:
                rectTransform.anchoredPosition = new Vector2(0, 0); // Example position
                break;
            case ButtonPosition.Bottom:
                rectTransform.anchoredPosition = new Vector2(0, -100); // Example position
                break;
        }
    }

    // Enum to define button positions
    private enum ButtonPosition
    {
        Top,
        Middle,
        Bottom
    }
*/
    public void BucketUpgradeClicked()
    {
        if (Main.Instance != null && Main.Instance.playerLevel >= 2 && Main.Instance.totalPowerUpsUpgradedInLevel < Main.Instance.playerLevel - 1)
        {
            Main.Instance.bucketUpgradePowerUpLevel++;
            Main.Instance.totalPowerUpsUpgradedInLevel++;
            Main.Instance.bucketUpgradePower += 1;
            // Make sure to save the state here if necessary
            SceneManager.LoadScene("Main");
        }
        else
        {
            Debug.LogError("Main instance is not set or the conditions are not met.");
        }
    }

    public void RainClicked()
    {
        if (main.playerLevel >= 3 && main.totalPowerUpsUpgradedInLevel < main.playerLevel - 1)
        {
            if (!main.isRainActive)
            {
                main.rainPowerUpLevel++;
                main.totalPowerUpsUpgradedInLevel++;
                main.rainPower += 5;
                main.isRainActive = false;
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void CloudClicked()
    {
        if (main.playerLevel >= 4 && main.totalPowerUpsUpgradedInLevel < main.playerLevel - 1)
        {
            main.cloudDropsPowerUpLevel++;
            main.totalPowerUpsUpgradedInLevel++;
            
            main.AdjustCloudDropsPowerUp();
            SceneManager.LoadScene("Main");
        }
    }
}
