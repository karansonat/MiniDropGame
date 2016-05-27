using UnityEngine;
using System.Collections;

//Main game control class.
public class GameController : MonoSingleton<GameController>
{

    private Level _level;
    private Material _grassMaterial;
    private Material _higlightedGrassMaterial;

    public Tile _selectedTile;

	void Start () {
        _level = new Level();
	    LevelConfig.Instance.InitLevel();
        _grassMaterial = Resources.Load("Materials/Grass") as Material;
        _higlightedGrassMaterial = Resources.Load("Materials/HighlightedMaterial") as Material;
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
            UnHighlightTile();
            _selectedTile.DeselectTile();
        }
        _selectedTile = tile;
        HighlightTile();
    }

    private void HighlightTile()
    {
        var materials = _selectedTile.gameObject.GetComponent<MeshRenderer>().materials;
        materials[materials.Length - 1] = _higlightedGrassMaterial;
        _selectedTile.gameObject.GetComponent<MeshRenderer>().materials = materials;
    }

    private void UnHighlightTile()
    {
        var materials = _selectedTile.gameObject.GetComponent<MeshRenderer>().materials;
        materials[materials.Length - 1] = _grassMaterial;
        _selectedTile.gameObject.GetComponent<MeshRenderer>().materials = materials;
    }
}
