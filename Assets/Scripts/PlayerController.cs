using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputControl inputControl;

    private PhysicsCheck physicsCheck;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Vector2 Dirtion;

    [Header("角色基本参数")]
    public float speed;
    public float jumpForce;
    private void Awake()
    {
        inputControl = new InputControl();
        physicsCheck = GetComponent<PhysicsCheck>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        inputControl.Gameplay.Jump.started += Jump;
    }

   

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        Dirtion = inputControl.Gameplay.Move.ReadValue<Vector2>();
    }

    public void Move()
    {
        rb.velocity = new Vector2(Dirtion.x * speed * Time.deltaTime, rb.velocity.y);

        if(Dirtion.x > 0)
            sr.flipX = false;  
        if (Dirtion.x < 0)
            sr.flipX = true;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}
