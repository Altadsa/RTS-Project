using UnityEditor;

namespace RTS
{
    [CustomEditor(typeof(Resource))]
    public class ResourceEditor : Editor
    { 
        private SerializedProperty _sResource;
        private SerializedProperty _sResourceAmount;
        private SerializedProperty _sLoadWeight;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SetResourceType();
            SetResourceAmount();
            SetLoadWeight();
            serializedObject.ApplyModifiedProperties();
        }

        private void SetResourceType()
        {
            _sResource = serializedObject.FindProperty("_resourceType");
            _sResource.intValue = EditorGUILayout.Popup("Resource Type", _sResource.enumValueIndex, _sResource.enumDisplayNames);
        }

        private void SetResourceAmount()
        {
            _sResourceAmount = serializedObject.FindProperty("_resourcesLeft");
            _sResourceAmount.intValue = EditorGUILayout.IntField("Resource Amount", _sResourceAmount.intValue);
        }

        private void SetLoadWeight()
        {
            _sLoadWeight = serializedObject.FindProperty("_loadWeight");
            _sLoadWeight.intValue = EditorGUILayout.IntField("Load Weight", _sLoadWeight.intValue);
        }
    }
}