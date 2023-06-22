using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;

[CreateAssetMenu(fileName = "Stats", menuName = "MUD/Components/Stats", order = 1)]
public class StatsMUD : MUDComponent
{
    [Header("Stats")]
    public int health;
    public int attack;
    public int energy;

    public override void UpdateFromTable() {
        
        var tableValue = StatsTable.GetTableValue(Entity.Key);
        if (tableValue == null)
        {
            Debug.LogError("No stats on " + entity.name);
            return;
        }


        var newHealth = tableValue.health;
        var newAttack = tableValue.attack;

        health = (int)newHealth;
        attack = (int)newAttack;
    }

}
