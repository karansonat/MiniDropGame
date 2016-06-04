using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndDayScreen : MonoBehaviour
{

    public Text population;
    public Text currentWater;
    public Text currentWaterIncome;
    public Text popConsume;
    public Text waterIncome;

    public void UpdateEndDayValues()
    {
        var level = LevelConfig.Instance.GetActiveLevel();
        var messageText = transform.FindChild("Message").GetComponent<Text>();
        messageText.text = "Day " + level.DayCount + " Report";
        currentWater.text = level.CurrentWater.ToString();
        population.text = level.Population.ToString();
        currentWaterIncome.text = "+" + (level.DailyWaterIncome - level.DailyWaterOutcome) + " / Day";
        popConsume.text = "-" + level.DailyWaterOutcome + " / Day";
        waterIncome.text = "+" + (level.DailyWaterIncome - level.DailyWaterOutcome) + " / Day";
    }

    void OnEnable()
    {
        UpdateEndDayValues();
    }

    public void ContinueGame()
    {
        gameObject.SetActive(false);
    }

    public void ShowEndDayReport()
    {
        gameObject.SetActive(true);
    }
}
