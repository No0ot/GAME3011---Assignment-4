using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TileDirections
{
    UP,
    LEFT,
    DOWN,
    RIGHT
}

public class Pipe : MonoBehaviour
{
    [Serializable]
    public struct TileConnections
    {
        public TileDirections direction;
        public bool canConnect;
    }

    public Pipe[] neighbours;
    public Vector2Int coordinates;

    public TileConnections[] connections;

    public float fillPercent = 0.0f;
    public float fillRate = 20f;

    public bool hidden = true;
    public GameObject hiddenSprite;
    public SpriteRenderer fillSprite;

    public bool fill = false;
    public bool locked = false;

    public void Reveal()
    {
        hidden = false;
        hiddenSprite.SetActive(false);
    }

    private void Update()
    {
        if(fill)
        {
            if (fillPercent < 100f)
            {
                fillPercent += fillRate * Time.deltaTime;
                fillSprite.color = new Color(1, 1, 1, (fillPercent / 100));
            }
            else
            {
                GetNextFillTiles();
            }
        }
    }

    public void GetNextFillTiles()
    {
        List<Pipe> nextPipes = new List<Pipe>();

        foreach(TileConnections connection in connections)
        {
            if(connection.canConnect)
            {
                Pipe temp = PipeManager.instance.grid.GetNextTileInDirection(coordinates, connection.direction);

                if (temp)
                {
                    if (!temp.hidden)
                    {
                        // This is a crazy line of code, basically checks the next tile to see if the connection in the opposite direction is possible
                        if (temp.connections[(int)GetOppositeNeighbour(connection.direction)].canConnect)
                        {
                            nextPipes.Add(temp);
                        }
                    }
                }
            }
            
        }

        foreach(Pipe temp in nextPipes)
        {
            temp.fill = true;
        }
    }

    public Pipe[] GetNeighboursArray()
    {
        return neighbours;
    }

    public Pipe GetNeighbour(TileDirections neighbour_direction)
    {
        return neighbours[(int)neighbour_direction];
    }

    public void SetNeighbour(TileDirections direction, Pipe pipe)
    {
        neighbours[(int)direction] = pipe;
        pipe.neighbours[(int)GetOppositeNeighbour(direction)] = this;
    }

    public TileDirections GetOppositeNeighbour(TileDirections direction)
    {
        return (int)direction < 2 ? (direction + 2) : (direction - 2);
    }

    public void ClearNeighbours()
    {
        for(int i = 0; i < neighbours.Length; i++)
        {
            neighbours[i] = null;
        }
    }

    public void OnMouseDown()
    {
        if (hidden)
        {
            Reveal();
        }
        else
        {
            PipeManager.instance.SwapPipe(this);
        }
    }
}
