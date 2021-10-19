using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSelectable : MonoBehaviour
{
    public void Selected()
    {
        print($"{gameObject.name} was selected!");
    }
}
