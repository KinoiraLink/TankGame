using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "NewBulletData",menuName = "Data/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float speed = 10f;
        public int damage = 5;
        public float maxDistance = 10f;
    }
}