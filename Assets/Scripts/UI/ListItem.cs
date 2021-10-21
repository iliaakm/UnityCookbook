using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItem : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Text labelText;

    public string Label
    {
        get
        {
            return labelText.text;
        }
        set
        {
            labelText.text = value;
        }
    }
}
