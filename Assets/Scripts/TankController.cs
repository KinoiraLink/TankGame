
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TankController : MonoBehaviour
{
    
    public PlayerInput playerInput;
    public TankMove tanMove;
    public AimTurret aimTurret;
    public Turret[] turrets;

    
    
    private void Awake()
    {
        if (playerInput == null)
        {
            playerInput = GetComponentInParent<PlayerInput>();
            playerInput.OnMoveBody.AddListener(HandleMoveBody);
            playerInput.OnMoveTurret.AddListener(HandleTurretMovement);
            playerInput.OnShoot.AddListener(HandleShoot);
        }
        if (tanMove == null)
            tanMove = GetComponentInChildren<TankMove>();
        if (aimTurret == null)
            aimTurret = GetComponentInChildren<AimTurret>();
        if (turrets == null || turrets.Length == 0)
            turrets = GetComponentsInChildren<Turret>();
    }

    private void OnDestroy()
    {
        playerInput.OnMoveBody.RemoveListener(HandleMoveBody);
        playerInput.OnMoveTurret.RemoveListener(HandleTurretMovement);
        playerInput.OnShoot.RemoveListener(HandleShoot);
    }


    public void HandleShoot()
    {
        foreach (Turret turret in turrets)
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        tanMove.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        aimTurret.Aim(pointerPosition);
    }
}
