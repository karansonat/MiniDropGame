using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UIHoverListener : MonoSingleton<UIHoverListener>
{
    public bool isUIOverride { get; private set; }

    void Update()
    {
        // It will turn true if hovering any UI Elements
        isUIOverride = EventSystem.current.IsPointerOverGameObject();
    }
}