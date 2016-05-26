using UnityEngine;
using System.Collections;

public class LevelConfig : MonoBehaviour
{
    [Header("Level Config")]
    public int row;
    public int col;

    [Header("Prefabs")]
    public GameObject Tile;

	// Use this for initialization
	void Start () {
	    InitLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void InitLevel()
    {
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        var levelContainer = GameObject.Find("Level");
        //Find middle point of the board.
        var tileWidth = Tile.GetComponent<Renderer>().bounds.size.x;
        Debug.Log(tileWidth);
        var topLeftCornerPos = new Vector3(-(tileWidth * col / 2), 0, (tileWidth * row / 2));

        for (var r = 0; r < row; r++)
        {
            for (var c = 0; c < col; c++)
            {
                var tile = Instantiate(Tile);
                tile.name = "Tile {" + r + " , " + c + "}";
                var tileComp = tile.GetComponent<Tile>();
                tileComp._row = r;
                tileComp._col = c;
                tile.transform.SetParent(levelContainer.transform);
                //Set tile's position.
                tile.transform.localPosition = new Vector3(topLeftCornerPos.x + (c * tileWidth), 0, topLeftCornerPos.z + (r * -tileWidth));
            }
        }
        levelContainer.transform.position = Vector3.zero;
    }
}
