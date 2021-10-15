using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectMouseInteraction
    : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    IPointerDownHandler,
    IPointerClickHandler
{
    private Material material;

    void  Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{gameObject.name} clicked!");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print($"{gameObject.name} pointer down!");

        material.color = Color.green;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print($"{gameObject.name} pointer enter!");

        material.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print($"{gameObject.name} pointer exit!");

        material.color = Color.white;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print($"{gameObject.name} pointer up!");

        material.color = Color.yellow;
    }
}