using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseNavigation : MonoBehaviour {

    public void ResumeGame() {
        SceneManager.UnloadSceneAsync("PauseScene");
    }

    public void LoadMainMenuScene() {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
    }
}
