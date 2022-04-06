using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public Vector2Int gridSize =  new Vector2Int(6,6);
    public static float tileSize;

    public GameObject pipePrefab;

    public Pipe[,] tileList;

    PipeManager pipeManager;

    public Pipe startPipe = null;
    public Pipe endPipe = null;
    private void Awake()
    {
        tileSize = pipePrefab.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        pipeManager = GetComponent<PipeManager>();
    }

    void Start()
    {
        tileList = new Pipe[gridSize.x, gridSize.y];
        RegenerateGrid();
        CreateStartAndEndPipes();
        startPipe.fill = true;
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
        Pipe temp = pipeManager.GeneratePipe();
        //Tile temp = Instantiate(tilePrefab, this.transform);
        temp.transform.SetParent(this.transform);
        temp.transform.position = new Vector3(x * tileSize, y * tileSize);
        temp.coordinates = new Vector2Int(x, y);

        tileList[x, y] = temp;

        if (x > 0)
        {
           temp.SetNeighbour(TileDirections.LEFT, tileList[x - 1, y]);
        }
        if (y > 0)
        {
            temp.SetNeighbour(TileDirections.DOWN, tileList[x, y - 1]);
        }
    }

    public void CreateStartAndEndPipes()
    {
        int startSide = Random.Range(0, 2);

        if(startSide > 0)
        {
            int x = Random.Range(0, gridSize.x);

            Pipe pipeTemp = pipeManager.GeneratePipe(1);
            pipeTemp.transform.position = new Vector3(x * tileSize, -1 * tileSize);
            startPipe = pipeTemp;
            startPipe.coordinates = new Vector2Int(x, -1);

            Pipe nextPipe = tileList[x, 0];
            nextPipe.Reveal();
        }
        else
        {
            int y = Random.Range(0, gridSize.y);
            Pipe pipeTemp = pipeManager.GeneratePipe(0);
            pipeTemp.transform.position = new Vector3(-1 * tileSize, y * tileSize);
            startPipe = pipeTemp;
            startPipe.coordinates = new Vector2Int(-1, y);

            Pipe nextPipe = tileList[0, y];
            nextPipe.Reveal();
        }

        startPipe.Reveal();
        startPipe.locked = true;

        int endSide = Random.Range(0, 2);

        if(endSide > 0)
        {
            int x = Random.Range(0, gridSize.x);

            Pipe pipeTemp = pipeManager.GeneratePipe(1);
            pipeTemp.transform.position = new Vector3(x * tileSize, (gridSize.y) * tileSize);
            endPipe = pipeTemp;
        }
        else
        {
            int y = Random.Range(0, gridSize.y);
            Pipe pipeTemp = pipeManager.GeneratePipe(0);
            pipeTemp.transform.position = new Vector3((gridSize.x) * tileSize, y * tileSize);
            endPipe = pipeTemp;
        }

        endPipe.Reveal();
        endPipe.locked = true;
    }

    public void SetNewNeighbours(Pipe initial)
    {
        Vector2Int temp = initial.coordinates;

        if(temp.y + 1 < gridSize.y)
        {
            initial.SetNeighbour(TileDirections.UP, tileList[temp.x, temp.y + 1]);
        }
        if(temp.y - 1 > 0)
        {
            initial.SetNeighbour(TileDirections.DOWN, tileList[temp.x, temp.y - 1]);
        }
        if (temp.x + 1 < gridSize.x)
        {
            initial.SetNeighbour(TileDirections.RIGHT, tileList[temp.x + 1, temp.y]);
        }
        if (temp.x - 1 > 0)
        {
            initial.SetNeighbour(TileDirections.LEFT, tileList[temp.x - 1, temp.y]);
        }
    }

    public Pipe GetNextTileInDirection(Vector2Int initial, TileDirections direction)
    {
        switch(direction)
        {
            case TileDirections.UP:
                if (initial.y + 1 < gridSize.y)
                {
                    return tileList[initial.x, initial.y + 1];
                }
                break;
            case TileDirections.LEFT:
                if (initial.x - 1 > 0)
                {
                    return tileList[initial.x - 1, initial.y];
                }
                break;
            case TileDirections.DOWN:
                if (initial.y - 1 > 0)
                {
                    return tileList[initial.x, initial.y - 1];
                }
                break;
            case TileDirections.RIGHT:
                if (initial.x + 1 < gridSize.x)
                {
                    return tileList[initial.x + 1, initial.y];
                }
                break;
        }
        return null;
    }

    //public Pipe GetTileFromCoordinates(Vector2Int coordinates)
    //{
    //    int iX = coordinates.x;
    //    int iY = coordinates.y;
    //    int index = iY + iX * gridSize.y;
    //    return tileList[index];
    //}
    //
    //public Pipe GetTileFromCoordinates(int x, int y)
    //{
    //    int index = y + x * gridSize.y;
    //    return tileList[index];
    //}
}
