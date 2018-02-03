using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesFromMainScreen : MonoBehaviour {


    public void LoadCredits() {
        SceneManager.LoadScene("CreditsScene");
    }

    public void LoadInstructions() {
        SceneManager.LoadScene("InstructionsScene");
    }
}
