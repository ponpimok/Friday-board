using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFightScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numCard;
    [SerializeField] private GameScript getInfo;
    

    private void Start()
    {
        numCard.text = getInfo.fight_card_s.Count.ToString();
    }

    
    void Update()
    {
        
    }
}
