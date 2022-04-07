using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileGrid : MonoBehaviour
{
    public Vector2Int gridSize =  new Vector2Int(6,6);
    public static float tileSize;

    public Vector2 gridOffset;

    public GameObject pipePrefab;

    [Serializable]
    public struct TileArray
    {
        TileArray(int arraySize)
        {
            rowTiles = new Pipe[arraySize];
        }
        public Pipe[] rowTiles;
    }
    public TileArray[] debugArray;


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
        tileList = new Pipe[gridSize.x + 1, gridSize.y + 1];
        RegenerateGrid();
        CreateStartAndEndPipes();
        startPipe.fill = true;

        debugArray = new TileArray[gridSize.y + 1];

        for (int i = 0; i < gridSize.x + 1; i++)
        {
            debugArray[i].rowTiles = new Pipe[gridSize.x + 1];
            for (int j = 0; j < gridSize.y + 1; j++)
            {
                debugArray[i].rowTiles[j] = tileList[i, j];
            }
        }
    }

    private void RegenerateGrid()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                GenerateTile(i, j);
            }
        }
    }

    private void GenerateTile(int x, int y)
    {
        Pipe temp = pipeManager.GeneratePipe();
        //Tile temp = Instantiate(tilePrefab, this.transform);
        temp.transform.SetParent(this.transform);
        temp.transform.position = new Vector3((x * tileSize) + gridOffset.x, (y * tileSize) + gridOffset.y);
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
        int startSide = UnityEngine.Random.Range(0, 2);

        if(startSide > 0)
        {
            int x = UnityEngine.Random.Range(0, gridSize.x);

            Pipe pipeTemp = pipeManager.GeneratePipe(1);
            pipeTemp.transform.position = new Vector3((x * tileSize) + gridOffset.x, (-1 * tileSize) + gridOffset.y);
            startPipe = pipeTemp;
            startPipe.coordinates = new Vector2Int(x, -1);

            Pipe nextPipe = tileList[x, 0];
            nextPipe.Reveal();
        }
        else
        {
            int y = UnityEngine.Random.Range(0, gridSize.y);
            Pipe pipeTemp = pipeManager.GeneratePipe(0);
            pipeTemp.transform.position = new Vector3((-1 * tileSize) + gridOffset.x, (y * tileSize) + gridOffset.y);
            startPipe = pipeTemp;
            startPipe.coordinates = new Vector2Int(-1, y);

            Pipe nextPipe = tileList[0, y];
            nextPipe.Reveal();
        }
        startPipe.transform.SetParent(this.transform);
        startPipe.Reveal();
        startPipe.locked = true;

        int endSide = UnityEngine.Random.Range(0, 2);

        if(endSide > 0)
        {
            int x = UnityEngine.Random.Range(0, gridSize.x);

            Pipe pipeTemp = pipeManager.GeneratePipe(1);
            pipeTemp.transform.position = new Vector3((x * tileSize) + gridOffset.x, ((gridSize.y) * tileSize) + gridOffset.y);
            endPipe = pipeTemp;
            endPipe.coordinates = new Vector2Int(x, gridSize.y);
        }
        else
        {
            int y = UnityEngine.Random.Range(0, gridSize.y);
            Pipe pipeTemp = pipeManager.GeneratePipe(0);
            pipeTemp.transform.position = new Vector3(((gridSize.x) * tileSize) + gridOffset.x, (y * tileSize) + gridOffset.y);
            endPipe = pipeTemp;
            endPipe.coordinates = new Vector2Int(gridSize.x, y);
        }

        tileList[endPipe.coordinates.x, endPipe.coordinates.y] = endPipe;
        endPipe.transform.SetParent(this.transform);
        endPipe.Reveal();
        endPipe.locked = true;
        endPipe.goalPipe = true;
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
                if (initial.y + 1 < gridSize.y + 1)
                {
                    Debug.Log(tileList[initial.x, initial.y + 1].coordinates);
                    return tileList[initial.x, initial.y + 1];
                }
                break;
            case TileDirections.LEFT:
                if (initial.x - 1 >= 0)
                {
                    return tileList[initial.x - 1, initial.y];
                }
                break;
            case TileDirections.DOWN:
                if (initial.y - 1 >= 0)
                {
                    return tileList[initial.x, initial.y - 1];
                }
                break;
            case TileDirections.RIGHT:
                if (initial.x + 1 < gridSize.x + 1)
                {
                    return tileList[initial.x + 1, initial.y];
                }
                break;
        }
        return null;
    }
}
