using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [SerializeField] Vector2 moveDirection;
    private void Update()
    {
        //Vector2 moveDirection = rb2D.velocity;
        //if (moveDirection != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        //    roombaSprite.rotation = Quaternion.Slerp(roombaSprite.rotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), rotationSpeed * Time.deltaTime);
        //}

        moveDirection = rb2D.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            ChangeAngle(angle);
        }
    }

    void ChangeAngle(float angle)
    {
        if (!DOTween.IsTweening(roombaSprite))
            roombaSprite.DORotateQuaternion(Quaternion.AngleAxis(angle - 90, Vector3.forward), 0.35f).SetEase(Ease.InExpo);
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
