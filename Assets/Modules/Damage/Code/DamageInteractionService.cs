using UnityEngine;
using verelll.Architecture;

namespace verelll.Damage
{
    public sealed class DamageInteractionService
    {
        private const int LMB = 0;
        private const int HitMaxDistance = 200;
        
        private readonly Camera _mainCamera;
        
        public DamageInteractionService(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Init()
        {
            UnityEventsProvider.OnUpdate += HandleUpdate;
        }

        public void Dispose()
        {
            UnityEventsProvider.OnUpdate -= HandleUpdate;
        }

        private void HandleUpdate()
        {
            if(!Input.GetMouseButtonDown(LMB))
                return;
            
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, HitMaxDistance))
                return;
            
            if(!hit.collider.TryGetComponent<IDamagableObject>(out var obj))
                return;
            
            obj.InvokeDamage();
        }
    }
}

