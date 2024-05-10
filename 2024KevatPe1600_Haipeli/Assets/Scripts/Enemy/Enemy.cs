using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float currentSpeed = 3f;
    public Transform playerTransform;
    public int maxHealth = 3;
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public float dashSpeed = 20f;
    public float dashDuration = 1f;
    private bool isDashing = false;
    private float attackTimer;
    private int currentHealth;
    private Rigidbody2D body;
    private Vector2 direction;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        currentHealth = maxHealth;
        attackTimer = attackCooldown;
        isDashing = false;
    }

    void OnDisable()
    {
        StopAllCoroutines();    
    }

    void FixedUpdate()
    {
        Move();
        Attack();
    }

    void Attack()
    {
        if(playerTransform == null){
            return;
        }

        if(attackTimer > 0){
            attackTimer -= Time.fixedDeltaTime;
        }
        else if(!isDashing && Vector2.Distance(transform.position, playerTransform.position) < attackRange){
            
            StartCoroutine(DashAttack());
        }
    }

    IEnumerator DashAttack(){
        float startTime = Time.time;
        isDashing = true;
        while(Time.time < startTime + dashDuration){
            body.velocity = direction * dashSpeed;
            yield return null;
        }
        isDashing = false;
        body.velocity = Vector2.zero;
        attackTimer = attackCooldown;
    }

    void Move()
    {
        if(playerTransform == null){
            GetPlayer();
            return;
        }
        if(isDashing){
            return;
        }
        direction = (playerTransform.position - transform.position).normalized;
        body.MovePosition(body.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    void GetPlayer(){
        playerTransform = GameManager.Instance.playerController.transform;
    }

    public void TakeDamage(int damage)
    {
       currentHealth -= damage;
       if(currentHealth <= 0)
       {
        Die();
       }
    }

    public void Die()
    {
        EnemyPoolManager.Instance.ReturnEnemy(gameObject);
    }
}

