using System;
using System.Collections;
using UnityEngine;
using verelll.Architecture;

namespace verelll.Enemy
{
    public abstract class BaseEnemy
    {
        public int InstanceId { get; }
        
        internal EnemyView View { get; private set; }
        
        internal BaseEnemyConfig Config { get; }

        private bool IsDead => _curHealth <= 0;
        
        private int _curHealth;
        protected IEnumerator _activeRoutine;

        public event Action<BaseEnemy> OnDead;

        protected BaseEnemy(BaseEnemyConfig config, int instanceId)
        {
            InstanceId = instanceId;
            Config = config;
            _curHealth = config.MaxHealth;
        }

        internal void SetView(EnemyView view)
        {
            View = view;
            View?.Init(DoDamage);
        }
        
        protected internal virtual void Init() { }

        protected internal virtual void Dispose()
        {
            if (_activeRoutine != null)
            {
                UnityEventsProvider.CoroutineStop(_activeRoutine);
            }
        }

#region Damage

        protected virtual void DoDamage()
        {
            if (IsDead)
                return;

            if (_activeRoutine != null)
                return;
            
            _activeRoutine = ApplyDamageRoutine();
            UnityEventsProvider.CoroutineStart(_activeRoutine);
        }

        private IEnumerator ApplyDamageRoutine()
        {
            if (_curHealth > 0)
            {
                _curHealth--;
                yield return DamageAnimationRoutine();
            }

            if (_curHealth <= 0)
            {
                yield return DeathAnimationRoutine();
                OnDead?.Invoke(this);
            }

            _activeRoutine = null;
        }

        protected virtual IEnumerator DamageAnimationRoutine()
        {
            var duration = 0.2f;
            var height = 0.5f;
            var elapsed = 0f;

            var startPos = View.transform.position;
            var peakPos = startPos + Vector3.up * height;

            while (elapsed < duration)
            {
                elapsed += UnityEventsProvider.DeltaTime;
                var t = elapsed / duration;
                View.transform.position = Vector3.Lerp(startPos, peakPos, t);
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += UnityEventsProvider.DeltaTime;
                var t = elapsed / duration;
                View.transform.position = Vector3.Lerp(peakPos, startPos, t);
                yield return null;
            }
        }

        protected virtual IEnumerator DeathAnimationRoutine()
        {
            yield return null;
        }

#endregion

    }
}

