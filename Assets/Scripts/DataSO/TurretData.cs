using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "NewTurretData",menuName = "Data/TurretData")]
    public class TurretData : ScriptableObject
    {
        public GameObject bulletPrefab;
        public float reloadDelay = 1;
        public BulletData bulletData;
    }
}