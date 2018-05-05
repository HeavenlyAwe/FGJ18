using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMazeFromBitmap : MonoBehaviour {

    public SpriteRenderer maze;
    public RectTransform gamePanel;

    public Sprite mazeSprite;

    void Start() {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Texture2D tex = sr.sprite.texture;

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

        sr.sprite = Sprite.Create(texture, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
    }

    public int GetWidth() {
        return GetComponent<SpriteRenderer>().sprite.texture.width;
    }

    public int GetHeight() {
        return GetComponent<SpriteRenderer>().sprite.texture.height;
    }
}
