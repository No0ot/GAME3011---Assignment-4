using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    public static PipeManager instance;

    public Pipe[] pipePrefabs;
    public GameObject swapPipePosition;

    public Pipe bankedPipe = null;

    private void Awake()
    {
        instance = this;
        bankedPipe = GeneratePipe();
        bankedPipe.transform.SetParent(swapPipePosition.transform);
        bankedPipe.transform.localPosition = Vector3.zero;
        bankedPipe.hidden = false;
        bankedPipe.hiddenSprite.SetActive(false);
    }

    public Pipe GeneratePipe()
    {
        Pipe temp = Instantiate(pipePrefabs[Random.Range(0, pipePrefabs.Length)]);

        return temp;
    }

    public void SwapPipe(Pipe temp)
    {
        
        if(temp != bankedPipe)
        {
            Transform pipeParent = temp.transform.parent;
            bankedPipe.transform.SetParent(pipeParent);
            bankedPipe.transform.localPosition = Vector3.zero;

            temp.transform.SetParent(swapPipePosition.transform);
            temp.transform.localPosition = Vector3.zero;
            bankedPipe = temp;
        }
    }
}
