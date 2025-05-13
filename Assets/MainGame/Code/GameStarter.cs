using System;
using System.Collections.Generic;
using UnityEngine;
using verelll.Enemy;
using verelll.Damage;
using verelll.Quests;

namespace verelll.MainGame
{
    public sealed class GameStarter : MonoBehaviour, IDisposable
    {
        [Header("Enemy Settings")] 
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private List<EnemySpawnerPair> _enemySpawnerPairs;

        [Header("Quests Settings")] 
        [SerializeField] private UIQuestsPanel _uiQuestsPanel;
        [SerializeField] private List<BaseQuestConfig> _questConfigs;

        [Header("Other Settings")] 
        [SerializeField] private Camera _mainCamera;

        private EnemyService _enemyService;
        private QuestsService _questsService;
        private DamageInteractionService _damageInteractionService;
        
        private void Start()
        {
            _enemyService = new EnemyService(_enemyContainer);
            _questsService = new QuestsService();
            _damageInteractionService = new DamageInteractionService(_mainCamera);

            _uiQuestsPanel.Init(_questsService);

            CreateEnemies();
            CreateQuests();
            _damageInteractionService.Init();
        }
        
        void IDisposable.Dispose()
        {
            _uiQuestsPanel.Dispose();
            
            _damageInteractionService.Dispose();
        }

        private void CreateEnemies()
        {
            foreach (var spawnerPair in _enemySpawnerPairs)
            {
                _enemyService.SpawnEnemies(spawnerPair.Config, spawnerPair.Count);
            }
        }

        private void CreateQuests()
        {
            foreach (var config in _questConfigs)
            {
                _questsService.StartQuest(config);
            }
        }
    }

    [Serializable]
    public sealed class EnemySpawnerPair
    {
        [field:SerializeField] public int Count { get; private set; }
        [field:SerializeField] public BaseEnemyConfig Config { get; private set; }
    }
}
