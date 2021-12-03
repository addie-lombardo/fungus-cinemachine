using Fungus.EditorUtils;
using Fungus;
using UnityEditor;

namespace HumbleGrove.Thriller.EditorUtils
{
    [CustomEditor (typeof(SetCinemachineBrainDefaultBlend))]
    public class SetCinemachineBrainDefaultBlendEditor : CommandEditor
    {
        private SerializedProperty brainProp;
        private SerializedProperty styleProp;
        private SerializedProperty customCurveProp;
        private SerializedProperty durationProp;
    
        public override void OnEnable()
        {
            base.OnEnable();
            
            if (NullTargetCheck()) // Check for an orphaned editor instance
                return;

            brainProp = serializedObject.FindProperty("brain");
            styleProp = serializedObject.FindProperty("style");
            customCurveProp = serializedObject.FindProperty("customCurve");
            durationProp = serializedObject.FindProperty("duration");
        }

        public override void DrawCommandGUI()
        {
            var flowchart = FlowchartWindow.GetFlowchart();
            if (flowchart == null) return;

            serializedObject.Update();

            EditorGUILayout.PropertyField(brainProp);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(styleProp);
            if (styleProp.enumValueIndex == 7)
                EditorGUILayout.PropertyField(customCurveProp);
            EditorGUILayout.PropertyField(durationProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}