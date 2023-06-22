using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUDEntity : MonoBehaviour
{
    public string Key { get { return mudKey; } }
    [Header("MUD")]
    [SerializeField] protected string mudKey;
    [SerializeField] protected bool isLocal;
    [SerializeField] protected MUDComponent[] components;

    private mud.Unity.NetworkManager net;

    public void SetMudKey(string newKey)
    {
        mudKey = newKey;
    }

    public void SetIsLocal(bool newValue)
    {
        isLocal = newValue;
    }

    protected virtual void Start()
    {

        net = mud.Unity.NetworkManager.Instance;
        net.OnNetworkInitialized += Init;

    }

    protected virtual void OnDestroy()
    {
        if (net)
        {
            net.OnNetworkInitialized -= Init;
        }
    }

    protected virtual void Init(mud.Unity.NetworkManager nm)
    {
        for(int i = 0; i < components.Length; i++) {
            //copy the scriptable object (so we don't write to the original)
            components[i] = Instantiate(components[i]);
            components[i].Init(this);
        }
    
    }
}
