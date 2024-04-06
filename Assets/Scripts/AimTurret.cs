
using UnityEngine;

//炮塔旋转
public class AimTurret : MonoBehaviour
{
    public float turretRotationSpeed = 150;

    public void Aim(Vector2 inputPointerPosition)
    {
        //获取朝向
        var turretDirection = (Vector3)inputPointerPosition - this.transform.position;
        //获取角度
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;// Mathf.Rad2Deg:弧度到度的转化常量
        //平滑
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        //转向
        this.transform.rotation = Quaternion.RotateTowards(
            this.transform.rotation,//现在的朝向
            Quaternion.Euler(0,0,desiredAngle-90),//旋转的角度
            rotationStep//平滑
            );        
    }
}
