using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;    //�̵� �ӵ�

    public int maxHP = 5;

    private int currentHP;

    private Transform player;       //�÷��̾� ������

    void Start()
    {
        currentHP = maxHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        //�÷��̾������ ���� ���ϱ�
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
