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
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        gameComplete = false;
        
    }

    private void OnEnable()
    {
        fillRate = 50f - (hackingSkill * 3f);
        //fillRate = 20f;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fillRate = 500f;
        }
    }

    public void SetDifficulty(int diff)
    {
        difficulty = (Difficulty)diff;
    }

}
