using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUI : PhaseUI
{
    [Header("Action")]
    public StatUI stats;
    public InfoUI info;

    public override void ToggleWindow(bool toggle)
    {
        base.ToggleWindow(toggle);

    }

}
