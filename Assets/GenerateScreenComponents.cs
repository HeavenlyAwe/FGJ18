using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateScreenComponents : MonoBehaviour {

    public List<Sprite> sprites = new List<Sprite>(); // Images of the traps
    public RectTransform parentPanel;
    
    // The target panel to allow dropping onto
    public RectTransform targetPanel;

    // public float sideLength = 50;
    public int imagesPerRow = 2;

    // Use this for initialization
    void Start() {

        float sideLength = (parentPanel.sizeDelta.x / imagesPerRow) * 0.8f;
        Debug.Log("Side length: " + sideLength);
        float spacing = (parentPanel.sizeDelta.x - sideLength * imagesPerRow) / (imagesPerRow + 1);

        float xPos = 0;
        float yPos = 0;

        Debug.Log(sideLength * imagesPerRow + (1 + imagesPerRow) * spacing + " : " + parentPanel.sizeDelta.x);

        for (int i = 0; i < sprites.Count; i++) {
            Sprite currentSprite = sprites[i];

            GameObject newObj = new GameObject();
            DraggableImage draggableImage = newObj.AddComponent<DraggableImage>();
            draggableImage.target = targetPanel;

            Image newImage = newObj.GetComponent<Image>();
            newImage.sprite = currentSprite;
            if (i % imagesPerRow == 0) {
                xPos = spacing;
                yPos -= (spacing + sideLength);
            } else {
                xPos += (spacing + sideLength);
            }
            newObj.GetComponent<RectTransform>().SetParent(parentPanel.transform);
            newObj.GetComponent<RectTransform>().localScale = Vector3.one;
            newObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            newObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            newObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            newObj.GetComponent<RectTransform>().sizeDelta = new Vector2(sideLength, sideLength);
            newObj.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);

            Debug.Log(newObj.transform.position);

            newObj.SetActive(true);
        }
    }
}
