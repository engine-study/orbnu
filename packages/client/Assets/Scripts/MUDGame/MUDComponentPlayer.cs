
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "MUD/Components/Player", order = 1)]
public class MUDComponentPlayer : MUDComponent
{
    [Header("Player")]
    public bool spawned;
    public override void InitFromTable()
    {
        throw new System.NotImplementedException();
    }
}