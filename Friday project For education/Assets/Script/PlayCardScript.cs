using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayCardScript : MonoBehaviour
{
    private DataCardScript dataCardScript;
    [SerializeField] private TextHPScroipt hp;

    public int phase;//0,1,2 ex

    [SerializeField] private GameObject cardPrefad;
    private GameObject newCard1;
    private GameObject newCard2;
    private GameObject fightThis;
    [SerializeField] private Transform[] showCard;//1,2,3
    [SerializeField] private GameObject[] buttonChoseUI;

    [HideInInspector] public List<GameObject> useCardFree;
    [SerializeField] private Transform positionCardFree;
    [HideInInspector] public List<GameObject> useCardNotFree;
    [SerializeField] private Transform positionCardNotFree;
    private int num_to_draw;
    [SerializeField] private GameObject warnUI;

    private void DrawCardDangerous()
    {
        buttonChoseUI[3].gameObject.SetActive(false);
        if (dataCardScript.fight_card_s.Count > 1)
        {
            newCard1 = Instantiate(cardPrefad, showCard[0]);
            newCard2 = Instantiate(cardPrefad, showCard[1]);
            newCard1.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[0];
            newCard2.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[1];
            dataCardScript.fight_card_s.RemoveAt(1);
            dataCardScript.fight_card_s.RemoveAt(0);

            buttonChoseUI[0].SetActive(true);
            buttonChoseUI[1].SetActive(true);
            buttonChoseUI[2].SetActive(false);
        }
        if (dataCardScript.fight_card_s.Count == 1)
        {
            GameObject newCard1 = Instantiate(cardPrefad, showCard[2]);
            newCard1.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[0];
            dataCardScript.fight_card_s.RemoveAt(0);

            buttonChoseUI[0].SetActive(true);
            buttonChoseUI[1].SetActive(false);
            buttonChoseUI[2].SetActive(true);
        }
        else
        {
            DontChose();
            //เปลี่ยนเฟส
        }
    }
    public void ChoseThisCard(bool i)
    {
        if (!i)
        {
            fightThis = Instantiate(newCard1, showCard[2]);
            dataCardScript.not_fight_now.Add(newCard2.GetComponent<CardObjScript>().thisCardInfo);
        }
        else
        {
            fightThis = Instantiate(newCard2, showCard[2]);
            dataCardScript.not_fight_now.Add(newCard1.GetComponent<CardObjScript>().thisCardInfo);
        }
        Destroy(newCard1);
        Destroy(newCard2);
        foreach (var item in buttonChoseUI)
        {
            item.gameObject.SetActive(false);
        }
        buttonChoseUI[3].gameObject.SetActive(true);
    }
    public void DontChose()
    {
        //เปลี่ยนเฟส
    }
    public void DrawCardUse()
    {
        if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card > num_to_draw)
        {
            num_to_draw++;
            if (dataCardScript.my_crad.Count > 0)
            {
                if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card == num_to_draw)
                {
                    warnUI.SetActive(true);//เตือนว่าจะเสียเลือด
                }//เตือนว่าจะเสียเลือด
                GameObject newCradUseFree = Instantiate(cardPrefad, positionCardFree);
                newCradUseFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                dataCardScript.my_crad.RemoveAt(0);
                useCardFree.Add(newCradUseFree);
            }
        }
        else
        {
            if (dataCardScript.my_crad.Count > 0)
            {
                hp.hp--;
                GameObject newCradUseNotFree = Instantiate(cardPrefad, positionCardNotFree);
                newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                dataCardScript.my_crad.RemoveAt(0);
                useCardNotFree.Add(newCradUseNotFree);
            }
        }
    }
    public void CheckPower()
    {
        int powerAll = 0;
        foreach (var item in useCardFree)
        {
            powerAll += item.GetComponent<CardObjScript>().thisCardInfo.power_card;
        }
        foreach (var item in useCardNotFree)
        {
            powerAll += item.GetComponent<CardObjScript>().thisCardInfo.power_card;
        }
        if (fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] <= powerAll)
        {
            //ผ่าน
            CollectCardBack();
        }
        else
        {
            hp.hp -= fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] - powerAll;
            CollectCardBack();
            //ไม่ผ่าน ลดเลือด
        }
        buttonChoseUI[3].SetActive(false);
        warnUI.SetActive(false);
        DrawCardDangerous();
    }
    private void CollectCardBack()
    {
        num_to_draw = 0;
        dataCardScript.my_crad_used.Add(fightThis.GetComponent<CardObjScript>().thisCardInfo);
        Destroy(fightThis);
        foreach (var item in useCardFree)
        {
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        foreach (var item in useCardNotFree)
        {
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        useCardFree.Clear();
        useCardNotFree.Clear();
    }

    private void Awake()
    {
        dataCardScript = GetComponent<DataCardScript>();
    }
    private void Start()
    {
        phase = 0;

        foreach (var item in buttonChoseUI)
        {
            item.gameObject.SetActive(false);
        }
        useCardFree.Clear();
        num_to_draw = 0;
        warnUI.SetActive(false);

        DrawCardDangerous();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
