using UnityEngine;
using System.Collections;

//Main game control class.
public class GameController : MonoSingleton<GameController>
{
    private Material _grassMaterial;
    private Material _higlightedGrassMaterial;
    private GameObject _cameraPivot;


    private Vector3 _mousePosOld = Vector3.zero;

    public Tile _selectedTile;

	void Start () {
        
	    LevelConfig.Instance.InitLevel();
        _cameraPivot = GameObject.Find("CameraPivot");
        _grassMaterial = Resources.Load("Materials/Grass") as Material;
        _higlightedGrassMaterial = Resources.Load("Materials/HighlightedMaterial") as Material;
    }
	
	void FixedUpdate ()
	{
	    TurnCamera();
        //Clear selection when user clicked outside to map.
	    if (Input.GetMouseButtonDown(0))
	    {
            if(UIHoverListener.Instance.isUIOverride) return;
	        if (UIController.Instance.AddDialogueObj.isActive)
	        {
                UIController.Instance.AddDialogueObj.ToogleMenu();
            }
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit))
            {
                if (_selectedTile)
                {
                    _selectedTile.DeselectTile();
                }
            }
        }
    }
    
    public void SetSelectedTile(Tile tile)
    {
        if (_selectedTile)
        {
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
        UIController.Instance.TileDescriptionObj.ShowPanel();
    }

    public void UnHighlightTile()
    {
        var materials = _selectedTile.gameObject.GetComponent<MeshRenderer>().materials;
        materials[materials.Length - 1] = _grassMaterial;
        _selectedTile.gameObject.GetComponent<MeshRenderer>().materials = materials;
        UIController.Instance.TileDescriptionObj.HidePanel();
    }

    public void EndDay()
    {
        var level = LevelConfig.Instance.GetActiveLevel();
        level.DayCount++;
        level.CurrentWater += level.DailyWaterIncome - level.DailyWaterOutcome;
        UIController.Instance.HUDControllerObj.UpdateHUD();
        UIController.Instance.EndDayScreemObj.ShowEndDayReport();
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
