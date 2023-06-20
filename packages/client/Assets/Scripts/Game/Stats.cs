using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Engine/Stats/Stat", order = 1)]
public class Stats : ScriptableObject
{
    public int health;
    public int energy; 
    public int attack;
    public Effect [] effects;
    
}
