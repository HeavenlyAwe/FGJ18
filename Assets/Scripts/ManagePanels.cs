using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePanels : MonoBehaviour {

    public RectTransform gamePanel;
    public RectTransform infoPanel;
    public RectTransform trapPanel;

    private float gamePanelWidth;
    private float gamePanelHeight;

	// Use this for initialization
	void Start () {
        gamePanelWidth = Screen.width - infoPanel.sizeDelta.x - trapPanel.sizeDelta.x;
        gamePanelHeight = Screen.height;

        if (gamePanelWidth > gamePanelHeight) {
            gamePanelWidth = gamePanelHeight;
        } else {
            gamePanelHeight = gamePanelWidth;
        }

        gamePanel.sizeDelta = new Vector2(gamePanelWidth, gamePanelHeight);
        Debug.Log(gamePanel.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
