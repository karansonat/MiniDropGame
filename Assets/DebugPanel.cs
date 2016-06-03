using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugPanel : MonoBehaviour
{
    [HideInInspector]
    public Button UpgradeTileButton;
    [HideInInspector]
    public Button NextDayButton;

	// Use this for initialization
	void Start ()
	{
	    UpgradeTileButton = transform.FindChild("UpgradeTileButton").GetComponent<Button>();
        NextDayButton = transform.FindChild("NextDayButton").GetComponent<Button>();

        UpgradeTileButton.onClick.AddListener(() =>
        {
            GameController.Instance._selectedTile.UpgradeTileSubstructureLevel();
        });
    }
	
	// Update is called once per frame
	void Update ()
	{
	    UpgradeTileButton.interactable = GameController.Instance._selectedTile != null;
	}
}
