using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SkillSystem
{
    [CustomEditor(typeof(Skill))]
    public class SkillEditor : Editor
    {
        Skill skill;

        private void OnEnable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            skill = (Skill)target;

            if (skill.animationsFoldouts == null || skill.animationsFoldouts.Count != skill.animations.Count) {
                skill.animationsFoldouts = new List<bool>(new bool[skill.animations.Count]);
            }

            serializedObject.Update();

            skill.skillName = EditorGUILayout.TextField("Skill Name", skill.skillName);
            skill.targetType = (SkillTargetType)EditorGUILayout.EnumPopup("Target Type", skill.targetType);

            //EditorGUILayout.Foldout();
            List<SkillAnimation> animations = skill.animations;
            List<bool> animationFoldouts = skill.animationsFoldouts;
            for (int i = 0; i < animations.Count; i++) {
                SkillAnimation anim = animations[i];
                animationFoldouts[i] = EditorGUILayout.BeginFoldoutHeaderGroup(animationFoldouts[i], 
                                                                               $"{anim.startTime} - {anim.animationType.ToString()}");
                if (animationFoldouts[i])
                {
                    EditorGUI.indentLevel++;
                    SerializeAnimation(animations[i]);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            int deleteIndex = -1;

            if (skill.skillObjects != null) {
                for (int i = 0; i < skill.skillObjects.Count; i++) {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"Skill Object {i}");

                    if (GUILayout.Button("Remove Object")) {
                        deleteIndex = i;
                        break;
                    }
                    skill.skillObjects[i] = (GameObject)EditorGUILayout.ObjectField(skill.skillObjects[i], typeof(GameObject));
                    EditorGUILayout.EndHorizontal();
                }
            }

            if (deleteIndex >= 0) {
                skill.skillObjects.RemoveAt(deleteIndex);
            }

            if (GUILayout.Button("Add Object")) {
                skill.skillObjects.Add(null);
            }
            
        }

        private void SerializeAnimation(SkillAnimation animation) {
            SerializedProperty animationType = serializedObject.FindProperty("animationType");
            animation.animationType = (SkillAnimationType)EditorGUILayout.EnumPopup("Animation Type", animation.animationType);
            animation.startTime = EditorGUILayout.Slider("Start Time", animation.startTime, 0, 10);

            switch (animation.animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    animation.animationName = EditorGUILayout.TextField("Animation Name", animation.animationName);
                    break;
                case SkillAnimationType.PlaySound:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Sound");
                    EditorGUILayout.ObjectField(animation.sound, typeof(AudioClip), true);
                    EditorGUILayout.EndHorizontal();
                    break;
                case SkillAnimationType.CreateObject:
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Skill Gameobject");

                    if (skill.skillObjects != null && skill.skillObjects.Count > 0)
                    {
                        int numOfObj = skill.skillObjects.Count;
                        GameObject obj = skill.GetGameObject(animation.objIndex);
                        string objName = obj == null ? "N/A" : obj.name;
                        EditorGUILayout.TextField(objName);
                    }
                    else {
                        EditorGUILayout.TextField("N/A");
                    }
                    EditorGUILayout.EndHorizontal();

                    animation.objIndex = EditorGUILayout.IntSlider(animation.objIndex, 0, skill.animations.Count - 1);

                    animation.lifetime = EditorGUILayout.Slider("Lifetime", animation.lifetime, -1, 10);

                    animation.lifetime = animation.lifetime < 0 && animation.lifetime > -1 ? 0 : animation.lifetime;
                    animation.location = (SkillObjectLocation)EditorGUILayout.EnumPopup("Location", animation.location);
                    animation.parent = (SkillObjectParent)EditorGUILayout.EnumPopup("Parent", animation.parent);

                    animation.positionOffset = EditorGUILayout.Vector3Field("Position Offset", animation.positionOffset);
                    animation.rotationOffset = EditorGUILayout.Vector3Field("Rotation Offset", animation.rotationOffset);

                    animation.moveForwards = EditorGUILayout.Toggle("Move Forwards", animation.moveForwards);
                    if (animation.moveForwards)
                    {
                        EditorGUI.indentLevel++;
                        animation.movementSpeed = EditorGUILayout.FloatField("Movement Speed", animation.movementSpeed);
                        EditorGUI.indentLevel--;
                    }


                    animation.rotateTowardsTarget = EditorGUILayout.Toggle("Rotate Towards Enemy", animation.rotateTowardsTarget);
                    if (animation.rotateTowardsTarget)
                    {
                        EditorGUI.indentLevel++;
                        animation.rotationSpeed = EditorGUILayout.FloatField("Rotation Speed", animation.rotationSpeed);
                        EditorGUI.indentLevel--;
                    }

                    animation.scale = EditorGUILayout.FloatField("Object Scale", animation.scale);

                    animation.destroyOnCollision = EditorGUILayout.Toggle("Destroy On Collision", animation.destroyOnCollision);
                    break;
            }
            
            
            //use string for this
            //animation.effectIds;
            

        }

        private void SetupObjectMenu(GenericMenu menu, string menuPath, Skill skill, SkillAnimation anim, int index) {
            menu.AddItem(new GUIContent($"Skill Objects/{skill.GetGameObject(index).name}"), 
                         anim.objIndex == index, 
                         (object x) => SelectNumber(anim, x), 
                         skill.GetGameObject(index));
        }

        private void SelectNumber(SkillAnimation anim, object num) {
            int objInd = (int)num;
            anim.objIndex = objInd;
        }



        private void AddObject()
        {
            if (skill.skillObjects == null)
            {
                skill.skillObjects = new List<GameObject>();
            }
            skill.skillObjects.Add(null);
        }
    }
}