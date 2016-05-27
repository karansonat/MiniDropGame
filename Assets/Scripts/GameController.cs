using UnityEngine;
using System.Collections;

//Main game control class.
public class GameController : MonoSingleton<GameController>
{

    private Level _level;
    private Material _grassMaterial;
    private Material _higlightedGrassMaterial;
    private GameObject _cameraPivot;


    private Vector3 _mousePosOld = Vector3.zero;

    public Tile _selectedTile;

	void Start () {
        _level = new Level();
	    LevelConfig.Instance.InitLevel();
        _cameraPivot = GameObject.Find("CameraPivot");
        _grassMaterial = Resources.Load("Materials/Grass") as Material;
        _higlightedGrassMaterial = Resources.Load("Materials/HighlightedMaterial") as Material;
    }
	
	void FixedUpdate ()
	{
	    TurnCamera();
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

    private void TurnCamera()
    {
        if (Input.GetMouseButton(1))
        {
            if (_mousePosOld == Input.mousePosition) return;
            if (_mousePosOld == Vector3.zero)
            {
                _mousePosOld = Input.mousePosition;
                return;
            }
            var diff = Input.mousePosition.x - _mousePosOld.x;
            _cameraPivot.transform.Rotate(Vector3.up, CalculateTurnAngle(diff));
            _mousePosOld = Input.mousePosition;
            /*
            if (diff > 0)
            {
                _cameraPivot.transform.Rotate(Vector3.up, 3.0f);
                _mousePosOld = Input.mousePosition;
            }
            else
            {
                _cameraPivot.transform.Rotate(Vector3.up, -3.0f);
                _mousePosOld = Input.mousePosition;
            }*/
        }
        if (Input.GetMouseButtonUp(1))
        {
            _mousePosOld = Vector3.zero;
        }
    }

    private float CalculateTurnAngle(float diff)
    {
        var width = (float)Screen.width;
        var perPixelAngle = 360.0f / width;
        return (perPixelAngle*diff) / 2;
        
    }
}
