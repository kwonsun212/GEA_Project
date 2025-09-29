using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;    //이동 속도

    public int maxHP = 5;

    private int currentHP;

    private Transform player;       //플레이어 추적용

    void Start()
    {
        currentHP = maxHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        //플레이어까지의 방향 구하기
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        if(currentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
