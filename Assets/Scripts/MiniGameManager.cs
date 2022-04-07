using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}
public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;
    public Difficulty difficulty;

    [Range(1,10)]
    public int hackingSkill = 1;

    public float fillRate = 20f;

    public bool gameComplete;

    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fillRate = 200f;
        }
    }
}
