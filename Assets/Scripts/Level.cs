using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
//Level class holds data of level.
public class Level
{
    public int Population;
    public int CurrentWater;
    public int DailyWaterIncome;
    public int DailyWaterOutcome;

    public List<Tile> tiles = new List<Tile>();

}
