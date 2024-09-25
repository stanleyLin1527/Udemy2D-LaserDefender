using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 rawInput;

    // Because the position of transform is where "the center of" the sprite at, 
    // need some paddings to make the whole sprite show on camera
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    // The Boundary of camera in world space position
    Vector2 minBound;
    Vector2 maxBound;

    // The reference of shooter
    Shooter shooter;

    // Get the camera position
    void InitBounds() {
        Camera camera = Camera.main;
        // Left-bottom
        minBound = camera.ViewportToWorldPoint(new Vector2(0, 0));
        // Right-up
        maxBound = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Start() {
        InitBounds();
        shooter = GetComponent<Shooter>();
    }

    void OnMove(InputValue value) { rawInput = value.Get<Vector2>(); }

    void OnFire(InputValue value) { 
        if(shooter!= null) { shooter.isFiring = value.isPressed; } 
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        // Time.deltaTime makes the movement framerate independent, which means the move speed will be the same on different PC
        Vector2 delta = moveSpeed * Time.deltaTime * rawInput;
        // Make sure that player is fully showed in camera. The bounds & paddings restrict where player can go.
        Vector2 newPos = new()
        {
            //              Where player wants to go        Minimum position          Maximum position
            x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x - paddingRight),
            y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y - paddingTop)
        };
        transform.position = (Vector3)newPos;
    }
}
