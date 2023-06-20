using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorUI : MonoBehaviour
{
    [Header("Cursor")]
    public PlacementUI placement;
    public StatsUI stats;

    void Start() {
        
        SPCursor.OnHover += UpdateHover;
    }

    void OnDestroy() {
        SPCursor.OnHover -= UpdateHover;
    }

    void UpdateHover(Entity newEntity) {
        
        if(newEntity == null) {
            stats.ToggleWindow(false);
        } else {
            stats.ToggleWindow(true);
            stats.UpdateEntity(newEntity);
        }

    }



}
