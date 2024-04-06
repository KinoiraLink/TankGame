
using UnityEngine;

public class TankController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxSpeed = 10f;
    public float rotationSpeed = 100f;
    public float turretRotationSpeed = 150;
    public Transform turretParent;
    private Vector2 movementVector;
    
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        //移动变化
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxSpeed * Time.fixedDeltaTime;
        //旋转
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0,0,-movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void HandleShoot()
    {
        Debug.Log("Shooting");
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        //获取朝向
        var turretDirection = (Vector3)pointerPosition - turretParent.position;
        //获取角度
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;// Mathf.Rad2Deg:弧度到度的转化常量
        //平滑
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        //转向
        turretParent.rotation = Quaternion.RotateTowards(
            turretParent.rotation,//现在的朝向
            Quaternion.Euler(0,0,desiredAngle-90),//旋转的角度
            rotationStep//平滑
            );

    }



}
