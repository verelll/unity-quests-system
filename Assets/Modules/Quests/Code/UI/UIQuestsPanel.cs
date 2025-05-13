using System.Collections.Generic;
using UnityEngine;

namespace verelll.Quests
{
    public sealed class UIQuestsPanel : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private UIQuestWidget _prefab;

        private readonly List<UIQuestWidget> _widgets = new List<UIQuestWidget>();
        private QuestsService _questsService;
        
        public void Init(QuestsService questsService)
        {
            _questsService = questsService;
            _questsService.OnQuestStarted += HandleQuestStarted;
        }

        public void Dispose()
        {
            _questsService.OnQuestStarted -= HandleQuestStarted;
            RemoveWidgets();
        }

        private void HandleQuestStarted(BaseQuest quest)
        {
            CreateWidget(quest);
        }

        private void CreateWidget(BaseQuest quest)
        {
            var widget = Instantiate(_prefab, _container);
            widget.SetQuest(quest);
            _widgets.Add(widget);
        }

        private void RemoveWidgets()
        {
            foreach (var widget in _widgets)
            {
                Destroy(widget.gameObject);
            }
            
            _widgets.Clear();
        }
    }
}