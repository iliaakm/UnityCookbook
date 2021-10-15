using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveableBehavior : MonoBehaviour, ISavable, ISerializationCallbackReceiver
{
    public abstract JsonData SavedData { get; }
    public abstract void LoadFromData(JsonData data);

    public string SaveID
    {
        get
        {
            return _saveID;
        }
        set
        {
            _saveID = value;
        }
    }

    [HideInInspector]
    [SerializeField]
    private string _saveID;

    public void OnBeforeSerialize()
    {
        if(_saveID == null)
        {
            _saveID = System.Guid.NewGuid().ToString();
        }
    }
    public void OnAfterDeserialize()
    {

    }
}
