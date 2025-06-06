using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;

public class CardObjScript : MonoBehaviour
{
    public CardPlayScript thisCardInfo;
    public EffectCardScript effectCard;
    public bool useOnly;

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

    private bool seeUse;
    private bool seeCrad;
    public float zoomCrad;
    public Button useEffectButton;
    public bool usedEffect;
    public Button destroyButton;
    public bool showDestroyButtom;

    public void RotateCrad()
    {
        if (!useOnly)
        {
            if (seeUse)
            {
                GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, 0);
                seeUse = false;
            }
            else
            {
                GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, -180);
                seeUse = true;
            }
        }
    }
    public void CheckCrad()
    {
        if (useOnly)
        {
            if (!seeCrad)
            {
                GetComponent<RectTransform>().transform.localScale = new Vector3(zoomCrad, zoomCrad, zoomCrad);
                seeCrad = true;
                if (!usedEffect)
                {
                    useEffectButton.gameObject.SetActive(true);
                }
            }
            else
            {
                GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                seeCrad = false;
                if (!usedEffect)
                {
                    useEffectButton.gameObject.SetActive(false);
                }
            }
        }
    }
    public void UseEffect()
    {
        GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, 90);
        seeCrad = true;
        CheckCrad();
        usedEffect = true;
    }
    public void DontUseEffect()
    {
        GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, 0);
        seeCrad = true;
        CheckCrad();
        usedEffect = false;
    }
    public void AddEffectCard(string nameEffect)
    {
        if (thisCardInfo.card_effect)
        {
            usedEffect = false;
            useEffectButton.onClick.AddListener(delegate { UseEffect(); });
            switch (nameEffect)
            {
                case "HP +1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetHP(thisCardInfo.effect_value); });
                    break;
                case "HP +2":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetHP(thisCardInfo.effect_value); });
                    break;
                case "Card +1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetCard(thisCardInfo.effect_value); });
                    break;
                case "Card +2":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetCard(thisCardInfo.effect_value); });
                    break;
                case "Arrange 3 cards":
                    useEffectButton.onClick.AddListener(delegate { effectCard.SetThreeCard(); });
                    break;
                case "Destroy 1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.DestoryOneCrad(); });
                    break;
                case "Insert under pile 1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.InsertUnderPile(); });
                    break;
                case "Double 1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.DoublePower(); });
                    break;
                case "Phase -1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.DownPhase(); });
                    break;
                case "Exchange 1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.Exchange(1); });
                    break;
                case "Exchange 2":
                    useEffectButton.onClick.AddListener(delegate { effectCard.Exchange(2); });
                    break;
                case "Copy":
                    useEffectButton.onClick.RemoveAllListeners();
                    useEffectButton.onClick.AddListener(delegate { effectCard.CopyEffectCard(this.gameObject); });
                    break;
                case "HP -1":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetHP(thisCardInfo.effect_value); });
                    break;
                case "HP -2":
                    useEffectButton.onClick.AddListener(delegate { effectCard.GetHP(thisCardInfo.effect_value); });
                    break;
                default://stop และ Highcrad อยู่ใน PlayCardScript, . . . ไม่มีอะไร
                    break;
            }
        }
    }
    private void Start()
    {
        seeUse = true;
        seeCrad = false;
        useEffectButton.gameObject.SetActive(false);
        destroyButton.gameObject.SetActive(false);
        image_card.sprite = thisCardInfo.artCard;
        AddEffectCard(thisCardInfo.card_effect_name);

        Debug.Log("no_fight_card_in_game : " + thisCardInfo.no_fight_card_in_game);
        useOnly = thisCardInfo.no_fight_card_in_game;

        text_name_use.text = thisCardInfo.name_use_card;
        text_power_use.text = thisCardInfo.power_card.ToString();
        text_effect.text = thisCardInfo.card_effect_name;
        text_destroy.text = thisCardInfo.use_hp_to_destroy.ToString();

        if (!useOnly)
        {
            text_name_fight.text = thisCardInfo.name_fight_card;
            text_num_crad.text = thisCardInfo.take_card.ToString();
            text_power_fight_1.text = thisCardInfo.power_enemy[0].ToString();
            text_power_fight_2.text = thisCardInfo.power_enemy[1].ToString();
            text_power_fight_3.text = thisCardInfo.power_enemy[2].ToString();
        }
        else
        {
            GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(0, 0, 0);

            text_name_fight.gameObject.SetActive(false);
            text_num_crad.gameObject.SetActive(false);
            text_power_fight_1.gameObject.SetActive(false);
            text_power_fight_2.gameObject.SetActive(false);
            text_power_fight_3.gameObject.SetActive(false);
        }

        destroyButton.onClick.AddListener(delegate { effectCard.gameObject.GetComponent<PlayCardScript>().
            DestroyCard(this.gameObject, thisCardInfo.use_hp_to_destroy); });


    }
    private void Update()
    {
        destroyButton.gameObject.SetActive(showDestroyButtom);
        if (thisCardInfo.powerDouble)
        {
            int i = thisCardInfo.power_card * 2;
            text_power_use.text = i.ToString();
        }
    }
}
