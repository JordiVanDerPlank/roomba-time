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
    [SerializeField] float rotationSpeed;
    Vector2 movement = new Vector2();
    Rigidbody2D rb2D;

    [SerializeField] Transform roombaSprite;

    public bool canMove;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //roombaSprite.rotation = movement;
        Vector2 moveDirection = rb2D.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            roombaSprite.rotation = Quaternion.Lerp(roombaSprite.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), rotationSpeed * Time.deltaTime);
        }
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
