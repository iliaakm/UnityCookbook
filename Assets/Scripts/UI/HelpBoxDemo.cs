using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelpBoxDemo : MonoBehaviour
{
    [HelpBox(text = "Here's a help box above the variable!")]
    [SerializeField]
    int integerVal;
}
