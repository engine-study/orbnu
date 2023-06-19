using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MUDEntity : MonoBehaviour
{
    [Header("Entity")]
    public string mudKey;
    public bool isLocal;

    public void SetMudKey(string newKey) {
        mudKey = newKey;
    }

    public void SetIsLocal(bool newValue) {
        isLocal = newValue;
    }

}
