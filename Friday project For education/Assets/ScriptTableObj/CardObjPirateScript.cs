using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardObjPirateScript : MonoBehaviour
{
    public PirateDataScript thisCardInfo;
    [SerializeField] DataCardScript data;

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

        if (thisCardInfo.num_effect == 1)
        {
            thisCardInfo.power_enemy = 0;
        }

        if (!thisCardInfo.have_effect)
        {
            textEffect.text = ". . .";
            numPower.text = thisCardInfo.power_enemy.ToString();
        }
        else
        {
            textEffect.text = thisCardInfo.effect_text;
            if (thisCardInfo.power_enemy != 0)
            {
                numPower.text = thisCardInfo.power_enemy.ToString();
            }
            else if (thisCardInfo.power_enemy == 0 && thisCardInfo.num_effect == 1)
            {
                int i = ((3 - data.age_card_ws.Count) + (10 - data.age_card_bs.Count)) * 2;//แก้ถ้าเพิ่มความยาก
                thisCardInfo.power_enemy = i;
                numPower.text = i.ToString();
            }
            else
            {
                numPower.text = " * ";
            }
        }
    }
}
