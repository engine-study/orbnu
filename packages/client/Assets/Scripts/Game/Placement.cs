using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Entity place;
    public GameObject valid, invalid;

    void Update() {

    }

    void UpdatePlacement() {
        if(place is Structure) {

        } else if (place is Terrain) {

        } else if(place is Unit) {
            
        }
    }
}
