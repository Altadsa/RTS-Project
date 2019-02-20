using UnityEditor;

namespace RTS
{
    [CustomEditor(typeof(Resource))]
    public class ResourceEditor : Editor
    {
        private SerializedProperty _sGatherPoint, _sCanvas, _sResource, _sResourceAmount, _sLoadWeight, _sWorkTime;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SetGatherPoint();
            SetResourceType();
            SetResourceAmount();
            SetLoadWeight();
            SetTimeToWork();
            serializedObject.ApplyModifiedProperties();
        }

        private void SetGatherPoint()
        {
            _sGatherPoint = serializedObject.FindProperty("_gatherPoint");
            EditorGUILayout.PropertyField(_sGatherPoint);
        }

        private void SetUiCanvas()
        {
            _sCanvas = serializedObject.FindProperty("_uiCanvas");
            EditorGUILayout.PropertyField(_sCanvas);
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
            _sLoadWeight.floatValue = EditorGUILayout.FloatField("Load Weight", _sLoadWeight.floatValue);
        }

        private void SetTimeToWork()
        {
            _sWorkTime = serializedObject.FindProperty("_timeToWork");
            _sWorkTime.floatValue = EditorGUILayout.FloatField("Work Time", _sWorkTime.floatValue);
        }
    }
}