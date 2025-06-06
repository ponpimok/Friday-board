using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardObjPirateScript : MonoBehaviour
{
    public PirateDataScript thisCardInfo;

    public TextMeshProUGUI numCard;
    public TextMeshProUGUI numPower;
    public TextMeshProUGUI textEffect;

    private bool seeCrad;

    private void Start()
    {
        seeCrad = false;

        if (thisCardInfo.take_card != 0)
        {
            numCard.text = thisCardInfo.take_card.ToString();
        }
        else
        {
            numCard.text = " * ";
        }

        if (!thisCardInfo.have_effect)
        {
            textEffect.text = ". . .";
            numPower.text = thisCardInfo.power_enemy.ToString();
        }
        else
        {
            if (thisCardInfo.power_enemy != 0)
            {
                numPower.text = thisCardInfo.power_enemy.ToString();
            }
            else
            {
                numPower.text = " * ";
            }
        }
    }


    private void Update()
    {
        
    }
}
