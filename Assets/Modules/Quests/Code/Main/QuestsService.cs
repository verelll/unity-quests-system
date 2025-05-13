using System;
using System.Collections.Generic;
using UnityEngine;

namespace verelll.Quests
{
    public sealed class QuestsService
    {
        private readonly HashSet<BaseQuest> _activeQuests;
        private readonly HashSet<BaseQuest> _completedQuests;

        public event Action<BaseQuest> OnQuestStarted;
        public event Action<BaseQuest> OnQuestCompleted;
        
        public QuestsService()
        {
            _activeQuests = new HashSet<BaseQuest>();
            _completedQuests = new HashSet<BaseQuest>();
        }

        public void StartQuest(BaseQuestConfig config)
        {
            var quest = config.CreateQuest(HandleQuestStarted, HandleQuestCompleted);
            quest.Start();
        }

        private void HandleQuestStarted(BaseQuest quest)
        {
            _activeQuests.Add(quest);
            OnQuestStarted?.Invoke(quest);
        }

        private void HandleQuestCompleted(BaseQuest quest)
        {
            _completedQuests.Add(quest);
            OnQuestCompleted?.Invoke(quest);
            _activeQuests.Remove(quest);

            if (_activeQuests.Count == 0)
                PrintMsg();
        }

        private void PrintMsg()
        {
            Debug.Log("Поздравляю! Вы выполнили все квесты!");
        }
    }
}
