using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObject : MonoBehaviour
{
    void Start()
    {
        IndicatorManager.manager.AddTrackingIndicator(this);
    }

    // Update is called once per frame
    void OnDestoy()
    {
        IndicatorManager.manager.RemoveTrackingIndicator(this);
    }
}
