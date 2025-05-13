using System;
using UnityEngine;

namespace verelll.Quests
{
    public abstract class BaseQuestConfig : ScriptableObject
    {
        [field:SerializeField, TextArea(5, 10)] public string Description { get; set; }
        
        protected internal abstract BaseQuest CreateQuest(Action<BaseQuest> startCallback, Action<BaseQuest> completeCallback);
    }
}

