using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FloorPlaneMesh : MonoBehaviour {

    public Texture2D m_map;

    public float m_scaleFactor = 0.2f;

    public Rect[] m_textureCoords;

    private void Awake() {

        Vector3[] vertices = new Vector3[(m_map.width * 2) * (m_map.height * 2)];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[m_map.width * 6 * m_map.height * 6];

        Color32[] map = m_map.GetPixels32();
        int i = 0;
        int tx = 0;
        for (int x = 0; x < m_map.width; x++) {
            for (int y = 0; y < m_map.height; y++) {
                Color32 c = map[x + y * m_map.width];

                int index = (int)(c.r / 16);

                // Offseting based on the wall tiles origin in center
                vertices[i + 0] = new Vector3((x - 0.5f) * m_scaleFactor, 0, (y - 0.5f) * m_scaleFactor);
                vertices[i + 1] = new Vector3((x - 0.5f) * m_scaleFactor, 0, (y + 0.5f) * m_scaleFactor);
                vertices[i + 2] = new Vector3((x + 0.5f) * m_scaleFactor, 0, (y - 0.5f) * m_scaleFactor);
                vertices[i + 3] = new Vector3((x + 0.5f) * m_scaleFactor, 0, (y + 0.5f) * m_scaleFactor);

                Rect uvRect;
                if (index < m_textureCoords.Length) {
                    uvRect = GetTextureUV(index);
                } else {
                    Debug.Log("Floor texture not in the UV list");
                    uvRect = new Rect(0f, 0f, 1f, 1f);
                }

                uv[i + 0] = new Vector2(uvRect.x, uvRect.y);
                uv[i + 1] = new Vector2(uvRect.x, uvRect.y + uvRect.height);
                uv[i + 2] = new Vector2(uvRect.x + uvRect.width, uvRect.y);
                uv[i + 3] = new Vector2(uvRect.x + uvRect.width, uvRect.y + uvRect.height);

                triangles[tx + 0] = i + 0;
                triangles[tx + 1] = i + 1;
                triangles[tx + 2] = i + 2;
                triangles[tx + 3] = i + 2;
                triangles[tx + 4] = i + 1;
                triangles[tx + 5] = i + 3;

                i += 4;
                tx += 6;
            }
        }


        Mesh mesh = new Mesh {
            name = "PlaneMesh",
            vertices = vertices,
            uv = uv,
            triangles = triangles
        };
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }

    private Rect GetTextureUV(int index) {
        return m_textureCoords[index];
    }

}
