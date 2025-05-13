using System;
using UnityEngine;
using verelll.Damage;

namespace verelll.Enemy
{
    public sealed class EnemyView : MonoBehaviour, IDamagableObject
    {
        private Action _damageCallback;

        internal void Init(Action damageCallback)
        {
            _damageCallback = damageCallback;
        }

#region IDamagableObject

        void IDamagableObject.InvokeDamage() => _damageCallback?.Invoke();
        
#endregion

    }
}