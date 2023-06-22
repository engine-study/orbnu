using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "MUD/Components/Building", order = 1)]
public class MUDComponentBuilding : MUDComponent
{
    [Header("Building")]
    public string buildingName;
    public override void InitFromTable()
    {

    }
}
