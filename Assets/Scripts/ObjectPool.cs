﻿using System;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace DefaultNamespace
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] protected GameObject objectToPool;
        [SerializeField] protected int poolSize = 10;

        protected Queue<GameObject> objectPool;

        public Transform spawnedObjectsParent;

        public bool alwaysDestroy = false;
        private void Awake()
        {
            objectPool = new Queue<GameObject>();
        }

        public void Initialize(GameObject objectToPool, int poolSize = 10)
        {
            this.objectToPool = objectToPool;
            this.poolSize = poolSize;
        }

        public GameObject CreateObject()
        {
            CreateObjectParentIfNeeded();
            GameObject spawnedObject = null;

            if (objectPool.Count < poolSize)
            {
                spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
                spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
                spawnedObject.transform.SetParent(spawnedObjectsParent);
                spawnedObject.AddComponent<DestroyIfDisabled>();
            }
            else
            {
                spawnedObject = objectPool.Dequeue();
                spawnedObject.transform.position = transform.position;
                spawnedObject.transform.rotation = Quaternion.identity;
                spawnedObject.SetActive(true);
            }
            objectPool.Enqueue(spawnedObject);
            return spawnedObject;
        }

        private void CreateObjectParentIfNeeded()
        {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
                spawnedObjectsParent = parentObject.transform;
            else
                spawnedObjectsParent = new GameObject(name).transform;
        }

        private void OnDestroy()
        {
            foreach (GameObject item in objectPool)
            {
                if(item == null)
                    continue;
                else if(item.activeSelf == false || alwaysDestroy)
                    Destroy(item);
                else
                {
                    item.GetComponent<DestroyIfDisabled>().SelfDestructionEnabled = true;
                }
            }
        }
    }
}