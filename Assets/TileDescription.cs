using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileDescription : MonoBehaviour
{

    private Text _pipeLevel;
    private Text _bonusRate;

    void Awake()
    {
        _pipeLevel = transform.FindChild("PipeLevel").GetComponent<Text>();
        _bonusRate = transform.FindChild("BonusRate").GetComponent<Text>();
        HidePanel();
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        var tile = GameController.Instance._selectedTile;
        _pipeLevel.text = tile.substructureLevel.ToString();
        _bonusRate.text = tile.bonusRate.ToString();
    }
}
