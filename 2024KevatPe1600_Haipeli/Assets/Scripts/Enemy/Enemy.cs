using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float currentSpeed = 3f;
    public Transform playerTransform;
    public int maxHealth = 3;
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
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if(playerTransform == null){
            GetPlayer();
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

