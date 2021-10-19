using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerObjectiveOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    ObjectiveTrigger objective = new ObjectiveTrigger();

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        objective.Invoke();
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
