using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
//Holds data about tile layers.
public class TileLayer
{
    public string LayerTag;
    public string Layer;
}
//The class that manage tile operations.
public class Tile : MonoBehaviour
{
    public int _row;
    public int _col;
    public List<TileLayer>  layers = new List<TileLayer>();
    
    private bool _isHighlighted;

    public void Init()
    {
        _isHighlighted = false;
    }
	
    void OnMouseEnter()
    {
        //Highlight Tile
        if (Input.GetMouseButtonDown(0))
        {
            GameController.Instance.SetSelectedTile(this);
        } 
    }

    void OnMouseExit()
    {
        //Clear Highlight
    }

    public void UpdateTileVisual()
    {
        //Delete all childs of tile.
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        //Create layer objects and set as child.
        foreach (var tileLayer in layers)
        {
            var layerObj = ContentLoader.Instance.GetGameObjectByPrefabName(tileLayer.Layer);
            layerObj.transform.SetParent(transform, false);
            //TODO(sonat): Set child layers positions here.
        }
    }

    public void AddLayer(string layerTag, string layer)
    {
        layers.Add(new TileLayer {LayerTag = layerTag, Layer = layer});
    }
}
