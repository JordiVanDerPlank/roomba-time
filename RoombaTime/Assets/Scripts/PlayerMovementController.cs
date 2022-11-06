using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
//using UnityEngine.LowLevel.PlayerLoop;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;

    public bool canMove;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        GetInput();
        MoveCharacter(movement);
    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void MoveCharacter(Vector2 movementVector)
    {
        movementVector.Normalize();
        // move the RigidBody2D instead of moving the Transform
        rb2D.velocity = movementVector * movementSpeed;
        return;
    }
}
