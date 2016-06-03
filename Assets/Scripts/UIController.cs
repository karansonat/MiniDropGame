using UnityEngine;
using System.Collections;

public class UIController : MonoSingleton<UIController>
{
    public AddDialogue AddDialogueObj;
    public HUDController HUDControllerObj;

    void Awake()
    {
        AddDialogueObj = transform.FindChild("AddDialogue").GetComponent<AddDialogue>();
        HUDControllerObj = transform.FindChild("HUD-TOP").GetComponent<HUDController>();
    }
}
