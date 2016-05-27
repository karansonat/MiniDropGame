using UnityEngine;
using System.Collections;

//This class responsible for initial works on Level Creation.
public class LevelConfig : MonoSingleton<LevelConfig>
{
    [Header("Level Config")]
    public int row;
    public int col;

    [Header("Prefabs")]
    public GameObject Tile;

	void Update () {
	
	}

    public void InitLevel()
    {
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        var levelContainer = GameObject.Find("Level");
         //Find middle point of the board.
        var tileWidth = Tile.GetComponent<Renderer>().bounds.size.x;
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
                //Add it to level tiles.
                GameController.Instance.GetActiveLevel().tiles.Add(tileComp);
                tile.transform.SetParent(levelContainer.transform);
                //Set tile's position.
                tile.transform.localPosition = new Vector3(topLeftCornerPos.x + (c * tileWidth), 0, topLeftCornerPos.z + (r * -tileWidth));
            }
        }
        levelContainer.transform.position = Vector3.zero;
    }
}
