using UnityEngine;
using System.Collections;

//Main game control class.
public class GameController : MonoSingleton<GameController>
{

    private Level _level;
    private Tile _selectedTile;

	void Start () {
        _level = new Level();
	    LevelConfig.Instance.InitLevel();
	}
	
	void Update () {
	
	}

    public Level GetActiveLevel()
    {
        return _level;
    }

    public void SetSelectedTile(Tile tile)
    {
        if (_selectedTile)
        {
            _selectedTile.DeselectTile();
        }
        _selectedTile = tile;
    }
}
