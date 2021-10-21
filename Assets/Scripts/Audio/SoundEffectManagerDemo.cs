using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManagerDemo : MonoBehaviour
{
        // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayEffect("Sound EffectHit");
    }  
}
