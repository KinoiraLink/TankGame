using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 5)] [SerializeField] private float viewRadius = 11;
    [SerializeField] private float detectionCheckDelay = 0.1f;

    //两层Layer 的原因是想要做视野阻挡
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask visibilityLayer;
    [SerializeField]
    private Transform target = null;
    [field: SerializeField]
    public bool TargetVisible { get; private set; }

    public Transform Target
    {
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        if (Target != null)
            TargetVisible = CheckTargetVisible();
    }

    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position,
            Target.position - transform.position,
            viewRadius,
            visibilityLayer);
        if (result.collider != null)
            //eg: 00001000 00000010 
            //> 0 检测到玩家
            //=0 玩家隐藏
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0;
        return false;
    }

    private void DetectTarget()
    {
        if (Target == null)
            CheckIfPlayerInRange();
        else if (Target != null)
            DetectIfOutOfRange();
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false ||
            Vector2.Distance(transform.position, Target.position) > viewRadius)
        {
            Target = null;
        }
    }
    private void CheckIfPlayerInRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);
        if (collider)
        {
            Target = collider.transform;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,viewRadius);
    }
}
