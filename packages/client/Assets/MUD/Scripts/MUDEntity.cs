using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUDEntity : MonoBehaviour
{
    public string Key{get{return mudKey;}}
    [Header("MUD")]
    [SerializeField] protected string mudKey;
    [SerializeField] protected bool isLocal;
    [SerializeField] protected MUDComponent [] components;

    public void SetMudKey(string newKey) {
        mudKey = newKey;
    }

    public void SetIsLocal(bool newValue) {
        isLocal = newValue;
    }

    protected virtual void Start() {
        foreach(MUDComponent c in components) {
            c.Init(this);
        }
    }
}
