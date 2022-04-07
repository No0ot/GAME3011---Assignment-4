using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text difficultyText;
    public TMP_Text skillText;

    public GameObject losePanel;
    public GameObject winPanel;

    private void Awake()
    {
        instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficultyText();
        UpdateSkillText();
    }

    void UpdateDifficultyText()
    {
        switch(MiniGameManager.instance.difficulty)
        {
            case Difficulty.EASY:
                difficultyText.text = "Difficulty: EASY";
                break;
            case Difficulty.MEDIUM:
                difficultyText.text = "Difficulty: MEDIUM";
                break;
            case Difficulty.HARD:
                difficultyText.text = "Difficulty: HARD";
                break;
        }
    }

    void UpdateSkillText()
    {
        skillText.text = "Hacking Skill: " + MiniGameManager.instance.hackingSkill;
    }
}
