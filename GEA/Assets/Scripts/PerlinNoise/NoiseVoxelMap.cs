using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PickaxeData;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject WaterPrefab;
    public GameObject DirtPrefab;
    public GameObject GrassPrefab;
    public GameObject StonePrefab;
    public GameObject IronOrePrefab;
    public GameObject GoldOrePrefab;
    public GameObject DiamondOrePrefab;
    
    public int maxHeight = 16;

    [SerializeField] float noiseScale = 20f;

    bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {

    }
    void OnDisable()
    {
        isDestroyed = true;
    }
    void OnEnable()
    {
        isDestroyed = false;
    }

    void OnDestroy()
    {
        isDestroyed = true;
    }
    public void Generate(int chunkSize, Vector2Int chunkCoord)
    {
        float offsetX = chunkCoord.x * chunkSize;
        float offsetZ = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);
                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    int wx = x + (chunkCoord.x * chunkSize);
                    int wz = z + (chunkCoord.y * chunkSize);

                    if (y < h)
                        PlaceDirt(wx, y, wz);
                    else
                        PlaceGrass(wx, y, wz);
                }

                for (int y = h + 1; y <= 5; y++)
                {
                    int wx = x + (chunkCoord.x * chunkSize);
                    int wz = z + (chunkCoord.y * chunkSize);

                    PlaceWater(wx, y, wz);
                }
            }
        }
    }
    private void PlaceWater(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(WaterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"W_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Water;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Wood;
    }

    private void PlaceDirt(int x, int y, int z)
    {
        if (isDestroyed) return;

        int s = Random.Range(0, 100);

        if (y <= 3 && s < 3)
        {
            PlaceDiamond(x, y, z);
        }
        else if (y <= 6 && s < 8)
        {
            PlaceGold(x, y, z);
        }
        else if (y <= 10 && s < 18)
        {
            PlaceIron(x, y, z);
        }
        else if (s < 40)
        {
            PlaceStone(x, y, z);
        }
        else
        {
            var go = Instantiate(DirtPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
            go.name = $"D_{x}_{y}_{z}";

            var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
            b.type = ItemType.Dirt;
            b.maxHp = 3;
            b.dropCount = 1;
            b.mineable = true;
            b.requiredTier = PickaxeTier.Wood;
        }
    }

    private void PlaceStone(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(StonePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"S_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Stone;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Stone;
    }
    private void PlaceGrass(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(GrassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"G_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.Grass;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Wood;
    }
    void PlaceIron(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(IronOrePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Iron_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.IronOre;
        b.maxHp = 4;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Iron;
    }

    void PlaceGold(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(GoldOrePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Gold_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.GoldOre;
        b.maxHp = 5;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Iron;
    }

    void PlaceDiamond(int x, int y, int z)
    {
        if (isDestroyed) return;
        var go = Instantiate(DiamondOrePrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Diamond_{x}_{y}_{z}";

        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = ItemType.DiamondOre;
        b.maxHp = 6;
        b.dropCount = 1;
        b.mineable = true;
        b.requiredTier = PickaxeTier.Diamond;
    }

    public void PlaceTile(Vector3Int pos, ItemType type)
    {
        switch(type)
        {
            case ItemType.Dirt:
                PlaceDirt(pos.x, pos.y, pos.z);
                break;
            case ItemType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;
            case ItemType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;
            case ItemType.Stone:
                PlaceStone(pos.x, pos.y, pos.z);
                break;
        }

    }
    public IEnumerator GenerateCoroutine(int chunkSize, Vector2Int chunkCoord)
    {
        float offsetX = chunkCoord.x * chunkSize;
        float offsetZ = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                if (isDestroyed)
                    yield break;

                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);
                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    int wx = x + chunkCoord.x * chunkSize;
                    int wz = z + chunkCoord.y * chunkSize;

                    if (y < h)
                        PlaceDirt(wx, y, wz);
                    else
                        PlaceGrass(wx, y, wz);
                }

                for (int y = h + 1; y <= 5; y++)
                {
                    int wx = x + chunkCoord.x * chunkSize;
                    int wz = z + chunkCoord.y * chunkSize;

                    PlaceWater(wx, y, wz);
                }

                //프레임 분산
                if ((x * chunkSize + z) % 8 == 0)
                    yield return null;
            }
        }
    }
    public void ClearChunk()
    {
        // 기존 생성 중단
        StopAllCoroutines();

        // 자식 블록 전부 제거
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


}
