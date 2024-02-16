using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    }
}
