//using UnityEngine;
//using UnityEditor;
//using System;
 
//[CustomEditor(typeof(EnemyManager))]
//[CanEditMultipleObjects]
//public class EnemyManagerEditor : Editor
//{
//    SerializedProperty hp;
//    SerializedProperty scoreValue;
//    SerializedProperty speed;
//    SerializedProperty enemyType;
//    SerializedProperty bulletCD;
//    SerializedProperty laserTickCD;
//    SerializedProperty laserRange;
//    SerializedProperty pattern;
//    SerializedProperty diagonaleAngle;
//    SerializedProperty stationnaireDistance;
//    SerializedProperty stationnaireTime;
//    SerializedProperty zigzagFrequency;
//    SerializedProperty zigzagMagnitude;
//    SerializedProperty roueRadius;
//    SerializedProperty zigzagsharpRange;
//    SerializedProperty zigzagsharpDistance;
//    SerializedProperty zigzagsharpTime;
//    SerializedProperty bulletPrefab;
//    SerializedProperty laserDamage;
//    SerializedProperty contactDamage;
//    SerializedProperty isRedInk;

//    private void OnEnable()
//    {
//        hp = serializedObject.FindProperty("hp");
//        scoreValue = serializedObject.FindProperty("scoreValue");
//        speed = serializedObject.FindProperty("speed");
//        enemyType = serializedObject.FindProperty("EnemyType");
//        bulletCD = serializedObject.FindProperty("bulletCD");
//        laserTickCD = serializedObject.FindProperty("laserTickCD");
//        laserRange = serializedObject.FindProperty("laserRange");
//        pattern = serializedObject.FindProperty("pattern");
//        diagonaleAngle = serializedObject.FindProperty("diagonaleAngle");
//        stationnaireDistance = serializedObject.FindProperty("stationnaireDistance");
//        stationnaireTime = serializedObject.FindProperty("stationnaireTime");
//        zigzagFrequency = serializedObject.FindProperty("zigzagFrequency");
//        zigzagMagnitude = serializedObject.FindProperty("zigzagMagnitude");
//        roueRadius = serializedObject.FindProperty("roueRadius");
//        zigzagsharpRange = serializedObject.FindProperty("zigzagsharpRange");
//        zigzagsharpDistance = serializedObject.FindProperty("zigzagsharpDistance");
//        zigzagsharpTime = serializedObject.FindProperty("zigzagsharpTime");
//        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
//        laserDamage = serializedObject.FindProperty("laserDamage");
//        contactDamage = serializedObject.FindProperty("contactDamage");
//        isRedInk = serializedObject.FindProperty("isRedInk");
//    }
//    override public void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        EditorGUILayout.PropertyField(hp);
//        EditorGUILayout.PropertyField(scoreValue);
//        EditorGUILayout.PropertyField(speed);
//        EditorGUILayout.PropertyField(contactDamage);
//        EditorGUILayout.Space();

//        EditorGUILayout.PropertyField(enemyType);

//        var myScript = target as EnemyManager;

//        using (new EditorGUI.DisabledScope(myScript.disableBool))
//        {
//            //myScript.scoreValue = EditorGUILayout.IntField("Score value", myScript.scoreValue);
//            //myScript.speed = EditorGUILayout.FloatField("Speed", myScript.speed);
//            //EditorGUILayout.Space();

//            //myScript.EnemyType = (EnemyManager.Type)EditorGUILayout.EnumPopup("Type", myScript.EnemyType);
//            switch (myScript.EnemyType)
//            {
//                case EnemyManager.Type.neutral:
//                    EditorGUILayout.PropertyField(isRedInk);
//                    EditorGUILayout.PropertyField(bulletCD);
//                    //myScript.bulletCD = EditorGUILayout.FloatField("Shoot cooldown in millisecond", myScript.bulletCD);
//                    break;
//                case EnemyManager.Type.solid:

//                    break;
//                case EnemyManager.Type.gaseous:
//                    EditorGUILayout.PropertyField(laserTickCD);
//                    EditorGUILayout.PropertyField(laserDamage);
//                    EditorGUILayout.PropertyField(laserRange);
//                    //myScript.laserTickCD = EditorGUILayout.FloatField("Laser tick cooldown", myScript.laserTickCD);
//                    //myScript.laserRange = EditorGUILayout.FloatField("Laser range", myScript.laserRange);
//                    break;
//                default:
//                    Debug.Log("Error");
//                    break;
//            }

//            EditorGUILayout.Space();
//            EditorGUILayout.PropertyField(pattern);
//            //myScript.pattern = (EnemyManager.Pattern)EditorGUILayout.EnumPopup("Pattern", myScript.pattern);
//            switch (myScript.pattern)
//            {
//                case EnemyManager.Pattern.MarcheAvant:

//                    break;
//                case EnemyManager.Pattern.Diagonale:
//                    EditorGUILayout.PropertyField(diagonaleAngle);
//                    //myScript.diagonaleAngle = EditorGUILayout.FloatField("Angle", myScript.diagonaleAngle);
//                    break;
//                case EnemyManager.Pattern.Stationnaire:
//                    EditorGUILayout.PropertyField(stationnaireDistance);
//                    EditorGUILayout.PropertyField(stationnaireTime);
//                    //myScript.stationnaireDistance = EditorGUILayout.FloatField("Stop distance", myScript.stationnaireDistance);
//                    //myScript.stationnaireTime = EditorGUILayout.FloatField("Stop time", myScript.stationnaireTime);
//                    break;
//                case EnemyManager.Pattern.ZigZag:
//                    EditorGUILayout.PropertyField(zigzagFrequency);
//                    EditorGUILayout.PropertyField(zigzagMagnitude);
//                    //myScript.zigzagFrequency = EditorGUILayout.FloatField("Zigzag frequency", myScript.zigzagFrequency);
//                    //myScript.zigzagMagnitude = EditorGUILayout.FloatField("ZigZag amplitude", myScript.zigzagMagnitude);
//                    break;
//                case EnemyManager.Pattern.Roue:
//                    EditorGUILayout.PropertyField(roueRadius);
//                    //myScript.roueRadius = EditorGUILayout.FloatField("Radius", myScript.roueRadius);
//                    break;
//                case EnemyManager.Pattern.ZigZagSharp:
//                    EditorGUILayout.PropertyField(zigzagsharpRange);
//                    EditorGUILayout.PropertyField(zigzagsharpDistance);
//                    EditorGUILayout.PropertyField(zigzagsharpTime);
//                    //myScript.zigzagsharpRange = EditorGUILayout.FloatField("Angle", myScript.zigzagsharpRange);
//                    //myScript.zigzagsharpDistance = EditorGUILayout.FloatField("Distance", myScript.zigzagsharpDistance);
//                    //myScript.zigzagsharpTime = EditorGUILayout.FloatField("Stop time", myScript.zigzagsharpTime);
//                    break;
//                default:
//                    Debug.Log("Error");
//                    break;
//            }

//            EditorGUILayout.Space();

//            if (myScript.bulletPrefab == null)
//            {
//                EditorGUILayout.PropertyField(bulletPrefab);
//                //myScript.bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", myScript.bulletPrefab, typeof(GameObject), true);
//            }
//            //if (myScript.droppedBonus == null)
//            //{
//            //    myScript.droppedBonus = (GameObject)EditorGUILayout.ObjectField("Bullet prefab", myScript.droppedBonus, typeof(GameObject), true);
//            //}
//        }
//        serializedObject.ApplyModifiedProperties();
//    }
//}