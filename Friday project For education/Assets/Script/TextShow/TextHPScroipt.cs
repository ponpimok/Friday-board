using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class TextHPScroipt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hp_text;
    [SerializeField] private PlayCardScript playCardScript;
    [SerializeField] private ScoreScript scoreScript;
    public int hp;
    public int max;
    void Update()
    {
        if (hp > max)
        {
            hp = max;
        }
        hp_text.text = hp.ToString() + "/" + max.ToString();

        if (hp < 0)
        {
            playCardScript.loseUI.SetActive(true);
            scoreScript.CalculateScore(playCardScript.pirateWin);
        }
    }
}
