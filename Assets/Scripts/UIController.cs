using UnityEngine;
using System.Collections;

public class UIController : MonoSingleton<UIController>
{
    public AddDialogue AddDialogueObj;
    public HUDController HUDControllerObj;
    public DebugPanel DebugPanelObj;
    public TileDescription TileDescriptionObj;

    void Awake()
    {
        AddDialogueObj = transform.FindChild("AddDialogue").GetComponent<AddDialogue>();
        HUDControllerObj = transform.FindChild("HUD-TOP").GetComponent<HUDController>();
        TileDescriptionObj = transform.FindChild("TileDescriptionPanel").GetComponent<TileDescription>();
    }
}
