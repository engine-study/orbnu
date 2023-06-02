using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public virtual void Generate() {
        Debug.Log("Generating: " + gameObject.name  + " " + this.name, this);

    }
}
