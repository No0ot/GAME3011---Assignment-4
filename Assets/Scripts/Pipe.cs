using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pipe : MonoBehaviour
{
    [Serializable]
    public struct TileConnections
    {
        public TileDirections direction;
        public bool canConnect;
    }

    public TileConnections[] connections;

    public float fillPercent = 0.0f;
    public float fillRate = 20f;

    public bool hidden = true;
    public GameObject hiddenSprite;
    public SpriteRenderer fillSprite;

    public bool fill = false;

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
                fill = false;
            }
        }
    }

    public void GetNextFillTiles(TileDirections initialDirection)
    {
        List<Pipe> nextPipes = new List<Pipe>();

        foreach(TileConnections connection in connections)
        {
            if(connection.direction != initialDirection)
            {
                if(connection.canConnect)
                {
                    nextPipes.Add();
                }
            }
        }
    }
}
