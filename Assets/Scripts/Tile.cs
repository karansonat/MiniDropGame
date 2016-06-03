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
    public int row;
    public int col;
    public List<TileLayer>  layers = new List<TileLayer>();
    public int substructureLevel;
    public int buildingTreeLevel;
    public float bonusRate;
    public int tilePopulation;
    
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
            layerObj.transform.localPosition = new Vector3(0, 0.1001f, 0);
            /*
            switch (tileLayer.LayerTag)
            {
                case "Tree":
                    layerObj.transform.localPosition = new Vector3(0, 0.1001f, 0);
                    break;
                case "Buildings":
                    layerObj.transform.localPosition = new Vector3(0, 0.1001f, 0);
                    break;
            }*/
        }
    }

    public void AddLayer(string name)
    {
        var level = LevelConfig.Instance.GetActiveLevel();

        var parsedString = name.Split('_');
        var tileLayer = new TileLayer
        {
            Layer = name,
            LayerTag = parsedString[0],
            LayerLevel = int.Parse(parsedString[1])
        };

        if(level.CurrentWater - LevelConfig.Instance._outcomes[tileLayer.Layer] < 0) return;

        //Player can not add buildings layer if tile has tree layer and not have Buildings layer.
        if (tileLayer.LayerTag == "Buildings" && layers.Count == 1 && layers.Any(layer => layer.LayerTag == "Tree")) return;

        for (var i = layers.Count - 1; i >= 0; i--)
        {
            if (layers[i].LayerTag == tileLayer.LayerTag)
            {
                if (layers[i].LayerLevel >= tileLayer.LayerLevel) return;
                LevelConfig.Instance.RemoveLayerDataFromLevel(layers[i].LayerTag, layers[i].LayerLevel);
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
                buildingTreeLevel = layer.LayerLevel;
                CalculateBonusRate();
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
        GameController.Instance.UnHighlightTile();
        GameController.Instance._selectedTile = null;
    }

    public void UpgradeTileSubstructureLevel()
    {
        Debug.Log(name + "Upgraded.");
        substructureLevel++;
        CalculateBonusRate();
        UIController.Instance.HUDControllerObj.UpdateHUD();
    }

    public void CalculateBonusRate()
    {
        bonusRate = ((substructureLevel * 10) + (buildingTreeLevel * 10)) / 100.0f;
        Debug.Log(bonusRate);
    }

    public int CalculateTilePopulation()
    {
        tilePopulation = 0;
        if (layers.All(layer => layer.LayerTag != "Buildings"))
        {
            tilePopulation = 0;
        }
        else
        {
            foreach (var tileLayer in layers)
            {
                if (tileLayer.LayerTag == "Buildings")
                {
                    var normalPopulation = LevelConfig.Instance._incomes[tileLayer.Layer];
                    tilePopulation += normalPopulation + (int)(normalPopulation*bonusRate);
                }
            }
        }
        return tilePopulation;
    }
}
