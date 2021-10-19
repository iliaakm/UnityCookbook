using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class LapTracker : MonoBehaviour
{
    [SerializeField]
    Transform target = null;
    [SerializeField]
    int longestPermittedShortcut = 2;
    [SerializeField]
    GameObject wrongWayIndicator;
    [SerializeField]
    Text lapCounter;

    int lapsComplete;
    Checkpoint lastSeenCheckpoint;
    Checkpoint[] allCheckpoints;

    Checkpoint StartCheckpoint
    {
        get
        {
            return FindObjectsOfType<Checkpoint>().Where(c => c.isLapStart).FirstOrDefault();
        }
    }

    private void Start()
    {
        UpdateLapCounter();
        wrongWayIndicator.SetActive(false);
        allCheckpoints = FindObjectsOfType<Checkpoint>();
        CreateCircuit();
        lastSeenCheckpoint = StartCheckpoint;
    }

    private void Update()
    {
        var nearestCheckpoint = NearestCheckpoint();
        if (nearestCheckpoint == null)
        {
            return;
        }

        if (nearestCheckpoint.index == lastSeenCheckpoint.index)
        {

        }
        else if (nearestCheckpoint.index > lastSeenCheckpoint.index)
        {
            var distance = nearestCheckpoint.index - lastSeenCheckpoint.index;
            if (distance > longestPermittedShortcut + 1)
            {
                wrongWayIndicator.SetActive(true);                
            }
            else
            {
                lastSeenCheckpoint = nearestCheckpoint;
                wrongWayIndicator.SetActive(false);
            }
        }
        else if(nearestCheckpoint.isLapStart && lastSeenCheckpoint.next.isLapStart)
        {
            lastSeenCheckpoint = nearestCheckpoint;
            lapsComplete++;
            UpdateLapCounter();
        }
        else
        {
            wrongWayIndicator.SetActive(true);
        }
    }

    Checkpoint NearestCheckpoint()
    {
        if(allCheckpoints == null)
        {
            return null;
        }

        Checkpoint nearestSoFar = null;
        float nearestDistanceSoFar = float.PositiveInfinity;

        for (int c = 0; c < allCheckpoints.Length; c++)
        {
            var checkpoint = allCheckpoints[c];
            var distance = (target.position - checkpoint.transform.position).sqrMagnitude;
            if(distance < nearestDistanceSoFar)
            {
                nearestSoFar = checkpoint;
                nearestDistanceSoFar = distance;
            }
        }

        return nearestSoFar;
    }

    void  CreateCircuit()
    {
        var index = 0;
        var currentCheckpoint = StartCheckpoint;

        do
        {
            currentCheckpoint.index = index;
            index++;
            currentCheckpoint = currentCheckpoint.next;

            if (currentCheckpoint == null)
            {
                Debug.LogError($"The circuit is not closec!");
                return;
            }
        } while (currentCheckpoint.isLapStart == false);
    }

    void UpdateLapCounter()
    {
        lapCounter.text = string.Format($"Lap {lapsComplete + 1}");
    }

    private void OnDrawGizmos()
    {
        var nearest = NearestCheckpoint();
        if(target != null && nearest != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(target.position, nearest.transform.position);
        }
    }
}
