using UnityEngine;

namespace verelll.Enemy
{
    public abstract class BaseEnemyConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] internal EnemyView Prefab { get; private set; }
        
        [field:SerializeField] internal int MaxHealth { get; private set; }

        internal abstract BaseEnemy Create(int instanceId);
    }
}