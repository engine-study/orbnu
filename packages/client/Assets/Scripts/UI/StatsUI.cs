using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUI : WindowEntity
{
    [Header("Stats")]
    public StatUI healthStat;
    public StatUI attackStat;
    public TextMeshProUGUI nameText;

    public override void UpdateEntity(Entity newEntity)
    {
        base.UpdateEntity(newEntity);

        Structure structure = newEntity as Structure;
        if(structure) {
            healthStat.SetValue(structure.stats.health.ToString());
            attackStat.SetValue(structure.stats.attack.ToString());
            nameText.text = structure.stats.objectName;
        } else {
            ToggleWindowClose();
        }
    }
}
