using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : PhaseUI
{
    [Header("Build")]
    public GameObject worldUI;
    public StatUI stats;
    public InfoUI info;

    public override void ToggleWindow(bool toggle)
    {
        base.ToggleWindow(toggle);

        worldUI.SetActive(toggle);

    }

    public override void UpdatePhase() {
        
    }
}
