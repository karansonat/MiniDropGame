using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//This class responsible for initial works on Level Creation.
public class LevelConfig : MonoSingleton<LevelConfig>
{
    [Header("Level Config")]
    public int row;
    public int col;

    [Header("Prefabs")]
    public GameObject Tile;
    //TODO(sonat): Make this one private.
    public Level _level;

    private readonly Dictionary<string, int> _costs = new Dictionary<string, int>
    {
        {"Tree_1", 10},
        {"Tree_2", 20},
        {"Tree_3", 50},
        {"Buildings_1", 15},
        {"Buildings_2", 30},
        {"Buildings_3", 60}
    };

    private readonly Dictionary<string, int> _incomes = new Dictionary<string, int>
    {
        {"Tree_1", 5},
        {"Tree_2", 15},
        {"Tree_3", 30},
        {"Buildings_1", 10},
        {"Buildings_2", 25},
        {"Buildings_3", 50}
    };

    void Update () {
	
	}

    public void InitLevel()
    {
        InitLevelData();
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        var levelContainer = GameObject.Find("Level");
         //Find middle point of the board.
        var tileWidth = Tile.GetComponent<Renderer>().bounds.size.x;
        var topLeftCornerPos = new Vector3(-(tileWidth * col / 2), 0, (tileWidth * row / 2));

        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                var tile = Instantiate(Tile);
                tile.name = "Tile {" + r + " , " + c + "}";
                var tileComp = tile.GetComponent<Tile>();
                tileComp._row = r;
                tileComp._col = c;
                //Add it to level tiles.
                _level.tiles.Add(tileComp);
                tile.transform.SetParent(levelContainer.transform);
                //Set tile's position.
                tile.transform.localPosition = new Vector3(topLeftCornerPos.x + (c * tileWidth), 0, topLeftCornerPos.z + (r * -tileWidth));
            }
        }
        levelContainer.transform.position = Vector3.zero;
    }

    private void InitLevelData()
    {
        _level = new Level
        {
            Population = 0,
            CurrentWater = 50,
            DailyWaterIncome = 0,
            DailyWaterOutcome = 0
        };
    }

    public Level GetActiveLevel()
    {
        return _level;
    }

    public void ApplyLayerDataToLevel(string LayerTag, int LayerLevel)
    {
        var key = LayerTag + "_" + LayerLevel;
        switch (LayerTag)
        {
            case "Tree":
                _level.CurrentWater -= _costs[key];
                _level.DailyWaterIncome += _incomes[key];
                break;
            case "Buildings":
                _level.CurrentWater -= _costs[key];
                _level.Population =+ _incomes[key];
                break;
        }
    }
    

}
