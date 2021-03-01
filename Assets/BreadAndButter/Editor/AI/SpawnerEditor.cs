using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.AnimatedValues;

namespace BreadAndButter.AI
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        private Spawner spawner;

        private SerializedProperty sizeProperty;
        private SerializedProperty centerProperty;

        private SerializedProperty floorYPositionProperty;
        private SerializedProperty spawnRateProperty;

        private SerializedProperty shouldSpawnBossProperty;
        private SerializedProperty bossSpawnChanceProperty;
        private SerializedProperty bossPrefabProperty;
        private SerializedProperty enemyPrefabProperty;

        private AnimBool shouldSpawnBoss = new AnimBool();

        private BoxBoundsHandle handle;

        // This function is called when the object becomes enabled and active
        private void OnEnable()
        {
            spawner = target as Spawner;

            sizeProperty = serializedObject.FindProperty("size");
            centerProperty = serializedObject.FindProperty("center");

            floorYPositionProperty = serializedObject.FindProperty("floorYPosition");
            spawnRateProperty = serializedObject.FindProperty("spawnRate");

            shouldSpawnBossProperty = serializedObject.FindProperty("shouldSpawnBoss");
            bossSpawnChanceProperty = serializedObject.FindProperty("bossSpawnChance");
            bossPrefabProperty = serializedObject.FindProperty("bossPrefab");
            enemyPrefabProperty = serializedObject.FindProperty("enemyPrefab");

            shouldSpawnBoss.value = shouldSpawnBossProperty.boolValue;
            shouldSpawnBoss.valueChanged.AddListener(Repaint);

            handle = new BoxBoundsHandle();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Create a vertical layout group visualised as a box
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                // Draw the center and size properties, exactly as unity would
                EditorGUILayout.PropertyField(centerProperty);
                EditorGUILayout.PropertyField(sizeProperty);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(floorYPositionProperty);

                // Cache the original value of spawn rate and create the label
                Vector2 spawnRate = spawnRateProperty.vector2Value;
                string label = $"Spawn Rate Bounds ({spawnRate.x.ToString("0.00")}s - {spawnRate.y.ToString("0.00")}s)";

                // Render the spawn rate as a min max slider and reset the vector2 value
                EditorGUILayout.MinMaxSlider(label, ref spawnRate.x, ref spawnRate.y, 0, 3);
                spawnRateProperty.vector2Value = spawnRate;

                // Apply spacing between lines
                EditorGUILayout.Space();

                // Render the enemyPrefab and shouldSpawnBoss as normal
                EditorGUILayout.PropertyField(enemyPrefabProperty);
                EditorGUILayout.PropertyField(shouldSpawnBossProperty);

                // Attempt to fade the next variables in and out
                shouldSpawnBoss.target = shouldSpawnBossProperty.boolValue;
                if(EditorGUILayout.BeginFadeGroup(shouldSpawnBoss.faded))
                {
                    // Only visible when 'shouldSpawnBossProperty' is true
                    EditorGUI.indentLevel++;

                    // Draw boss spawn chance and boss prefab as normal
                    EditorGUILayout.PropertyField(bossSpawnChanceProperty);
                    EditorGUILayout.PropertyField(bossPrefabProperty);

                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        // Enables the Editor to handle an event in the scene view
        private void OnSceneGUI()
        {
            // Make the handles color green
            Handles.color = Color.green;

            // Store the default matrix
            Matrix4x4 baseMatrix = Handles.matrix;

            // Make the handles use the objects matrix
            Matrix4x4 rotationMatrix = spawner.transform.localToWorldMatrix;
            Handles.matrix = rotationMatrix;

            // Setup the box bounds handle with the spawners values
            handle.center = spawner.center;
            handle.size = spawner.size;

            // Begin listening for changes to the handle, and then draw it
            EditorGUI.BeginChangeCheck();
            handle.DrawHandle();

            // Check if any changes were detected
            if(EditorGUI.EndChangeCheck())
            {
                // Reset the spawner values to the new handle values
                spawner.size = handle.size;
                spawner.center = handle.center;
            }

            // Reset the handles matrix back to default
            Handles.matrix = baseMatrix;
        }
    }
}