using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFightScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numFightCard;
    [SerializeField] private TextMeshProUGUI numFightedCard;
    [SerializeField] private DataCardScript getInfo;   
    private void Update()
    {
        numFightCard.text = getInfo.fight_card_s.Count.ToString();
        numFightedCard.text = getInfo.not_fight_now.Count.ToString();
    }
}
