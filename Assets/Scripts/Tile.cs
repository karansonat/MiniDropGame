using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TileLayer
{
    public string LayerTag;
    
}

public class Tile : MonoBehaviour
{
    public int _row;
    public int _col;
    
    private bool _isHighlighted;

    public void Init()
    {
        _isHighlighted = false;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnMouseEnter()
    {
        //Highlight Tile 
    }

    void OnMouseExit()
    {
        //Clear Highlight
    }

    public void UpdateTileVisual()
    {
        
    }
}
