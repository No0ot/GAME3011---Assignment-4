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
    public bool hidden = true;
    public GameObject hiddenSprite;

    public void OnMouseDown()
    {
        if (hidden)
        {
            hidden = false;
            hiddenSprite.SetActive(false);
        }
        else
        {
            PipeManager.instance.SwapPipe(this);
        }
    }
}
