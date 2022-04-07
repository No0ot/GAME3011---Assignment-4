using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public static PipeManager instance;

    public Pipe[] pipePrefabs;
    public GameObject swapPipePosition;

    public Pipe bankedPipe = null;
    public TileGrid grid;


    private void Awake()
    {
        instance = this;
        grid = GetComponent<TileGrid>();
        bankedPipe = GeneratePipe();
        bankedPipe.transform.SetParent(swapPipePosition.transform);
        bankedPipe.transform.localPosition = Vector3.zero;
        bankedPipe.Reveal();
    }

    public Pipe GeneratePipe()
    {
        Pipe temp = Instantiate(pipePrefabs[Random.Range(0, 6 + (int)MiniGameManager.instance.difficulty)]);
        return temp;
    }

    public Pipe GeneratePipe(int i)
    {
        Pipe temp = Instantiate(pipePrefabs[i]);
        return temp;
    }

    public void SwapPipe(Pipe temp)
    {
        if (!MiniGameManager.instance.gameComplete)
        {
            if (temp != bankedPipe && !temp.fill && !temp.locked && !temp.fillComplete)
            {
                Transform pipeParent = temp.transform.parent;
                Vector2Int coord = temp.coordinates;
                bankedPipe.transform.SetParent(pipeParent);
                bankedPipe.transform.position = new Vector3((coord.x * TileGrid.tileSize) + grid.gridOffset.x, (coord.y * TileGrid.tileSize) + grid.gridOffset.y);
                bankedPipe.coordinates = coord;
                grid.tileList[coord.x, coord.y] = bankedPipe;
                grid.SetNewNeighbours(bankedPipe);

                temp.transform.SetParent(swapPipePosition.transform);
                temp.transform.localPosition = Vector3.zero;
                temp.coordinates = new Vector2Int(-1, -1);
                bankedPipe = temp;
                bankedPipe.ClearNeighbours();
            }
        }
    }

   
}
