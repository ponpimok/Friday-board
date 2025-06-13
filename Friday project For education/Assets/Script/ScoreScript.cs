using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private DataCardScript dataCardScript;
    [SerializeField] private TextHPScroipt hp;
    [SerializeField] private TextMeshProUGUI textScoreWin;
    [SerializeField] private TextMeshProUGUI textScoreLose;
    private int score;

    public void CalculateScore(int pirateWin)
    {
        score = 0;

        if (dataCardScript.my_crad.Count > 0)
        {
            foreach (var item in dataCardScript.my_crad)
            {
                if (!item.ageCrad)
                {
                    score += item.power_card;
                }
                else
                {
                    score -= 5;
                }
            }
        }

        if (dataCardScript.my_crad_used.Count > 0)
        {
            foreach (var item in dataCardScript.my_crad_used)
            {
                if (!item.ageCrad)
                {
                    score += item.power_card;
                }
                else
                {
                    score -= 5;
                }
            }
        }

        score += (pirateWin * 15);

        score += (hp.hp * 5);

        score -= (dataCardScript.not_fight_now.Count * 3);
    }
    private void Update()
    {
        textScoreWin.text = "Score : " + score.ToString();
        textScoreLose.text = "Score : " + score.ToString();
    }
}
