using UnityEngine;
using UnityEditor;
using System;
 
[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var myScript = target as EnemyManager;

        using (new EditorGUI.DisabledScope(myScript.disableBool))
        {
            //myScript.someColor = EditorGUILayout.ColorField("Color", myScript.someColor);
            //myScript.someString = EditorGUILayout.TextField("Text", myScript.someString);
            myScript.scoreValue = EditorGUILayout.IntField("Score value", myScript.scoreValue);
            myScript.speed = EditorGUILayout.FloatField("Speed", myScript.speed);
            EditorGUILayout.Space();

            myScript.EnemyType = (EnemyManager.Type)EditorGUILayout.EnumPopup("Type", myScript.EnemyType);
            switch (myScript.EnemyType)
            {
                case EnemyManager.Type.neutral:
                    myScript.bulletCD = EditorGUILayout.FloatField("Shoot cooldown in millisecond", myScript.bulletCD);
                    break;
                case EnemyManager.Type.solid:

                    break;
                case EnemyManager.Type.gaseous:
                    myScript.laserTickCD = EditorGUILayout.FloatField("Laser tick cooldown", myScript.laserTickCD);
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }

            EditorGUILayout.Space();
            myScript.pattern = (EnemyManager.Pattern)EditorGUILayout.EnumPopup("Pattern", myScript.pattern);
            switch (myScript.pattern)
            {
                case EnemyManager.Pattern.MarcheAvant:

                    break;
                case EnemyManager.Pattern.Diagonale:
                    myScript.diagonaleAngle = EditorGUILayout.FloatField("Angle", myScript.diagonaleAngle);
                    break;
                case EnemyManager.Pattern.Stationnaire:
                    myScript.stationnaireDistance = EditorGUILayout.FloatField("Stop distance", myScript.stationnaireDistance);
                    myScript.stationnaireTime = EditorGUILayout.FloatField("Stop time", myScript.stationnaireTime);
                    break;
                case EnemyManager.Pattern.ZigZag:
                    myScript.zigzagFrequency = EditorGUILayout.FloatField("Zigzag frequency", myScript.zigzagFrequency);
                    myScript.zigzagMagnitude = EditorGUILayout.FloatField("ZigZag amplitude", myScript.zigzagMagnitude);
                    break;
                case EnemyManager.Pattern.Roue:
                    myScript.roueRadius = EditorGUILayout.FloatField("Radius", myScript.roueRadius);
                    break;
                case EnemyManager.Pattern.ZigZagSharp:
                    myScript.zigzagsharpRange = EditorGUILayout.FloatField("Angle", myScript.zigzagsharpRange);
                    myScript.zigzagsharpDistance = EditorGUILayout.FloatField("Distance", myScript.zigzagsharpDistance);
                    myScript.zigzagsharpTime = EditorGUILayout.FloatField("Stop time", myScript.zigzagsharpTime);
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }

            EditorGUILayout.Space();

            if (myScript.bulletPrefab == null)
            {
                myScript.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", myScript.bulletPrefab, typeof(GameObject), true);
            }
            if (myScript.droppedBonus == null)
            {
                myScript.droppedBonus = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", myScript.droppedBonus, typeof(GameObject), true);
            }
            if (myScript.neutral == null)
            {
                myScript.neutral = (Sprite)EditorGUILayout.ObjectField("Neutral sprite", myScript.neutral, typeof(Sprite), true);
            }
            if (myScript.solid == null)
            {
                myScript.solid = (Sprite)EditorGUILayout.ObjectField("Solid sprite", myScript.solid, typeof(Sprite), true);
            }
            if (myScript.gazeous == null)
            {
                myScript.gazeous = (Sprite)EditorGUILayout.ObjectField("Gazeous sprite", myScript.gazeous, typeof(Sprite), true);
            }
            if (myScript.spriteRenderer == null)
            {
                myScript.spriteRenderer = (SpriteRenderer)EditorGUILayout.ObjectField("Gazeous sprite", myScript.spriteRenderer, typeof(SpriteRenderer), true);
            }
        }
    }
}