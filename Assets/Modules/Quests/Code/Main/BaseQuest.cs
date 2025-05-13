using System;
using UnityEngine;

namespace verelll.Quests
{
    public abstract class BaseQuest
    {
        public string Id => Config.name;
        
        public BaseQuestConfig Config { get; }

        public QuestState State { get; private set; }

        public abstract int MaxProgress { get; }
        public abstract int CurProgress { get; }
        public bool Completed => State == QuestState.Completed;
        public float NormalizedProgress => Mathf.Clamp01((float)CurProgress / MaxProgress);

        
        private readonly Action<BaseQuest> _startCallback;
        private readonly Action<BaseQuest> _completeCallback;

        public event Action OnProgressChanged;

        protected BaseQuest(
            BaseQuestConfig config, 
            Action<BaseQuest> startCallback,
            Action<BaseQuest> completeCallback)
        {
            Config = config;
            State = QuestState.Default;

            _startCallback = startCallback;
            _completeCallback = completeCallback;
        }

        internal void Start()
        {
            HandleStart();
            State = QuestState.Started;
            _startCallback?.Invoke(this);
        }

        protected virtual void HandleStart() { }

        protected virtual void HandleComplete() { }

        protected void InvokeComplete()
        {
            HandleComplete();
            State = QuestState.Completed;
            _completeCallback?.Invoke(this);
            InvokeProgressChanged();
        }

        protected void InvokeProgressChanged()
        {
            OnProgressChanged?.Invoke();
        }
    }

    public enum QuestState
    {
        Default = 0,
        Started = 1,
        Completed = 2
    }
}