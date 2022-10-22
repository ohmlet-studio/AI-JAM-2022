using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public GameObject grassTile;
    public GameObject mudTile;
    public GameObject dirtTile;
    public GameObject waterTile;

    public int width;
    public int height;

    public int seed;

    public float noise_scale;
    public float height_multiplier;

    private List<GameObject> tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new List<GameObject>();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float sampleX = i * noise_scale + seed;
                float sampleY = j * noise_scale + seed;

                float n = Mathf.PerlinNoise((float)sampleX, (float)sampleY);

                GameObject tile = mudTile;
                if (n > 0.4) {
                    tile = grassTile;
                } else if (n >= 0.35 && n < 0.4){
                    tile = dirtTile;
                } else if(n < 0.15) {
                    tile = waterTile;
                }

                GameObject go = Instantiate(tile, this.transform, false);
                go.transform.position = new Vector3(i, n > 0.5 ? (n-0.5f)*height_multiplier : 0, j);
            }
        }

        this.transform.position = new Vector3(-width / 2, 0, -height / 2);

        //combineMesh();
    }

    private void combineMesh() {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        //transform.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
