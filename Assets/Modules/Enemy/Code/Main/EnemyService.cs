using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace verelll.Enemy
{
    public sealed class EnemyService
    {
        private readonly Dictionary<int, BaseEnemy> _allEnemies;
        private readonly Transform _spawnContainer;
        
        private int _curInstanceId;

        public static event Action<BaseEnemyConfig> OnEnemyDead; //Было бы не статичным, если бы можно было прокинуть ссылку на сервис без костылей и синглтонов

        public EnemyService(Transform spawnContainer)
        {
            _allEnemies = new Dictionary<int, BaseEnemy>();
            _spawnContainer = spawnContainer;
        }

#region RandomSpawn

        public void SpawnEnemies(BaseEnemyConfig config, int count)
        {
            var spawnX = new Vector2(-30, 30);
            var spawnZ = new Vector2(-20, 20);
            for (var i = 0; i < count; i++)
            {
                var pos = GetRandomPoint(spawnX, spawnZ);
                CreateEnemy(config, pos, Quaternion.identity);
            }
        }

        private Vector3 GetRandomPoint(Vector2 minMaxX, Vector2 minMaxZ)
        {
            return new Vector3(Random.Range(minMaxX.x, minMaxX.y), 0, Random.Range(minMaxZ.x, minMaxZ.y));
        }

#endregion
        

#region Create/Remove
        
        public void CreateEnemy(BaseEnemyConfig enemyConfig, Vector3 pos, Quaternion rot)
        {
            var enemy = enemyConfig.Create(_curInstanceId);
            var view = Object.Instantiate(enemyConfig.Prefab, pos, rot, _spawnContainer);
            enemy.SetView(view);
            _allEnemies[_curInstanceId] = enemy;
            enemy.Init();
            _curInstanceId++;

            enemy.OnDead += HandleEnemyDead;
        }

        public void RemoveEnemy(int enemyInstanceId)
        {
            if (!_allEnemies.TryGetValue(enemyInstanceId, out var enemy))
            {
                Debug.LogError($"[EnemyService] Enemy with instanceId: {enemyInstanceId} not found!");
                return;
            }
            
            enemy.Dispose();
            
            if(enemy.View != null)
                Object.Destroy(enemy.View.gameObject);

            _allEnemies.Remove(enemyInstanceId);
        }
        
#endregion


#region Enemy Death

        private void HandleEnemyDead(BaseEnemy enemy)
        {
            if(enemy == null)
                return;

            enemy.OnDead -= HandleEnemyDead;
            var config = enemy.Config;
            OnEnemyDead?.Invoke(config);
            RemoveEnemy(enemy.InstanceId);
        }

#endregion

    }
}