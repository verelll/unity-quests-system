using UnityEngine;

namespace verelll.Enemy
{
    [CreateAssetMenu(
        menuName = "verelll/Enemies/" + nameof(SimpleEnemyConfig),
        fileName = nameof(SimpleEnemyConfig))]
    public sealed class SimpleEnemyConfig : BaseEnemyConfig
    {
        internal override BaseEnemy Create(int instanceId)
        {
            return new SimpleEnemy(this, instanceId);
        }
    }
}