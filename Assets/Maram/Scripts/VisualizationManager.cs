using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
    public GameObject spherePrefab;
    public int width = 20;
    public int height = 20;
    public float spacing = 1.2f;

    private GameObject[,] spheres;

    void Start()
    {
        spheres = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * spacing, 0, y * spacing);
                spheres[x, y] = Instantiate(spherePrefab, pos, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float value = 0.5f + 0.5f * Mathf.Sin(Time.time + x + y);

                Vector3 scale = spheres[x, y].transform.localScale;
                scale.y = 0.8f + value * 2f + 0.3f * Mathf.Sin(Time.time * 5f + x); spheres[x, y].transform.localScale = scale;

                Renderer rend = spheres[x, y].GetComponent<Renderer>();
                Color color;

                float rowValue = 1f - ((float)y / (height - 1));

                if (rowValue < 0.33f)
                {
                    color = Color.blue;
                }
                else if (rowValue < 0.66f)
                {
                    color = Color.yellow;
                }
                else
                {
                    color = Color.red;
                }

                rend.material.color = color;
            }
        }
    }
}