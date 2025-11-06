using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public enum EnemyState { Idle, Trace, LongAttack, ShortAttack, RunAway }

    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;    //이동 속도

    public float RunSpeed = 10f;

    public float traceRange = 15f;

    public float LongAttackRange = 10f;

    public float ShortAttackRange = 5f;

    public float attackCooldown = 1.5f;

    public float RunRange = 20f;

    public GameObject projectilePrefabL;

    public GameObject projectilePrefabS;

    public Transform firePoint;

    public int RunCount = 0;


    private Transform player;       //플레이어 추적용

    private Transform RunPlayer;

    private float lastAttackTime;    

    public int maxHP = 5;

    private int currentHP;

    public Slider hpSlider;

    


    void Start()
    {

        currentHP = maxHP;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        RunPlayer = GameObject.FindGameObjectWithTag("Player").transform;

        lastAttackTime = -attackCooldown;

        hpSlider.value = 1f;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);

        switch(state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;


            case EnemyState.RunAway:
                if (dist > RunRange)
                    state = EnemyState.Idle;
                else
                    RunAway();
                    break;

            case EnemyState.Trace:
                if (dist < LongAttackRange)
                    state = EnemyState.LongAttack;
                else if(dist < ShortAttackRange)
                    state = EnemyState.ShortAttack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else if (currentHP <= 5 * 0.2f && RunCount < 1)
                    state = EnemyState.RunAway;
                else
                    TracePlayer();
                break;

            case EnemyState.LongAttack:
                 if (dist < LongAttackRange)
                     state = EnemyState.Trace;
                else if (dist < ShortAttackRange)
                    state = EnemyState.ShortAttack;
                else if (currentHP <= 5 * 0.2f && RunCount < 1)
                    state = EnemyState.RunAway;
                else
                    AttackPlayerL();
                break;

            case EnemyState.ShortAttack:
                if (dist < ShortAttackRange)
                    state = EnemyState.Trace;
                else if (dist < LongAttackRange)
                    state = EnemyState.LongAttack;
                else if (currentHP <= 5 * 0.2f && RunCount < 1)
                    state = EnemyState.RunAway;
                else
                    AttackPlayerS();
                break;
        }
       
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void RunAway()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        transform.position += dir * RunSpeed * Time.deltaTime;
        transform.LookAt(2 * transform.position - player.position);

        RunCount++;
        
    }

    void AttackPlayerL()

    {
        if(Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectileL();
        }
    }

    void AttackPlayerS()

    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectileS();
        }
    }
    void ShootProjectileL()
    {
        if(projectilePrefabL != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefabL, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if(ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }

        }
    }

    void ShootProjectileS()
    {
        if (projectilePrefabS != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefabS, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }

        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
