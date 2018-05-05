using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenLayout : MonoBehaviour {

	public Canvas canvas;

	public GameObject bombSlotPrefab;

	public GameObject bombPrefab;


	public Sprite mazeSpriteTest;


	private float widthTrapPanel;
	private float widthMazePanel;
	private float widthInstructionPanel;


	// Use this for initialization
	void Start () {
		widthTrapPanel = (0.25f * Screen.width)/Screen.width;
		widthMazePanel = (0.50f * Screen.width)/Screen.width;
		widthInstructionPanel = (0.25f * Screen.width)/Screen.width;

		Debug.Log(Screen.width + " : " + Screen.height);
		Debug.Log(widthTrapPanel + " : " + widthMazePanel + " : " + widthInstructionPanel);

		GameObject trapPanel = AddPanel("Trap Panel", 0, widthTrapPanel, Color.red);
		GameObject instructionPanel = AddPanel("Instruction Panel", widthTrapPanel+widthMazePanel, widthInstructionPanel, Color.green);

		Rect rectMazePanel = new Rect();
		rectMazePanel.x = widthTrapPanel;
		rectMazePanel.y = 0;
		rectMazePanel.width = widthMazePanel;
		rectMazePanel.height = 1.0f;

		Debug.Log(rectMazePanel);

		Sprite loadedMazeSprite = LoadMazeFromBitmap(mazeSpriteTest);

		AddMaze(loadedMazeSprite, rectMazePanel);
	}

	private GameObject AddPanel(string name, float posX, float width, Color color) {
		GameObject panel = new GameObject(name);
		panel.AddComponent<CanvasRenderer>();
		Image i = panel.AddComponent<Image>();
		i.color = new Color(color.r, color.g, color.b, 0.5f);
		panel.transform.SetParent(canvas.transform, false);

		RectTransform rectMazePanel = panel.GetComponent<RectTransform>();
		// Coordinates relative to normalized space
		rectMazePanel.anchorMin = new Vector2(posX, 0);
		rectMazePanel.anchorMax = new Vector2(posX+width, 1.0f);
		rectMazePanel.offsetMin = new Vector2(0, 0);
		rectMazePanel.offsetMax = new Vector2(0, 0);

		// // Coordinates relative to world space
		// rectMazePanel.anchorMin = new Vector2(0, 0);
		// rectMazePanel.anchorMax = new Vector2(0, 0);
		// rectMazePanel.offsetMin = new Vector2(posX, 0);
		// rectMazePanel.offsetMax = new Vector2(posX + width, Screen.height);

		return panel;
	}

	private void AddMaze(Sprite mazeSprite, Rect boundingBox) {
		GameObject maze = new GameObject("Maze");
		maze.AddComponent<CanvasRenderer>();
		Image image = maze.AddComponent<Image>();
		image.sprite = mazeSprite;
		maze.transform.SetParent(canvas.transform, false);

		float size = Mathf.Min(boundingBox.width, boundingBox.height);
		float scale = boundingBox.width/(float)boundingBox.height;

		float xMin = (boundingBox.width - size)/2 + boundingBox.x;
		float xMax = xMin + size;
		float yMin = (boundingBox.height - size) /2 + boundingBox.y;
		float yMax = yMin + size;

		RectTransform rect = maze.GetComponent<RectTransform>();
		rect.anchorMin = new Vector2(xMin, yMin);
		rect.anchorMax = new Vector2(xMax, yMax);
		rect.pivot = new Vector2(0, 0);
		// rect.offsetMin = new Vector2((boundingBox.width - size)/2 + boundingBox.x,
		// 							 (boundingBox.height - size)/2 + boundingBox.y);
		// rect.sizeDelta = new Vector2(size, size);
		// Coordinates relative to normalized space
		// rectMazePanel.anchorMin = new Vector2(posX, 0);
		// rectMazePanel.anchorMax = new Vector2(posX+width, 1.0f);
		// rectMazePanel.offsetMin = new Vector2(0, 0);
		// rectMazePanel.offsetMax = new Vector2(0, 0);

		// Coordinates relative to world space
		// rectMazePanel.anchorMin = new Vector2(0, 0);
		// rectMazePanel.anchorMax = new Vector2(0, 0);
		// rectMazePanel.offsetMin = new Vector2(0, 0);
		// rectMazePanel.offsetMax = new Vector2(0, 0);
	}

	public Sprite LoadMazeFromBitmap(Sprite maze) {

        Texture2D tex = maze.texture;

        int w = tex.width;
        int h = tex.height;

        Color32[] c = tex.GetPixels32();

        Texture2D texture = new Texture2D(w, h);

        for (int x = 0; x < w; x++) {
            for (int y = 0; y < h; y++) {

                int index = (int)(c[x + w * y].r / 16);

                if (index < 2) {
                    // c[x + w * y] = new Color32(255, 255, 255, 255);
                    texture.SetPixel(x, y, Color.white);
                } else {
                    // c[x + w * y] = new Color32(0, 0, 0, 0);
                    texture.SetPixel(x, y, new Color(0, 0, 0, 0));
                }

            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        return Sprite.Create(texture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
	}

	// // Update is called once per frame
	// void Update () {
		
	// }
}
