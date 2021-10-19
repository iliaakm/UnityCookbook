using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    public bool isLapStart;
    [SerializeField]
    public Checkpoint next;
    internal int index = 0;

    private void OnDrawGizmos()
    {
        if(isLapStart)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.blue;
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
        if(next)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Checkpoint))]
public class CheckPointEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var checkpoint = this.target as Checkpoint;

        if(GUILayout.Button("Insert Checkpoint"))
        {
            var newCheckpoint = new GameObject("Checkpoint").AddComponent<Checkpoint>();
            //newCheckpoint.next = checkpoint.next;
            checkpoint.next = newCheckpoint;
            newCheckpoint.transform.SetParent(checkpoint.transform.parent, true);
            var nextSiblingIndex = checkpoint.transform.GetSiblingIndex() + 1;
            newCheckpoint.transform.SetSiblingIndex(nextSiblingIndex);
            newCheckpoint.transform.position = checkpoint.transform.position + new Vector3(1, 0, 0);
            Selection.activeGameObject = newCheckpoint.gameObject;
        }

        var disableRemoveButton = checkpoint.next == null || checkpoint.next.isLapStart;

        using(new EditorGUI.DisabledGroupScope(disableRemoveButton))
        {
            if(GUILayout.Button("Remove Next Checkpoint"))
            {
                var next = checkpoint.next.next;
                DestroyImmediate(checkpoint.next.gameObject);
                checkpoint.next = next;
            }
        }
    }
}
#endif
