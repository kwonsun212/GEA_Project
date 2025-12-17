using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPool : MonoBehaviour
{
    public GameObject chunkPrefab;
    public int preloadCount = 20;

    Queue<GameObject> pool = new();

    void Awake()
    {
        // 미리 청크 만들어 두기 (초기 렉 방지)
        for (int i = 0; i < preloadCount; i++)
        {
            var chunk = CreateNewChunk();
            pool.Enqueue(chunk);
        }
    }

    GameObject CreateNewChunk()
    {
        var go = Instantiate(chunkPrefab, transform);
        go.SetActive(false);
        return go;
    }

    public GameObject Get()
    {
        if (pool.Count > 0)
            return pool.Dequeue();

        return CreateNewChunk();
    }

    public void Release(GameObject chunk)
    {
        chunk.SetActive(false);
        pool.Enqueue(chunk);
    }
}
