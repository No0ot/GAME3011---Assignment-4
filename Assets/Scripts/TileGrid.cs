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

    public Pipe startPipe = null;
    public Pipe endPipe = null;
    private void Awake()
    {
        tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
        pipeManager = GetComponent<PipeManager>();
    }

    void Start()
    {
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
        Tile temp = Instantiate(tilePrefab, this.transform);
        temp.transform.position = new Vector3(x * tileSize, y * tileSize);
        temp.coordinates = new Vector2Int(x, y);

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

    public void CreateStartAndEndPipes()
    {
        int startSide = Random.Range(0, 2);

        if(startSide > 0)
        {
            int x = Random.Range(0, gridSize.x);

            Pipe pipeTemp = pipeManager.GeneratePipe(1);
            pipeTemp.transform.position = new Vector3(x * tileSize, -1 * tileSize);
            startPipe = pipeTemp;

            Pipe nextPipe = GetTileFromCoordinates(x, 0).occupyingPipe;
            nextPipe.Reveal();
        }
        else
        {
            int y = Random.Range(0, gridSize.y);
            Pipe pipeTemp = pipeManager.GeneratePipe(0);
            pipeTemp.transform.position = new Vector3(-1 * tileSize, y * tileSize);
            startPipe = pipeTemp;

            Pipe nextPipe = GetTileFromCoordinates(0, y).occupyingPipe;
            nextPipe.Reveal();
        }

        startPipe.Reveal();

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
    }

    public Tile GetTileFromCoordinates(Vector2Int coordinates)
    {
        int iX = coordinates.x;
        int iY = coordinates.y;
        int index = iY + iX * gridSize.y;
        return tileList[index];
    }

    public Tile GetTileFromCoordinates(int x, int y)
    {
        int index = y + x * gridSize.y;
        return tileList[index];
    }
}
