using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SkillSystem
{
    //[CustomEditor(typeof(SkillAnimation))]
    public class SkillAnimationEditor : Editor
    {
        SerializedProperty animationType;
        SerializedProperty startTime;
        SerializedProperty animationName;
        SerializedProperty sound;
        SerializedProperty lifetime;
        SerializedProperty objIndex;
        SerializedProperty location;
        SerializedProperty parent;
        SerializedProperty positionOffset;
        SerializedProperty rotationOffset;
        SerializedProperty moveForwards;
        SerializedProperty movementSpeed;
        SerializedProperty rotateTowardsTarget;
        SerializedProperty rotationSpeed;
        SerializedProperty scale;
        SerializedProperty destroyOnCollision;
        SerializedProperty effectIds;

        private void OnEnable()
        {

            animationType = serializedObject.FindProperty("animationType");
            startTime = serializedObject.FindProperty("startTime");

            
            //animationName = serializedObject.FindProperty("animationName");
            sound = serializedObject.FindProperty("sound");
            lifetime = serializedObject.FindProperty("lifetime");
            objIndex = serializedObject.FindProperty("objIndex");
            location = serializedObject.FindProperty("location");
            parent = serializedObject.FindProperty("parent");
            positionOffset = serializedObject.FindProperty("positionOffset");
            rotationOffset = serializedObject.FindProperty("rotationOffset");
            moveForwards = serializedObject.FindProperty("moveForwards");
            movementSpeed = serializedObject.FindProperty("movementSpeed");
            rotateTowardsTarget = serializedObject.FindProperty("rotateTowardsTarget");
            rotationSpeed = serializedObject.FindProperty("rotationSpeed");
            scale = serializedObject.FindProperty("scale");
            destroyOnCollision = serializedObject.FindProperty("destroyOnCollision");
            effectIds = serializedObject.FindProperty("effectIds");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(animationType);
            EditorGUILayout.PropertyField(startTime);
            EditorGUILayout.PropertyField(animationName);
            EditorGUILayout.PropertyField(sound);
            EditorGUILayout.PropertyField(lifetime);
            EditorGUILayout.PropertyField(objIndex);
            EditorGUILayout.PropertyField(location);
            EditorGUILayout.PropertyField(parent);
            EditorGUILayout.PropertyField(positionOffset);
            EditorGUILayout.PropertyField(rotationOffset);
            EditorGUILayout.PropertyField(moveForwards);
            EditorGUILayout.PropertyField(movementSpeed);
            EditorGUILayout.PropertyField(rotateTowardsTarget);
            EditorGUILayout.PropertyField(rotationSpeed);
            EditorGUILayout.PropertyField(scale);
            EditorGUILayout.PropertyField(destroyOnCollision);
            EditorGUILayout.PropertyField(effectIds);

            serializedObject.ApplyModifiedProperties();
        }
    }
}