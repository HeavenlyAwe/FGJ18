using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsOutputCanvasScript : MonoBehaviour {

    private class CreditsEntry {
        public string header;
        public List<string> entries = new List<string>();
    }

    public TextAsset creditsTextFile;

    public Text headerEntryPrefab;
    public CreditsEntryScript nameEntryPrefab;

    public float spacing = 20.0f;

    public float scrollSpeed = 35.0f;

    private List<CreditsContainerScript> creditsContainers = new List<CreditsContainerScript>();

    private RectTransform canvas;

    // Use this for initialization
    void Awake() {
        RectTransform box = GetComponent<RectTransform>();
        Debug.Log(box.rect.max + " : " + box.rect.min);
        Debug.Log(transform.position);

        List<CreditsEntry> creditEntries = ReadCreditsFile();

        canvas = GetComponent<RectTransform>();

        Debug.Log(creditEntries.Count);

        foreach (CreditsEntry entry in creditEntries) {
            float height = 0;

            GameObject containerObject = new GameObject();
            VerticalLayoutGroup layout = containerObject.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;

            containerObject.name = entry.header + "Panel";
            containerObject.transform.SetParent(transform, false);

            Text headerEntry = Instantiate<Text>(headerEntryPrefab);
            headerEntry.text = entry.header;

            CreditsContainerScript container = containerObject.AddComponent<CreditsContainerScript>();
            container.Setup(headerEntry, -canvas.sizeDelta.y / 2, canvas.sizeDelta.y / 2, 50.0f);

            headerEntry.transform.SetParent(container.transform, false);

            height += headerEntry.rectTransform.sizeDelta.y;
            height += spacing;

            foreach (string line in entry.entries) {
                CreditsEntryScript creditsEntry = Instantiate<CreditsEntryScript>(nameEntryPrefab);
                string[] parts = line.Split(';');
                creditsEntry.nameText.text = parts[0].Trim();
                creditsEntry.taskText.text = "";
                if (parts.Length > 1) {
                    creditsEntry.taskText.text = parts[1].Trim();
                }
                creditsEntry.transform.SetParent(container.transform, false);
                container.AddEntry(creditsEntry);

                height += creditsEntry.GetComponent<RectTransform>().sizeDelta.y;
                height += spacing;
            }

            container.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, height);
            container.transform.localPosition = new Vector3(0, -(canvas.sizeDelta.y + height) / 2, 0);

            creditsContainers.Add(container);
        }

        containerIndices.Enqueue(0);
    }

    private List<CreditsEntry> ReadCreditsFile() {
        string credits = creditsTextFile.text;

        //char[] archDelim = new char[] { '\r', '\n' };
        //string[] lines = credits.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);
        string[] lines = credits.Split('\n');
        
        List<CreditsEntry> creditEntries = new List<CreditsEntry>();

        CreditsEntry entry = null;
        foreach (string line in lines) {
            if (line.Trim().StartsWith("#")) continue;
            if (line.Trim().Length == 0) continue;
            if (line.Contains("[")) {
                entry = new CreditsEntry();
                entry.header = line.Substring(1, line.Length - 2);
                creditEntries.Add(entry);
            } else {
                entry.entries.Add(line);
            }
        }

        return creditEntries;
    }

    //private int containerIndex = 0;
    //private int containerIndex2 = 1;

    private Queue<int> containerIndices = new Queue<int>();

    private void UpdateContainerPosition(CreditsContainerScript container) {
        container.transform.Translate(new Vector3(0, scrollSpeed, 0) * Time.deltaTime);
    }

    private void AddContainerIndex(int index) {
        if (creditsContainers[index].transform.localPosition.y - creditsContainers[index].GetComponent<RectTransform>().sizeDelta.y / 2 >= -canvas.sizeDelta.y / 2) {
            int nextIndex = index + 1;
            if (nextIndex >= creditsContainers.Count) {
                nextIndex = 0;
            }
            if (!containerIndices.Contains(nextIndex)) {
                containerIndices.Enqueue(nextIndex);
            }
        }
    }

    private void RemoveContainerIndex(CreditsContainerScript container) {
        if (container.transform.localPosition.y >= (canvas.sizeDelta.y + container.GetComponent<RectTransform>().sizeDelta.y) / 2) {
            container.transform.localPosition = new Vector3(0, -(canvas.sizeDelta.y + container.GetComponent<RectTransform>().sizeDelta.y) / 2, 0);
            int index = containerIndices.Dequeue();
            Debug.Log("RemoveContainerIndex: " + index);
        }
    }

    // Update is called once per frame
    void Update() {
        int[] containerIndicesTemp = containerIndices.ToArray();
        foreach (int index in containerIndicesTemp) {
            UpdateContainerPosition(creditsContainers[index]);
            AddContainerIndex(index);
            RemoveContainerIndex(creditsContainers[index]);
        }
    }
}
