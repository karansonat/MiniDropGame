using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public int _row;
    public int _col;
    
    private bool _isHighlighted;

    public void Init()
    {
        _isHighlighted = false;

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnMouseEnter()
    {
        Debug.Log("Tile : " + gameObject.name);
    }

    void OnMouseExit()
    {
        
    }
}
