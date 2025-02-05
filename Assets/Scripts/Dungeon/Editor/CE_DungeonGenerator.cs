using UnityEditor;
using UnityEngine;

namespace WinterUniverse
{
    [CustomEditor(typeof(DungeonGenerator))]
    public class CE_DungeonGenerator : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DungeonGenerator generator = (DungeonGenerator)target;
            if (GUILayout.Button("Generate Dungeon"))
            {
                generator.Generate();
            }
            if (GUILayout.Button("Clear Dungeon"))
            {
                generator.Clear();
            }
        }
    }
}