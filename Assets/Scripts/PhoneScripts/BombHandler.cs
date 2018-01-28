using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombHandler : MonoBehaviour {

    public RectTransform[] bombSlots;
    public BombStates bombStatesPrefab;

    public LoadMazeFromBitmap maze;
    public RectTransform gamePanel;

    private Client clientScript;

    private BombStates[] bombs;

    private BombStates dragBomb;
    private int currentBombSlot = -1;

    // Use this for initialization
    void Start() {
        bombs = new BombStates[bombSlots.Length];
        for (int i = 0; i < bombSlots.Length; i++) {
            bombs[i] = GameObject.Instantiate<BombStates>(bombStatesPrefab, Vector3.zero, Quaternion.identity);
            bombs[i].transform.SetParent(GameObject.Find("GamePanel").transform);
            bombs[i].gameObject.SetActive(false);

            bombSlots[i].GetComponent<Image>().sprite = bombStatesPrefab.bombUnplaced;
        }

        GameObject go = GameObject.Find("Client");
        if (go != null) {
            clientScript = go.GetComponent<Client>();
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 screenMousePosition = Input.mousePosition;
        screenMousePosition.z = -10.0f; //distance of the plane from the camera
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePosition);
        Vector2 screenPoint = new Vector2(mousePosition.x, mousePosition.y);

        if (dragBomb != null) {
            dragBomb.transform.position = new Vector3(mousePosition.x, mousePosition.y, -10);
            Vector3 localPos = dragBomb.transform.localPosition;
            dragBomb.transform.localPosition = new Vector3(localPos.x, localPos.y, -10);
            Debug.Log(dragBomb.transform.localPosition);
        }

        if (Input.GetMouseButtonDown(0)) {
            for (int i = 0; i < bombSlots.Length; i++) {
                if (RectTransformUtility.RectangleContainsScreenPoint(bombSlots[i], screenPoint)) {
                    ClickBombSlot(i, new Vector3(mousePosition.x, mousePosition.y, -10));
                }
            }
        } else if (Input.GetMouseButtonUp(0)) {
            if (dragBomb != null) {
                if (RectTransformUtility.RectangleContainsScreenPoint(gamePanel, screenPoint)) {
                    PlaceBomb(bombs[currentBombSlot]);
                } else {
                    bombSlots[currentBombSlot].GetComponent<Image>().sprite = bombs[currentBombSlot].bombUnplaced;
                    bombs[currentBombSlot].gameObject.SetActive(false);
                }
            }
        }
    }

    private void ClickBombSlot(int i, Vector3 position) {
        Debug.Log("ClickBombSlot: " + i);
        if (bombs[i].IsAvailable()) {
            currentBombSlot = i;
            bombs[i].gameObject.SetActive(true);
            dragBomb = bombs[i];
            dragBomb.transform.position = position;
            dragBomb.transform.localScale = Vector3.one;
            dragBomb.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            dragBomb.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);

            bombSlots[i].GetComponent<Image>().sprite = bombs[i].bombUsed;
        } else if (bombs[i].IsPlaced() && !bombs[i].IsActivated()) {
            clientScript.ActivateTrap(i);
            bombs[i].Activate();
        }
    }

    private void PlaceBomb(BombStates bomb) {
        bomb.Place();
        // currentBombSlot = -1;

        float x = bomb.transform.localPosition.x / gamePanel.rect.width * maze.GetWidth() * 0.2f;
        float y = bomb.transform.localPosition.y / gamePanel.rect.height * maze.GetHeight() * 0.2f;

        Debug.Log("Bomb location: " + x + " : " + y);

        clientScript.PlaceTrap(x, y, currentBombSlot);

        dragBomb = null;
    }

}
