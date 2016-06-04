using UnityEngine;
using System.Collections;

public class UIController : MonoSingleton<UIController>
{
    public AddDialogue AddDialogueObj;
    public HUDController HUDControllerObj;
    public DebugPanel DebugPanelObj;
    public TileDescription TileDescriptionObj;
    public EndDayScreen EndDayScreemObj;

    void Awake()
    {
        AddDialogueObj = transform.FindChild("AddDialogue").GetComponent<AddDialogue>();
        HUDControllerObj = transform.FindChild("HUD-TOP").GetComponent<HUDController>();
        TileDescriptionObj = transform.FindChild("TileDescriptionPanel").GetComponent<TileDescription>();
        EndDayScreemObj = transform.FindChild("EndDayScreen").GetComponent<EndDayScreen>();
    }
}
