using UnityEngine;
using System.Collections;

//Main game control class.
public class GameController : MonoSingleton<GameController>
{

    private Level _level;

	// Use this for initialization
	void Start () {
        _level = new Level();
	    LevelConfig.Instance.InitLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Level GetActiveLevel()
    {
        return _level;
    }
}
