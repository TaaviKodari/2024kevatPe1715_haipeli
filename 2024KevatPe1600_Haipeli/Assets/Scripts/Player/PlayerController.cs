using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform gunTransform;
    public float moveSpeed = 5f;

    private Rigidbody2D body;

    private Master controls;

    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        controls = new Master();
    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
       moveInput = controls.Player.Move.ReadValue<Vector2>();
       Vector2 movement = new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;
       body.MovePosition(body.position + movement);
    }

    void Update(){
        Shoot();
    }

    private void Shoot()
    {
        if(controls.Player.Fire.triggered){
            GameObject bullet = BulletPoolManager.Instance.GetBullet();
            if(bullet == null){
                return;
            }
            bullet.transform.position = gunTransform.position;
            bullet.transform.rotation = gunTransform.rotation;
        }
    }
}
