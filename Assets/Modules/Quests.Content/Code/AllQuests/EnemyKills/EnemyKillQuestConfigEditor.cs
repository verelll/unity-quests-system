#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using verelll.Enemy;

namespace verelll.Quests.Content
{
    [CustomEditor(typeof(EnemyKillQuestConfig))]
    public class EnemyKillQuestConfigEditor : Editor
    {
        private ReorderableList _questPairs; 
        
        public override void OnInspectorGUI()
        {
            var config = (EnemyKillQuestConfig)target;
            
            config.Description = EditorGUILayout.TextField("Description", config.Description, GUILayout.Height(120));
            EditorGUILayout.Space(10);
            EditorGUI.BeginChangeCheck();
            config.Type = (QuestEnemyKillType) EditorGUILayout.EnumPopup("Type", config.Type);
            config.Count = EditorGUILayout.IntField("Count", config.Count);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(config);
            }
            
            switch (config.Type)
            {
                case QuestEnemyKillType.Any:
                {
                    break;
                }
                case QuestEnemyKillType.Target:
                {
                    config.Target = (BaseEnemyConfig)EditorGUILayout.ObjectField("Target", config.Target, typeof(BaseEnemyConfig), false);
                    break;
                }
            }
            
        }
    }
}
#endif