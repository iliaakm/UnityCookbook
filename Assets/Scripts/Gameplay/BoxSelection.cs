using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelection : MonoBehaviour
{
    public RectTransform selectionBox;
    public Rect SelectionRect { get; private set; }
    public bool IsSelecting
    {
        get; private set;
    }
    private Vector2 initialClickPosition = Vector2.zero;

    private void Start()
    {
        selectionBox.anchorMin = Vector2.zero;
        selectionBox.anchorMax = Vector2.zero;
        selectionBox.pivot = Vector2.zero;

        selectionBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialClickPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            selectionBox.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            var xMin = Mathf.Min(currentMousePosition.x, initialClickPosition.x);
            var xMax = Mathf.Max(currentMousePosition.x, initialClickPosition.x);
            var yMin = Mathf.Min(currentMousePosition.y, initialClickPosition.y);
            var yMax = Mathf.Max(currentMousePosition.y, initialClickPosition.y);

            var screenSpaceRect = Rect.MinMaxRect(xMin, yMin, xMax, yMax);
            selectionBox.anchoredPosition = screenSpaceRect.position;
            selectionBox.sizeDelta = screenSpaceRect.size;
            SelectionRect = screenSpaceRect;
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectionComplete();
            selectionBox.gameObject.SetActive(false);
            IsSelecting = false;
        }
    }

    private void SelectionComplete()
    {
        Camera mainCamera = GetComponent<Camera>();
        var min = mainCamera.ScreenToViewportPoint(new Vector3(SelectionRect.xMin, SelectionRect.yMin));
        var max = mainCamera.ScreenToViewportPoint(new Vector3(SelectionRect.xMax, SelectionRect.yMax));

        min.z = mainCamera.nearClipPlane;
        max.z = mainCamera.farClipPlane;

        var viewportBounds = new Bounds();
        viewportBounds.SetMinMax(min, max);

        foreach (var selectable in FindObjectsOfType<BoxSelectable>())
        {
            var selectedPosition = selectable.transform.position;
            var viewportPoint = mainCamera.WorldToViewportPoint(selectedPosition);

            var selected = viewportBounds.Contains(viewportPoint);
            if(selected)
            {
                selectable.Selected();
            }
        }
    }
}
