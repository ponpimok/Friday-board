using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class PlayCardScript : MonoBehaviour
{
    private DataCardScript dataCardScript;
    private EffectCardScript effectCardScript;
    public TextHPScroipt hp;

    public int playOrder = 0;
    public int phase;//0,1,2 ex
    public int effectDownPhase;

    public GameObject cardPrefad;
    private GameObject newCard1;
    private GameObject newCard2;
    private GameObject fightThis;
    [SerializeField] private Transform[] showCard;//1,2,3
    [SerializeField] private GameObject[] buttonChoseUI;

    public List<GameObject> useCardFree;
    [SerializeField] private Transform positionCardFree;
    public List<GameObject> useCardNotFree;
    [SerializeField] private Transform positionCardNotFree;
    [HideInInspector] public int num_to_draw;
    [SerializeField] private GameObject warnUI;

    public List<GameObject> destoryList;
    public GameObject destroyCradUI;
    private int pointToDestroy;

    //effect card
    [HideInInspector] public int getCradFreeFromEffect;
    [HideInInspector] public bool useEffect;

    public GameObject doubleUI;

    private void DrawCardDangerous()
    {
        playOrder = 1;
        buttonChoseUI[3].gameObject.SetActive(false);
        if (dataCardScript.fight_card_s.Count > 1)
        {
            newCard1 = Instantiate(cardPrefad, showCard[0]);
            newCard2 = Instantiate(cardPrefad, showCard[1]);
            newCard1.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[0];
            newCard1.GetComponent<CardObjScript>().effectCard = effectCardScript;
            newCard2.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[1];
            newCard2.GetComponent<CardObjScript>().effectCard = effectCardScript;
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
            newCard1.GetComponent<CardObjScript>().effectCard = effectCardScript;
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
            fightThis.GetComponent<CardObjScript>().effectCard = effectCardScript;
            dataCardScript.not_fight_now.Add(newCard2.GetComponent<CardObjScript>().thisCardInfo);
        }
        else
        {
            fightThis = Instantiate(newCard2, showCard[2]);
            fightThis.GetComponent<CardObjScript>().effectCard = effectCardScript;
            dataCardScript.not_fight_now.Add(newCard1.GetComponent<CardObjScript>().thisCardInfo);
        }
        Destroy(newCard1);
        Destroy(newCard2);
        foreach (var item in buttonChoseUI)
        {
            item.gameObject.SetActive(false);
        }
        buttonChoseUI[3].gameObject.SetActive(true);
        playOrder = 2;
    }
    public void DontChose()
    {
        //เปลี่ยนเฟส
        phase++;
    }
    public void DrawCardUse()
    {
        if (playOrder == 2 && dataCardScript.my_crad.Count > 0)
        {
            if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card > num_to_draw)
            {
                num_to_draw++;
                Debug.Log("num_to_draw : " + num_to_draw);
                if (dataCardScript.my_crad.Count > 0)
                {
                    if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card == num_to_draw)
                    {
                        warnUI.SetActive(true);//เตือนว่าจะเสียเลือด
                    }//เตือนว่าจะเสียเลือด
                    GameObject newCradUseFree = Instantiate(cardPrefad, positionCardFree);
                    newCradUseFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                    newCradUseFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                    newCradUseFree.GetComponent<CardObjScript>().effectCard = effectCardScript;
                    dataCardScript.my_crad.RemoveAt(0);
                    EditorUtility.SetDirty(dataCardScript);
                    EditorUtility.SetDirty(effectCardScript);
                    EditorUtility.SetDirty(this);
                    useCardFree.Add(newCradUseFree);
                }
            }
            else
            {
                if (dataCardScript.my_crad.Count > 0)
                {
                    if (getCradFreeFromEffect <= 0)
                    {
                        hp.hp--;
                    }
                    else
                    {
                        getCradFreeFromEffect -= 0;
                    }
                    GameObject newCradUseNotFree = Instantiate(cardPrefad, positionCardNotFree);
                    newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                    newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                    newCradUseNotFree.GetComponent<CardObjScript>().effectCard = effectCardScript;
                    dataCardScript.my_crad.RemoveAt(0);
                    EditorUtility.SetDirty(dataCardScript);
                    EditorUtility.SetDirty(effectCardScript);
                    EditorUtility.SetDirty(this);
                    useCardNotFree.Add(newCradUseNotFree);
                }
            }
        }
        else if (playOrder == 2 && dataCardScript.my_crad.Count == 0)
        {
            AddAgeCard();
        }
    }
    public void CheckPower()
    {
        num_to_draw = 0;
        int powerAll = 0;
        foreach (var item in useCardFree)
        {
            if (item.GetComponent<CardObjScript>().thisCardInfo.powerDouble)
            {
                item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                int p = item.GetComponent<CardObjScript>().thisCardInfo.power_card * 2;
                item.GetComponent<CardObjScript>().text_power_use.text = p.ToString();
                powerAll += p;
            }
            else
            {
                powerAll += item.GetComponent<CardObjScript>().thisCardInfo.power_card;
            }
        }
        foreach (var item in useCardNotFree)
        {
            powerAll += item.GetComponent<CardObjScript>().thisCardInfo.power_card;
        }
        Debug.Log("powerAll : " + powerAll);
        if (phase != 0)
        {
            phase -= effectDownPhase;
        }
        if (fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] <= powerAll)
        {
            //ผ่าน
            fightThis.GetComponent<CardObjScript>().thisCardInfo.no_fight_card = true;
            dataCardScript.my_crad_used.Add(fightThis.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(fightThis);
            CollectCardBack();
            DrawCardDangerous();
        }
        else
        {
            dataCardScript.not_fight_now.Add(fightThis.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(fightThis);
            hp.hp -= fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] - powerAll;
            if (fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] - powerAll > 0)
            {
                pointToDestroy = fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase] - powerAll;
                destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = pointToDestroy;
                destroyCradUI.SetActive(true);
                foreach (var item in useCardFree)
                {
                    GameObject showCrad = Instantiate(item, destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);
                    Destroy(item);
                    destoryList.Add(showCrad);
                }
                foreach (var item in useCardNotFree)
                {
                    GameObject showCrad = Instantiate(item, destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);                    
                    Destroy(item);
                    destoryList.Add(showCrad);
                }
                useCardFree.Clear();
                useCardNotFree.Clear();
                foreach (var item in destoryList)
                {
                    item.GetComponent<CardObjScript>().showDestroyButtom = true;
                }
            }
            //ไม่ผ่าน ลดเลือด
        }
        buttonChoseUI[3].SetActive(false);
        warnUI.SetActive(false);
        effectDownPhase = 0;
        playOrder = 3;
    }
    public void DestroyCard(GameObject gameObject, int use_point)
    {
        if (pointToDestroy - use_point >= 0)
        {
            destoryList.Remove(gameObject);
            Destroy(gameObject);

            pointToDestroy -= use_point;
            destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = pointToDestroy;
            if ((pointToDestroy == 0 || destoryList.Count == 0) && !useEffect)
            {
                DontDestroyCard();
            }
            else if(useEffect)
            {
                destroyCradUI.SetActive(false);
                useEffect = false;
            }
        }

        foreach (var item in destoryList)
        {
            item.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(delegate
            {
                item.GetComponent<CardObjScript>().effectCard.gameObject.GetComponent<PlayCardScript>().
                DestroyCard(this.gameObject, item.GetComponent<CardObjScript>().thisCardInfo.use_hp_to_destroy);
            });
        }//ให้ค่ากลับมาเท่าเดิม
    }
    public void DontDestroyCard()
    {
        destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = 0;
        destroyCradUI.SetActive(false);
        getCradFreeFromEffect = 0;

        if (destoryList.Count != 0)
        {
            foreach (var item in destoryList)
            {
                dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
                Destroy(item);
            }
            destoryList.Clear();
        }
        DrawCardDangerous();
    }
    //public void CancelDoubleButton()
    //{
    //    foreach (var item in destoryList)
    //    {
    //        Destroy(item);
    //    }
    //    destoryList.Clear();
    //    doubleUI.SetActive(false);
    //}
    private void CollectCardBack()
    {
        foreach (var item in useCardFree)
        {
            item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        foreach (var item in useCardNotFree)
        {
            item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        useCardFree.Clear();
        useCardNotFree.Clear();
    }
    private void AddAgeCard()
    {
        if (dataCardScript.age_card_bs.Count > 0)
        {
            dataCardScript.my_crad_used.Add(dataCardScript.age_card_bs[0]);
            dataCardScript.age_card_bs.RemoveAt(0);
            dataCardScript.ShuffleCards(dataCardScript.my_crad_used, dataCardScript.my_crad);
        }//add age black
        else if (dataCardScript.age_card_ws.Count > 0)
        {
            dataCardScript.my_crad_used.Add(dataCardScript.age_card_ws[0]);
            dataCardScript.age_card_bs.RemoveAt(0);
            dataCardScript.ShuffleCards(dataCardScript.my_crad_used, dataCardScript.my_crad);
        }//age white
        else
        {
            //gameover
        }

    }//no don
    private void Awake()
    {
        dataCardScript = GetComponent<DataCardScript>();
        effectCardScript = GetComponent<EffectCardScript>();
    }
    private void Start()
    {
        phase = 0;
        getCradFreeFromEffect = 0;
        useEffect = false;
        effectDownPhase = 0;

        foreach (var item in buttonChoseUI)
        {
            item.gameObject.SetActive(false);
        }
        useCardFree.Clear();
        destoryList.Clear();
        num_to_draw = 0;
        warnUI.SetActive(false);
        destroyCradUI.SetActive(false);
        doubleUI.SetActive(false);

        DrawCardDangerous();
    }
}
