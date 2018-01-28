using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public GameObject win;
    public GameObject lose;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void restartGame()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void newGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void winGame()
    {
        win.SetActive(true);
        lose.SetActive(false);
    }

    public void loseGame()
    {
        win.SetActive(false);
        lose.SetActive(true);
    }
}
