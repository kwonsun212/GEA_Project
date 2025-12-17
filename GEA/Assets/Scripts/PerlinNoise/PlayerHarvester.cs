using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PickaxeData;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public float hitCooldown = 0.15f;

    private float _nextHitTime;
    private Camera _cam;

    public Inventory inventory;
    InvntoryUI invenUI;
    public GameObject selectedBlock;

    void Awake()
    {
        _cam = Camera.main;
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
        invenUI = FindObjectOfType<InvntoryUI>();
    }

    void Update()
    {
        // 슬롯 선택 안 함(손)
        if (invenUI.selectedIndex < 0)
        {
            selectedBlock.transform.localScale = Vector3.zero;
            HandleHandMining();
            return;
        }

        ItemType selected = invenUI.GetInventorySlot();
        var pickaxe = PickaxeDatabase.Get(selected);

        if (pickaxe != null)
        {
            HandleMining(pickaxe);
        }

        else
        {
            HandlePlaceMode();
        }
    }

    void HandleMining(PickaxeInfo pickaxe)
    {
        selectedBlock.transform.localScale = Vector3.zero;

        if (!Input.GetMouseButton(0) || Time.time < _nextHitTime)
            return;

        _nextHitTime = Time.time + hitCooldown;

        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            return;

        var block = hit.collider.GetComponent<Block>();
        if (block == null)
            return;

        if (pickaxe.tier < block.requiredTier)
        {
            Debug.Log("곡괭이 등급이 낮습니다!");
            return;
        }

        block.Hit(pickaxe.damage, inventory);
    }

    void HandlePlaceMode()
    {
        Ray rayDebug = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(rayDebug, out var hitDebug, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
        {
            Vector3Int placePos = AdjacentCellOnHitFace(hitDebug);
            selectedBlock.transform.localScale = Vector3.one;
            selectedBlock.transform.position = placePos;
            selectedBlock.transform.rotation = Quaternion.identity;
        }
        else
        {
            selectedBlock.transform.localScale = Vector3.zero;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
            {
                Vector3Int placePos = AdjacentCellOnHitFace(hit);

                ItemType selected = invenUI.GetInventorySlot();
                if (inventory.Consume(selected, 1))
                {
                    FindAnyObjectByType<NoiseVoxelMap>().PlaceTile(placePos, selected);
                }
            }
        }
    }

    static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
    {
        Vector3 baseCenter = hit.collider.transform.position;
        Vector3 adjCenter = baseCenter + hit.normal;
        return Vector3Int.RoundToInt(adjCenter);
    }
    void HandleHandMining()
    {
        if (!Input.GetMouseButton(0) || Time.time < _nextHitTime)
            return;

        _nextHitTime = Time.time + hitCooldown;

        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            return;

        var block = hit.collider.GetComponent<Block>();
        if (block == null)
            return;

        // 손으로는 Wood 등급 블록만 가능
        if (block.requiredTier > PickaxeTier.Wood)
        {
            Debug.Log("손으로는 캘 수 없습니다!");
            return;
        }

        block.Hit(1, inventory); // 손 데미지 1
    }

}
