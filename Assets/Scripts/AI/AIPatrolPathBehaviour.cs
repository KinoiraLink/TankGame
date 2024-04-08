using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.AI
{
    public class AIPatrolPathBehaviour : AIBehaviour
    {
        public PatrolPath patrolPath;
        [Range(0.1f, 1)] public float arriveDistance = 1;
        public float waitTime = 0.5f;
        [SerializeField] private bool isWaiting = false;
        [FormerlySerializedAs("currentPatrollTarget")] [SerializeField] private Vector2 currentPatrolTarget = Vector2.one;
        private bool isInitialized = false;

        private int currentIndex = -1;

        private void Awake()
        {
            if (patrolPath == null)
                patrolPath = GetComponentInChildren<PatrolPath>();
        }

        public override void PerformAction(TankController tank, AIDetector detector)
        {
            if (!isWaiting)
            {
                if (patrolPath.Lenght < 2)
                    return;
                if (!isInitialized)
                {
                    var currentPathPoint = patrolPath.GetClosestPathPoint(tank.transform.position);
                    this.currentIndex = currentPathPoint.Index;
                    this.currentPatrolTarget = currentPathPoint.Position;
                    isInitialized = true;
                }

                if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < arriveDistance)
                {
                    isWaiting = true;
                    StartCoroutine(WaitCoroutine());
                    return;
                }

                Vector2 directionToGO = currentPatrolTarget - (Vector2)tank.tanMove.transform.position;
                var dotProduct = Vector2.Dot(tank.tanMove.transform.up, directionToGO.normalized);

                if (dotProduct < 0.98f)
                {
                    var crossProduct = Vector3.Cross(tank.tanMove.transform.up, directionToGO.normalized);
                    int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                    tank.HandleMoveBody(new Vector2(rotationResult,1));
                }
                else
                {
                    tank.HandleMoveBody(Vector2.up);
                }
            }
        }

        IEnumerator  WaitCoroutine()
        {
            yield return new WaitForSeconds(waitTime);
            var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
            currentPatrolTarget = nextPathPoint.Position;
            currentIndex = nextPathPoint.Index;
            isWaiting = false;
        }
    }
}