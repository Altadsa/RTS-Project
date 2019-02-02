using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

namespace RTS
{
    [CustomEditor(typeof(Resource))]
    public class ResourceEditor : Editor
    {
        private ResourceType _resourceType;

        private SerializedProperty _resource;
        private SerializedProperty _resourceAmount;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SetResourceType();
            SetResourceAmount();
            serializedObject.ApplyModifiedProperties();
        }

        private void SetResourceType()
        {
            _resource = serializedObject.FindProperty("resourceType");
            _resource.intValue = EditorGUILayout.Popup("Resource Type", _resource.enumValueIndex, _resource.enumDisplayNames);
        }

        private void SetResourceAmount()
        {
            _resourceAmount = serializedObject.FindProperty("_resourcesLeft");
            _resourceAmount.intValue = EditorGUILayout.IntField("Resource Amount", _resourceAmount.intValue);
        }
    }
}