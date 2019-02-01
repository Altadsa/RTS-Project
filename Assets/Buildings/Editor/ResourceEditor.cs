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

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            SetResourceType();
            serializedObject.ApplyModifiedProperties();
        }

        private void SetResourceType()
        {
            _resource = serializedObject.FindProperty("resourceType");
            _resource.enumValueIndex = EditorGUILayout.Popup(_resource.enumValueIndex, _resource.enumDisplayNames);
        }
    }
}