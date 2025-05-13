using System;
using UnityEngine;
using verelll.Architecture;

namespace verelll.Quests.Content
{
    public sealed class TimerQuest : BaseQuest
    {
        public override int MaxProgress => (int)_config.SecondsToComplete;

        public override int CurProgress => (int)_curTicks;
        
        private readonly TimerQuestConfig _config;
        private float _curTicks;
        private float _lastProgressUpdateTime;

        public TimerQuest(
            TimerQuestConfig config,
            Action<BaseQuest> startCallback,
            Action<BaseQuest> completeCallback) 
            : base(config, startCallback, completeCallback)
        {
            _config = config;
            _curTicks = 0;
        }
        
        protected override void HandleStart()
        {
            base.HandleStart();
            UnityEventsProvider.OnUpdate += HandleUpdate;
        }

        protected override void HandleComplete()
        {
            UnityEventsProvider.OnUpdate -= HandleUpdate;
        }

        private void HandleUpdate()
        {
            if(_curTicks >= _config.SecondsToComplete)
            {
                _curTicks = _config.SecondsToComplete;
                InvokeComplete();
                return;
            }
            
            _curTicks += UnityEventsProvider.DeltaTime;
            
            if (_curTicks - _lastProgressUpdateTime >= 1f)
            {
                _lastProgressUpdateTime = Mathf.Floor(_curTicks);
                InvokeProgressChanged();
            }
        }
    }
}