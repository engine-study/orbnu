using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoUI : WindowEntity
{
    [Header("Info")]
    public SPHeading header;
    public SPHeading playerName, coordinate;
    public SPRawText text;

    public override void UpdateEntity(Entity newEntity)
    {
        base.UpdateEntity(newEntity);

        if(newEntity is Terrain) {

        } else if(newEntity is Structure) {

        }
    }
}
