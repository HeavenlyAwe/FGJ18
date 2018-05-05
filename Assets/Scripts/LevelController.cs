using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public int level;

    public Sprite[] levelBitmaps;

    //private MovePlayer mp;

    bool paused;

	// Use this for initialization
	void Start () {
        level = 0;
        //mp = GameObject.FindGameObjectWithTag("Player").GetComponent<MovePlayer>();

        paused = false;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            paused = !paused;
            if (paused) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
	}

    public void SetPaused(bool paused) {
        this.paused = paused;
    }

    public void PauseGame() {

        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
    }

    public void ResumeGame() {
        SceneManager.UnloadSceneAsync("PauseScene");
    }
}
