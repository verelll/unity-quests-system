using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using verelll.Enemy;

namespace verelll.Quests.Content
{
    [CreateAssetMenu(
        menuName = "verelll/Quests/" + nameof(EnemyKillQuestConfig), 
        fileName = nameof(EnemyKillQuestConfig))]
    public sealed class EnemyKillQuestConfig : BaseQuestConfig
    {
        [field: SerializeField] internal QuestEnemyKillType Type { get; set; }
        [field: SerializeField] internal int Count { get; set; }
        [field: SerializeField] internal BaseEnemyConfig Target { get; set; }
        
        protected override BaseQuest CreateQuest(Action<BaseQuest> startCallback, Action<BaseQuest> completeCallback)
        {
            return new EnemyKillQuest(this, startCallback, completeCallback);
        }
    }
    
    public enum QuestEnemyKillType
    {
        Any = 0,
        Target = 1
    }
}