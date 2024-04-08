using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class TrackMarksSpawner : MonoBehaviour
    {
        private Vector2 lastPosition;
        public float trackDistance = 0.2f;
        public GameObject trackPrefab;
        public int objectPoolSize = 50;
        private ObjectPool _objectPool;

        private void Awake()
        {
            _objectPool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            lastPosition = transform.position;
            _objectPool.Initialize(trackPrefab,objectPoolSize);
        }

        private void Update()
        {
            var distanceDriven = Vector2.Distance(transform.position, lastPosition);
            if (distanceDriven >= trackDistance)
            {
                lastPosition = transform.position;
                var tracks = _objectPool.CreateObject();
                tracks.transform.position = transform.position;
                tracks.transform.rotation = transform.rotation;
            }
        }
    }
    
}