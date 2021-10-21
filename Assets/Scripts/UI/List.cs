using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{
    [SerializeField]
    int itemCount = 5;
    [SerializeField]
    ListItem itemPrefab;
    [SerializeField]
    RectTransform itemContainer;

    private void Start()
    {
        for (int i = 0; i < itemCount; i++)
        {
            var label = $"Item {i}";
            CreateNewListItem(label);
        }
    }

    public void CreateNewListItem(string label)
    {
        var newItem = Instantiate(itemPrefab);
        newItem.transform.SetParent(itemContainer, worldPositionStays: true);
        newItem.Label = label;
    }
}
