
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 70f;
    public float rotationSpeed = 200f;

    public float acceleration = 70f;
    public float deacceleration = 50f;
    public float currentSpeed = 0;
    public float currentForewardDirection = 1f;
        
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
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0,0,-movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;
        CalculateSpeed(movementVector);
        if (movementVector.y > 0)
            currentForewardDirection = 1f;
        else if (this.movementVector.y < 0)
            currentForewardDirection = -1f;
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }
    
    
}
