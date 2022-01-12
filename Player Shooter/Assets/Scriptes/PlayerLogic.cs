using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float gravity = 3.0f;
    [SerializeField] float jumpForce = 10.0f;
    public bool canMove = false;
    Rigidbody2D rigidbody2d;
    Animator animator;
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (!canMove) { return; }
        MovePlayer();
    }

    void MovePlayer()
    {
        //Vector2 moveVector = new Vector2(moveSpeed, 0);
        //rigidbody2d.AddForce(moveVector * Time.deltaTime);
        if(Input.GetMouseButton(0))
        {
            Vector2 jumpSpeed = new Vector2(0, jumpForce);
            rigidbody2d.AddForce(jumpSpeed);
            animator.SetBool("JumpWithoutGun 0",true);
        }
        else
        {
            animator.SetBool("JumpWithoutGun 0", false);
        }
    }
}
