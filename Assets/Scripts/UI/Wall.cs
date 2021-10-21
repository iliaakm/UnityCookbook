using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Wall : MonoBehaviour
{
    [SerializeField]
    public int rows = 5;
    [SerializeField]
    public int columns = 5;
    [SerializeField]
    public Renderer brickPrefab;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Wall))]
public class WallEditor: Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rows"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("columns"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("brickPrefab"));
        serializedObject.ApplyModifiedProperties();

        if(GUILayout.Button("Create Wall"))
        {
            CreateWall();
        }
    }

    private void CreateWall()
    {
        Undo.RegisterFullObjectHierarchyUndo(target, "Create Wall");

        var wall = target as Wall;
        if(wall == null)
        {
            return;
        }

        GameObject[] allChildren = new GameObject[wall.transform.childCount];
        int i = 0;

        foreach (Transform child in wall.transform)
        {
            allChildren[i] = child.gameObject;
            i++;
        }

        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child);
        }

        var brickSize = wall.brickPrefab.GetComponent<Renderer>().bounds.size;

        for (int row = 0; row < wall.rows; row++)
        {
            var rowPosition = Vector3.zero;
            rowPosition.y += brickSize.y * row;

            for (int collumn = 0; collumn < wall.columns; collumn++)
            {
                var collumnPosition = rowPosition;
                collumnPosition.x += brickSize.x * collumn;

                if(row % 2 == 0)
                {
                    collumnPosition.x += brickSize.x / 2f;
                }

                var brick = PrefabUtility.InstantiatePrefab(wall.brickPrefab.gameObject) as GameObject;
                brick.name = $"{wall.brickPrefab.name} ({collumn},{row})";
                brick.transform.SetParent(wall.transform, false);
                brick.transform.localPosition = collumnPosition;
                brick.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
#endif
