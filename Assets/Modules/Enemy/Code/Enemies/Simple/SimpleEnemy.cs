using System.Collections;
using UnityEngine;

namespace verelll.Enemy
{
    public sealed class SimpleEnemy : BaseEnemy
    {
        public SimpleEnemy(SimpleEnemyConfig config, int instanceId)
            : base(config, instanceId) { }

        protected override IEnumerator DeathAnimationRoutine()
        {
            var originalScale = View.transform.localScale;
            var enlarged = originalScale * 1.5f;
            var zero = Vector3.zero;

            var duration = 0.2f;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = elapsed / duration;
                View.transform.localScale = Vector3.Lerp(originalScale, enlarged, t);
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = elapsed / duration;
                View.transform.localScale = Vector3.Lerp(enlarged, zero, t);
                yield return null;
            }
        }
    }
}