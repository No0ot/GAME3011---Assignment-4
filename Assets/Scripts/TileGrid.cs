using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Vector2Int gridSize =  new Vector2Int(6,6);
    public float tileSize;
    public Tile tilePrefab;

    List<Tile> tileList = new List<Tile>();

    PipeManager pipeManager;
    // Start is called before the first frame update

    private void Awake()
    {
        tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        pipeManager = GetComponent<PipeManager>();
    }

    void Start()
    {
        RegenerateGrid();
    }

    private void RegenerateGrid()
    {
        for(int i = 0, k = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                GenerateTile(i, j, k++);
            }
        }
    }

    private void GenerateTile(int x, int y, int index)
    {
        Tile temp = Instantiate(tilePrefab, this.transform);
        temp.transform.position = new Vector3(x * tileSize, y * tileSize);

        if (index > -1)
            tileList.Insert(index, temp);
        else
            tileList.Add(temp);

        if (x > 0)
        {
           temp.SetNeighbour(TileDirections.LEFT, tileList[index - gridSize.x]);
        }
        if (y > 0)
        {
            temp.SetNeighbour(TileDirections.DOWN, tileList[index - 1]);
        }

        Pipe pipeTemp = pipeManager.GeneratePipe();

        pipeTemp.transform.SetParent(temp.transform);
        temp.occupyingPipe = pipeTemp;
        pipeTemp.transform.localPosition = Vector3.zero;
    }
}
