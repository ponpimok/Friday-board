using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObjScript : MonoBehaviour
{
    public CardPlayScript thisCardInfo;

    public Image image_card;
    public TextMeshProUGUI text_name_use;
    public TextMeshProUGUI text_power_use;
    public TextMeshProUGUI text_effect;
    public TextMeshProUGUI text_destroy;

    public TextMeshProUGUI text_name_fight;
    public TextMeshProUGUI text_num_crad;
    public TextMeshProUGUI text_power_fight_1;
    public TextMeshProUGUI text_power_fight_2;
    public TextMeshProUGUI text_power_fight_3;

    void Start()
    {
        text_name_use.text = thisCardInfo.name_use_card;
        text_power_use.text = thisCardInfo.power_card.ToString();
        text_effect.text = thisCardInfo.card_effect_name;
        text_destroy.text = thisCardInfo.use_hp_to_destroy.ToString();
        if (!thisCardInfo.starter_card)
        {
            text_name_fight.text = thisCardInfo.name_fight_card;
            text_num_crad.text = thisCardInfo.take_card.ToString();
            text_power_fight_1.text = thisCardInfo.power_enemy[0].ToString();
            text_power_fight_2.text = thisCardInfo.power_enemy[1].ToString();
            text_power_fight_3.text = thisCardInfo.power_enemy[2].ToString();
        }
        else
        {
            text_name_fight.gameObject.SetActive(false);
            text_num_crad.gameObject.SetActive(false);
            text_power_fight_1.gameObject.SetActive(false);
            text_power_fight_2.gameObject.SetActive(false);
            text_power_fight_3.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
