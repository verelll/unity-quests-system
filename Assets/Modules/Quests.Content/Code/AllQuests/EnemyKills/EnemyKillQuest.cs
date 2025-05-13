using System;
using System.Collections.Generic;
using System.Linq;
using verelll.Enemy;

namespace verelll.Quests.Content
{
    public sealed class EnemyKillQuest : BaseQuest
    {
        public override int MaxProgress => _config.Count;

        public override int CurProgress => _curProgress;

        private readonly EnemyKillQuestConfig _config;
        private readonly Dictionary<string, int> _killedEnemiesById;

        private int _curProgress;

        public EnemyKillQuest(
            EnemyKillQuestConfig config, 
            Action<BaseQuest> startCallback, 
            Action<BaseQuest> completeCallback) 
            : base(config, startCallback, completeCallback)
        {
            _config = config;
            _killedEnemiesById = new Dictionary<string, int>();
        }
        
        protected override void HandleStart()
        {
            EnemyService.OnEnemyDead += HandleEnemyDead;
        }

        protected override void HandleComplete()
        {
            EnemyService.OnEnemyDead -= HandleEnemyDead;
        }

        private void HandleEnemyDead(BaseEnemyConfig enemyConfig)
        {
            if(State == QuestState.Completed)
                return;

            if (!_killedEnemiesById.ContainsKey(enemyConfig.Id))
                _killedEnemiesById[enemyConfig.Id] = 0;

            _killedEnemiesById[enemyConfig.Id]++;

            if(CanCompleteQuest())
               InvokeComplete();
        }

        private bool CanCompleteQuest()
        {
            switch (_config.Type)
            {
                case QuestEnemyKillType.Any:
                {
                    _curProgress = _killedEnemiesById.Sum(p => p.Value);
                    break;
                }
                case QuestEnemyKillType.Target:
                {
                    _killedEnemiesById.TryGetValue(_config.Target.Id, out _curProgress);
                    break;
                }
            }

            InvokeProgressChanged();
            return _curProgress >= _config.Count;
        }
    }
}