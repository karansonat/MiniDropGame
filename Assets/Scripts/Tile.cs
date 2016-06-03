using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
[System.Serializable]
//Holds data about tile layers.
public class TileLayer
{
    public string LayerTag;
    public string Layer;
    public int LayerLevel;
}
[System.Serializable]
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
            
            switch (tileLayer.LayerTag)
            {
                case "Tree":
                    layerObj.transform.localPosition = new Vector3(0, 0.1001f, 0);
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

        //Player can not add buildings layer if tile has tree layer.
        if (layers.Any(layer => layer.LayerTag == "Tree") && tileLayer.LayerTag == "Buildings") return;

        for (var i = layers.Count - 1; i >= 0; i--)
        {
            if (layers[i].LayerTag == tileLayer.LayerTag)
            {
                LevelConfig.Instance.RemoveLayerDataFromLevel(layers[i].LayerTag, layers[i].LayerLevel, true);
                layers.RemoveAt(i);
            }
        }

        layers.Add(tileLayer);

        //This means tile has Tree and Buildings layer. So change Tree prefab with BuildingsTree prefab
        if (layers.Count == 2)
        {
            foreach (var layer in layers.Where(layer => layer.LayerTag == "Tree"))
            {
                layer.Layer = "BuildingsTree" + "_" + layer.LayerLevel;
            }
        }

        LevelConfig.Instance.AddLayerDataToLevel(tileLayer.LayerTag, tileLayer.LayerLevel);
    }

    public void SelectTile()
    {
        GameController.Instance.SetSelectedTile(this);
        _isSelected = true;
    }

    public void DeselectTile()
    {
        _isSelected = false;
    }
}
