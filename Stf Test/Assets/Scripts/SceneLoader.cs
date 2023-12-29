using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //public Main main;
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        MainUI mainUI = FindObjectOfType<MainUI>();
        
        if (mainUI != null)
        {
            mainUI.enabled = false;
        }

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += OnMainSceneLoaded;
    }

    private void OnMainSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) // Assuming the Main scene is at build index 1
        {
            MainUI mainUI = FindObjectOfType<MainUI>();
            
            if (mainUI != null)
            {
                mainUI.enabled = true;
                mainUI.UpdateUI();
            }
            
            // Since we've done our job, unsubscribe from the event
            SceneManager.sceneLoaded -= OnMainSceneLoaded;
        }
    }


}
