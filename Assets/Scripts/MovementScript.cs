using UnityEngine;
using UnityEngine.InputSystem; 
using System.Collections;
using System.Collections.Generic;

public class MovementScript : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    Animator anim;



    void Start()
    {
        //assign rigidbody
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //animation

    }

    void FixedUpdate()
    {
        // Normalize the input to prevent faster diagonal movement
        movement.Normalize();
        rb.linearVelocity = movement * moveSpeed;
    }


    void Aninmate()
    {
        anim.SetFloat("moveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));
        anim.SetFloat("MoveMagnitude", movement.magnitude);
    }    
    
}
