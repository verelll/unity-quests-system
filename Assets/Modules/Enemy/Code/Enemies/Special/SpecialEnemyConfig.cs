using UnityEngine;

namespace verelll.Enemy
{
    [CreateAssetMenu(
        menuName = "verelll/Enemies/" + nameof(SpecialEnemyConfig),
        fileName = nameof(SpecialEnemyConfig))]
    public sealed class SpecialEnemyConfig : BaseEnemyConfig
    {
        [field: SerializeField, Range(0, 1f)] internal float DodgeChance { get; private set; }
        [field: SerializeField] internal float DodgeDistance { get; private set; }

        internal override BaseEnemy Create(int instanceId)
        {
            return new SpecialEnemy(this, instanceId);
        }
    }
}