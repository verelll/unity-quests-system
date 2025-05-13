using System;
using UnityEngine;

namespace verelll.Quests.Content
{
    [CreateAssetMenu(
        menuName = "verelll/Quests/" + nameof(TimerQuestConfig), 
        fileName = nameof(TimerQuestConfig))]
    public sealed class TimerQuestConfig : BaseQuestConfig
    {
        [field: SerializeField] internal float SecondsToComplete { get; private set; }
        
        protected override BaseQuest CreateQuest(Action<BaseQuest> startCallback, Action<BaseQuest> completeCallback)
        {
            return new TimerQuest(this, startCallback, completeCallback);
        }
    }
}