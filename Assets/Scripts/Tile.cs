using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileDirections
{
    UP,
    LEFT,
    DOWN,
    RIGHT
}

public class Tile : MonoBehaviour
{
    public Tile[] neighbours;
    public Pipe occupyingPipe = null;
    public Vector2Int coordinates;

    public Tile[] GetNeighboursArray()
    {
        return neighbours;
    }

    public Tile GetNeighbour(TileDirections neighbour_direction)
    {
        return neighbours[(int)neighbour_direction];
    }

    public void SetNeighbour(TileDirections direction, Tile tile)
    {
        neighbours[(int)direction] = tile;
        tile.neighbours[(int)GetOppositeNeighbour(direction)] = this;
    }

    public TileDirections GetOppositeNeighbour(TileDirections direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }

    public void OnMouseDown()
    {
        if (occupyingPipe.hidden)
        {
            occupyingPipe.Reveal();
        }
        else
        {
            PipeManager.instance.SwapPipe(occupyingPipe);
        }
    }
}
