using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public Transform player;
    public ChunkPool chunkPool;

    public int chunkSize = 16;
    public int renderDistance = 2;

    Dictionary<Vector2Int, GameObject> chunks = new();
    Vector2Int lastPlayerChunk = new Vector2Int(int.MinValue, int.MinValue);

    void Update()
    {
        Vector2Int playerChunk = GetChunkCoord(player.position);

        if (playerChunk == lastPlayerChunk)
            return;

        lastPlayerChunk = playerChunk;

        // 생성
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int coord = playerChunk + new Vector2Int(x, z);

                if (!chunks.ContainsKey(coord))
                    CreateChunk(coord);
            }
        }

        // 반환
        ReleaseFarChunks(playerChunk);
    }

    Vector2Int GetChunkCoord(Vector3 pos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(pos.x / chunkSize),
            Mathf.FloorToInt(pos.z / chunkSize)
        );
    }

    void CreateChunk(Vector2Int coord)
    {
        GameObject chunk = chunkPool.Get();
        chunk.transform.SetParent(transform);

        chunk.transform.position = new Vector3(
            coord.x * chunkSize,
            0,
            coord.y * chunkSize
        );

        chunk.name = $"Chunk_{coord.x}_{coord.y}";
        chunk.SetActive(true);

        var noise = chunk.GetComponent<NoiseVoxelMap>();

        noise.ClearChunk();
        noise.StopAllCoroutines();

        StartCoroutine(
            noise.GenerateCoroutine(chunkSize, coord)
        );

        chunks.Add(coord, chunk);
    }

    void ReleaseFarChunks(Vector2Int playerChunk)
    {
        List<Vector2Int> removeList = new();

        foreach (var kv in chunks)
        {
            Vector2Int coord = kv.Key;
            int dx = Mathf.Abs(coord.x - playerChunk.x);
            int dz = Mathf.Abs(coord.y - playerChunk.y);

            if (dx > renderDistance || dz > renderDistance)
                removeList.Add(coord);
        }

        foreach (var coord in removeList)
        {
            var chunk = chunks[coord];

            var noise = chunk.GetComponent<NoiseVoxelMap>();
            if (noise != null)
                noise.StopAllCoroutines();

            chunks.Remove(coord);
            chunkPool.Release(chunk); 
        }
    }
}
