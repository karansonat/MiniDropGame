using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private Text _currentPupulation;
    private Text _currentWater;
    private Text _waterIncome;
    private Text _buildingOutcome;
    private Text _treeIncome;

    // Use this for initialization
    void Start () {
        _currentPupulation = transform.FindChild("Population/Current").GetComponent<Text>();
        _currentWater = transform.FindChild("InfoPanel/Water/Current").GetComponent<Text>();
        _waterIncome = transform.FindChild("InfoPanel/Water/Income").GetComponent<Text>();
        _buildingOutcome = transform.FindChild("InfoPanel/Buildings/Outcome").GetComponent<Text>();
        _treeIncome = transform.FindChild("InfoPanel/Trees/Income").GetComponent<Text>();

        UpdateHUD();
    }

    public void UpdateHUD()
    {
        var level = LevelConfig.Instance.GetActiveLevel();
        //Population
        var population = level.tiles.Sum(tile => tile.CalculateTilePopulation());
        Debug.Log(population);
        _currentPupulation.text = population.ToString();
        //Current water
        _currentWater.text = level.CurrentWater.ToString();
        //Calculated water income
        _waterIncome.text = "+" + (level.DailyWaterIncome - level.DailyWaterOutcome) + " / Day";
        //Population consumption
        _buildingOutcome.text = "-" + level.DailyWaterOutcome + " / Day";
        //Water income from trees
        _treeIncome.text = "+" + level.DailyWaterIncome + " / Day";
    }
}
