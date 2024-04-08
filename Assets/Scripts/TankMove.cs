﻿
using UnityEngine;
using UnityEngine.Events;

public class TankMove : MonoBehaviour
{
    public Rigidbody2D rb2d;

    public TankMovementData movementData;
    
    public float currentSpeed = 0;
    public float currentForewardDirection = 1f;
    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();
    private Vector2 movementVector;
    
    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }
    
    
    private void FixedUpdate()
    {
        //移动变化
        rb2d.velocity = (Vector2)transform.up *  currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        //旋转
        rb2d.MoveRotation(transform.rotation 
                          * Quaternion.Euler(0,0,-movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
        OnSpeedChange?.Invoke(this.movementVector.magnitude);//矢量
        if (movementVector.y > 0)
            currentForewardDirection = 1f;
        else if (this.movementVector.y < 0)
            currentForewardDirection = -1f;
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= movementData.deacceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
    }
    
    
}
