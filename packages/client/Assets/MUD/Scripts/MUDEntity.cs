using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUDEntity : MonoBehaviour
{
    public string Key { get { return mudKey; } }
    public  MUDComponent[] Components { get { return components; } }
    public static Dictionary<string, MUDEntity> Entities;

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

    protected virtual void Awake() {
        if(Entities == null) {
            Entities = new Dictionary<string, MUDEntity>();
        }
    }

        
    public MUDComponent GetMUDComponent(MUDComponent componentType) {

        for(int i = 0; i < Components.Length; i++) {
            if(Components[i].GetType() == componentType.GetType())
                return Components[i];
        }

        return null;
    }

    public static MUDEntity GetEntity(string Key) {return Entities[Key];}
    public static MUDEntity GetEntitySafe(string Key) {MUDEntity e; Entities.TryGetValue(Key, out e); return e;}
    public static void ToggleEntity(bool toggle, MUDEntity entity) {

        if(toggle) {
            Entities.Add(entity.Key, entity);
        } else {
            Entities.Remove(entity.Key);
        }
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
