using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public SoundEffect[] effects;
    private Dictionary<string, SoundEffect> _effectDictionary;
    private AudioListener _listener;

    private void Awake()
    {
        _effectDictionary = new Dictionary<string, SoundEffect>();
        foreach (var effect in effects)
        {
            print($"registed effect {effect.name}");
            _effectDictionary.Add(effect.name, effect);
        }
    }

    public void PlayEffect(string effectName)
    {
        if(_listener == null)
        {
            _listener = FindObjectOfType<AudioListener>();
        }

        PlayEffect(effectName, _listener.transform.position);
    }

    public void PlayEffect(string effectName, Vector3 worldPosition)
    {
        if(!_effectDictionary.ContainsKey(effectName))
        {
            Debug.LogWarning($"Effect {effectName} is  not registred");
            return;
        }

        var clip = _effectDictionary[effectName].GetRandomClip();
        if(clip == null)
        {
            Debug.LogWarning($"Effect {effectName} has no clips to play");
            return;
        }

        AudioSource.PlayClipAtPoint(clip, worldPosition);
    }
}
