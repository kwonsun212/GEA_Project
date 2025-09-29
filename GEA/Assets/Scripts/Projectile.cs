using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 moveDir;

    public float speed;

    private int damage;

    public float lifeTime = 2f;

    public void Init(Vector3 direction, float speed, int damage)
    {
        moveDir = direction.normalized;
        this.speed = speed;
        this.damage = damage;
    }

    private void Start()
    {
        //���� �ð� �� �ڵ� ���� (�޸� ����)
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //������ forward ����(��)���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other) 
    { 
        Enemy enemy = other.GetComponent<Enemy>(); 
        if (enemy != null) 
        { 
            enemy.TakeDamage(damage); 
            Destroy(gameObject); 
        } 
    }
}
