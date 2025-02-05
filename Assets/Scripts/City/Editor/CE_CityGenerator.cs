using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(CityGenerator))]
    public class CE_CityGenerator : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CityGenerator generator = (CityGenerator)target;
            if (GUILayout.Button("Generate City"))
            {
                generator.Generate();
            }
            if (GUILayout.Button("Clear City"))
            {
                generator.Clear();
            }
        }
    }
}