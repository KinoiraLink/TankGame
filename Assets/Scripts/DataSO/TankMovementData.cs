using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTankMovementData",menuName = "Data/TankMovmentData")]
public class TankMovementData : ScriptableObject
{
    public float maxSpeed = 70f;
    public float rotationSpeed = 200f;
    public float acceleration = 70f;
    public float deacceleration = 50f;
}
