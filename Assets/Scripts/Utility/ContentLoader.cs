using UnityEngine;
using System.Collections;

public class ContentLoader : MonoSingleton<ContentLoader> {

    public GameObject GetGameObjectByPrefabName(string name)
    {
        GameObject go;
        var prefab = Resources.Load("Prefabs/" + name);
        go = Instantiate(prefab) as GameObject;
        return go;
    }
}
