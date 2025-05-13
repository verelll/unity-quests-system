using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace verelll.Quests
{
    public sealed class UIQuestWidget : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _completeColor;
        [Space(20)]
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private Image _completeMarkImage;
        [SerializeField] private Image _backgroundImage;
        
        private BaseQuest _curQuest;
        
        internal void SetQuest(BaseQuest quest)
        {
            if (_curQuest != null)
            {
                _curQuest.OnProgressChanged -= HandleProgressChanged;
            }
            
            _curQuest = quest;
            
            if (_curQuest != null)
            {
                _curQuest.OnProgressChanged += HandleProgressChanged;
                _progressSlider.minValue = 0;
                _progressSlider.maxValue = 1;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            if(_curQuest == null)
                return;

            _descriptionText.text = _curQuest.Config.Description;
            _progressSlider.value = _curQuest.NormalizedProgress;
            _progressText.text = $"{_curQuest.CurProgress}/{_curQuest.MaxProgress}";
            _completeMarkImage.gameObject.SetActive(_curQuest.Completed);
            _backgroundImage.color = _curQuest.Completed 
                ? _completeColor 
                : _defaultColor;
        }

        private void HandleProgressChanged()
        {
            UpdateView();
        }
    }
}