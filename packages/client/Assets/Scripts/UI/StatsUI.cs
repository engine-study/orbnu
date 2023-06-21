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

        healthStat.SetValue(newEntity.stats.health.ToString());
        attackStat.SetValue(newEntity.stats.attack.ToString());
        nameText.text = newEntity.stats.objectName;

        if(newEntity is Structure) {
            Structure structure = newEntity as Structure;

        } else if(newEntity is Resource) {
            Resource resource = newEntity as Resource;

        } else if (newEntity is Unit) {
            Unit unit = newEntity as Unit;

        } else {
            ToggleWindowClose();
        }
    }
}
