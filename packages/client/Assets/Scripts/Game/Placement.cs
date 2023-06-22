using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Entity place;
    public GameObject valid, invalid;

    void Update() {
        UpdatePlacement();
        UpdateInput();
    }

    void UpdatePlacement() {

        transform.position = SPCursor.GridPos;

        if(place is Structure) {

        } else if (place is Ground) {

        } else if(place is Unit) {

        }
    }

    void UpdateInput() {
        if(Input.GetMouseButtonDown(0)) {
            
        }
    }
}
