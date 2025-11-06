using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject WaterPrefab;
    public GameObject DirtPrefab;
    public GameObject GrassPrefab;
    public GameObject StonePrefab;

    public int width = 20;
    
    public int depth = 20;
    
    public int maxHeight = 16;

    [SerializeField] float noiseScale = 20f;


    // Start is called before the first frame update
    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for(int x = 0; x< width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    if(y < h)
                    PlaceDirt(x, y, z);

                    if (y == h)
                        PlaceGrass(x, y, z);

                }

                for (int y = h+1; y <= 5; y++)
                {
                    PlaceWater(x, y, z);
                }
            }

        }

    }
    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(WaterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"W_{x}_{y}_{z}";
    }

    private void PlaceDirt(int x, int y, int z)
    {
        int s = Random.Range(0, 100);

        if(s <= 10)
        {
            var go = Instantiate(StonePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"S_{x}_{y}_{z}";
        }
        else
        {
            var go = Instantiate(DirtPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"D_{x}_{y}_{z}";
        }

    }
    private void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(GrassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"G_{x}_{y}_{z}";
    }
    

}
