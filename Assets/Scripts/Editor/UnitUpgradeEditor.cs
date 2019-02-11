using UnityEditor;
using UnityEngine;

namespace RTS
{
    ////[CustomEditor(typeof(UnitUpgradeData))]
    //public class UnitUpgradeEditor : Editor
    //{
    //    SerializedProperty _sTitle;
    //    SerializedProperty _sUpgrade;
    //    SerializedProperty _sCost;
    //    SerializedProperty _sTime;
    //    SerializedProperty _sRequirements;
    //    int _totalRequirements;

    //    public override void OnInspectorGUI()
    //    {
    //        serializedObject.Update();
    //        _sTitle = serializedObject.FindProperty("_upgradeTitle");
    //        _sUpgrade = serializedObject.FindProperty("_upgradeValue");
    //        _sTime = serializedObject.FindProperty("_timeNeeded");
    //        _sCost = serializedObject.FindProperty("_cost");
    //        _sRequirements = serializedObject.FindProperty("_requirements");

    //        _sTitle.stringValue = EditorGUILayout.TextField("Title", _sTitle.stringValue);
    //        EditorGUILayout.PropertyField(_sUpgrade);
    //        _sTime.floatValue = EditorGUILayout.FloatField("Time Needed", _sTime.floatValue);
    //        _sCost.vector3IntValue = EditorGUILayout.Vector3IntField("Gold, Timber and Food Cost", _sCost.vector3IntValue);

    //        _totalRequirements = _sRequirements.arraySize;
    //        _totalRequirements = EditorGUILayout.IntField("Number of Requirements", _totalRequirements);

    //        if (_totalRequirements != _sRequirements.arraySize && _totalRequirements >= 0)
    //        {
    //            while (_totalRequirements > _sRequirements.arraySize)
    //            { _sRequirements.InsertArrayElementAtIndex(_sRequirements.arraySize); }
    //            while (_totalRequirements < _sRequirements.arraySize)
    //            { _sRequirements.DeleteArrayElementAtIndex(_sRequirements.arraySize - 1); }
    //        }

    //        for (int i = 0; i < _totalRequirements; i++)
    //        {
    //            SerializedProperty item = _sRequirements.GetArrayElementAtIndex(i);
    //            EditorGUILayout.PropertyField(item);
    //        }


    //        serializedObject.ApplyModifiedProperties();
    //    }
 //   }
}
