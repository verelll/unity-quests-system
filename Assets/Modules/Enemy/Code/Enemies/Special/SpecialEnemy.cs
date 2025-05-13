using System.Collections;
using UnityEngine;
using verelll.Architecture;

namespace verelll.Enemy
{
    public sealed class SpecialEnemy : BaseEnemy
    {
        private readonly SpecialEnemyConfig _config;
        private Vector3 _startPos;

        public SpecialEnemy(SpecialEnemyConfig config, int instanceId)
            : base(config, instanceId)
        {
            _config = config;
        }
        
        protected internal override void Init()
        {
            _startPos = View.transform.position;
            base.Init();
        }
        
        protected override void DoDamage()
        {
            if (Random.value < _config.DodgeChance)
            {
                _activeRoutine ??= DodgeAnimationRoutine();
                UnityEventsProvider.CoroutineStart(_activeRoutine);
                Debug.Log("DODGE!!!");
                return;
            }

            base.DoDamage();
        }

        private IEnumerator DodgeAnimationRoutine()
        {
            var dir2D = Random.insideUnitCircle.normalized;
            var direction = new Vector3(dir2D.x, 0, dir2D.y);

            var target = View.transform.position + direction * _config.DodgeDistance;
            var start = View.transform.position;

            var duration = 0.1f;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                View.transform.position = Vector3.Lerp(start, target, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            View.transform.position = target;

            elapsed = 0f;
            while (elapsed < duration)
            {
                View.transform.position = Vector3.Lerp(target, _startPos, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            View.transform.position = _startPos;
            _activeRoutine = null;
        }

        protected override IEnumerator DeathAnimationRoutine()
        {
            var duration = 1.5f;
            var elapsed = 0f;
            var startPos = View.transform.position;
            var targetPos = startPos + Vector3.down * 2f;

            var startScale = View.transform.localScale;
            var endScale = startScale * 0.6f;

            while (elapsed < duration)
            {
                var t = elapsed / duration;

                View.transform.position = Vector3.Lerp(startPos, targetPos, t);
                View.transform.localScale = Vector3.Lerp(startScale, endScale, t);

                elapsed += Time.deltaTime;
                yield return null;
            }

            View.transform.position = targetPos;
            View.transform.localScale = endScale;
        }
    }
}