using UnityEngine;
using UnityEditor;

namespace BreadAndButter
{
    [CustomPropertyDrawer(typeof(SceneFieldAttribute))]
    public class SceneFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            EditorGUI.BeginProperty(_position, _label, _property);

            // Load the scene currently set in the inspector
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(_property.stringValue);

            // Check if anything has changed in the inspector
            EditorGUI.BeginChangeCheck();

            // Draw the scene field as an object field with the sceneasset type
            var newScene = EditorGUI.ObjectField(_position, _label, oldScene, typeof(SceneAsset), false) as SceneAsset;

            // Did anything actually change in the inspector?
            if(EditorGUI.EndChangeCheck())
            {
                // Set the string value to the path of the scene
                string path = AssetDatabase.GetAssetPath(newScene);
                _property.stringValue = path;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}