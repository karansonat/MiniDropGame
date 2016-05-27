using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
//Holds data about tile layers.
public class TileLayer
{
    public string LayerTag;
    public string Layer;
    public int LayerLevel;
}
//The class that manage tile operations.
public class Tile : MonoBehaviour
{
    public int _row;
    public int _col;
    public List<TileLayer>  layers = new List<TileLayer>();
    
    private bool _isSelected;

    public void Init()
    {
        _isSelected = false;
    }
	
    void OnMouseOver()
    {
        if (UIHoverListener.Instance.isUIOverride) return;
        //TODO(sonat): Highlight Tile
        if (Input.GetMouseButtonDown(0))
        {
            SelectTile();
        } 
    }

    void OnMouseExit()
    {
        //TODO(sonat): Clear Highlight
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
            switch (tileLayer.LayerTag)
            {
                case "Tree":
                    break;
                case "Buildings":
                    layerObj.transform.localPosition = new Vector3(0, 0.1001f, 0);
                    break;
            }
        }
    }

    public void AddLayer(string name)
    {
        var parsedString = name.Split('_');
        var tileLayer = new TileLayer
        {
            Layer = name,
            LayerTag = parsedString[0],
            LayerLevel = int.Parse(parsedString[1])
        };
        for (var i = layers.Count - 1; i >= 0; i--)
        {
            if (layers[i].LayerTag == tileLayer.LayerTag)
            {
                layers.RemoveAt(i);
            }
        }
            
        layers.Add(tileLayer);
    }

    public void SelectTile()
    {
        GameController.Instance.SetSelectedTile(this);
        _isSelected = true;
        UIController.Instance.ShowTileMenu(this);
    }

    public void DeselectTile()
    {
        _isSelected = false;
    }
}
