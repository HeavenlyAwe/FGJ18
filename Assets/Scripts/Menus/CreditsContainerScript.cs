using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsContainerScript : MonoBehaviour {

    private Text headerText;
    private List<CreditsEntryScript> creditEntries;

    private float canvasMin;
    private float canvasMax;
    private float offset;

    //private Color headerTextColor;
    //private Color headerTextColorTransparent;

    public CreditsContainerScript() {
        this.creditEntries = new List<CreditsEntryScript>();
    }

    public void Setup(Text headerText, float min, float max, float offset) {
        this.headerText = headerText;

        this.canvasMin = min;
        this.canvasMax = max;
        this.offset = offset;

        //headerTextColor = headerText.color;
        //headerTextColorTransparent = new Color(headerTextColor.r, headerTextColor.g, headerTextColor.b, 0.0f);
    }

    public void AddEntry(CreditsEntryScript entry) {
        creditEntries.Add(entry);
    }

    private Color Fade(Color color, float posY, float height) {
        float dt = 0.0f;
        if (posY - height / 2 > canvasMin + offset && posY + height / 2 < canvasMax - offset) {
            dt = 1.0f;
        } else if (posY - height / 2 >= canvasMin && posY - height / 2 <= canvasMin + offset) {
            dt = (posY - height / 2 - canvasMin) / offset;
        } else if (posY + height / 2 >= canvasMax - offset && posY + height / 2 <= canvasMax) {
            dt = 1.0f - (posY + height / 2 - (canvasMax - offset)) / offset;
        }
        return Color.Lerp(new Color(color.r, color.g, color.b, 0.0f), new Color(color.r, color.g, color.b, 1.0f), dt);
    }

    // Update is called once per frame
    void Update() {
        float yOffset = transform.localPosition.y;
        float posY = headerText.transform.localPosition.y + yOffset;
        float height = headerText.rectTransform.sizeDelta.y;
        headerText.color = Fade(headerText.color, posY, height);
        foreach (CreditsEntryScript entry in creditEntries) {
            entry.nameText.color = Fade(entry.nameText.color, entry.transform.localPosition.y + yOffset, entry.GetComponent<RectTransform>().sizeDelta.y);
            entry.taskText.color = Fade(entry.taskText.color, entry.transform.localPosition.y + yOffset, entry.GetComponent<RectTransform>().sizeDelta.y);
        }
    }
}
