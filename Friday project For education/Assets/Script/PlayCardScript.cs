using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class PlayCardScript : MonoBehaviour
{
    [Header("Reg Script")]
    private DataCardScript dataCardScript;
    private EffectCardScript effectCardScript;
    public TextHPScroipt hp;

    [Header("Check Value")]
    public int phase;//0,1,2 ,P3,P
    public int effectDownPhase;

    [Header("Reg Obj")]
    public GameObject cardPrefad;
    private GameObject newCard1;
    private GameObject newCard2;
    public GameObject fightThis;
    [SerializeField] private Transform[] showCard;//1,2,3
    [SerializeField] private GameObject[] buttonChoseUI;
    [SerializeField] private GameObject[] phaseUI;

    [Header("Check Card")]
    public List<GameObject> useCardFree;
    [SerializeField] private Transform positionCardFree;
    public List<GameObject> useCardNotFree;
    [SerializeField] private Transform positionCardNotFree;
    [HideInInspector] public int num_to_draw;
    [SerializeField] private GameObject warnUI;

    public List<GameObject> destoryList;
    public GameObject destroyCradUI;
    private int pointToDestroy;

    [Header("Effect Card")]
    [HideInInspector] public int getCradFreeFromEffect;
    [HideInInspector] public bool useEffect;

    public GameObject doubleUI;
    public Button doubleUIButton;
    [HideInInspector] public int exchangeEffect;
    [SerializeField] private GameObject warnAgeCardUI;


    [Header("Pirate")]
    [SerializeField] private GameObject piratePrefad;
    [SerializeField] private List<GameObject> pirateObj;
    [SerializeField] private Button[] pirateChackButtom;
    [SerializeField] private GameObject piratChoseUI;
    [SerializeField] private Button[] pirateChoseButtom;
    private bool seePirate;
    [SerializeField] private Transform[] positionShowPirate;
    private bool fightPirate;
    public GameObject warnPirateUI;

    private void DrawCardDangerous()
    {
        buttonChoseUI[3].gameObject.SetActive(false);
        if (dataCardScript.fight_card_s.Count > 1)
        {
            newCard1 = Instantiate(cardPrefad, showCard[0]);
            newCard2 = Instantiate(cardPrefad, showCard[1]);
            newCard1.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[0];
            newCard1.GetComponent<CardObjScript>().thisCardInfo.no_fight_card_in_game = false;
            newCard1.GetComponent<CardObjScript>().effectCard = effectCardScript;
            newCard2.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[1];
            newCard2.GetComponent<CardObjScript>().thisCardInfo.no_fight_card_in_game = false;
            newCard2.GetComponent<CardObjScript>().effectCard = effectCardScript;
            dataCardScript.fight_card_s.RemoveAt(1);
            dataCardScript.fight_card_s.RemoveAt(0);

            buttonChoseUI[0].SetActive(true);
            buttonChoseUI[1].SetActive(true);
            buttonChoseUI[2].SetActive(false);
        }//เหลือมากกว่า 1
        else if (dataCardScript.fight_card_s.Count == 1)
        {
            newCard1 = Instantiate(cardPrefad, showCard[2]);
            newCard1.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.fight_card_s[0];
            newCard1.GetComponent<CardObjScript>().thisCardInfo.no_fight_card_in_game = false;
            newCard1.GetComponent<CardObjScript>().effectCard = effectCardScript;
            dataCardScript.fight_card_s.RemoveAt(0);

            buttonChoseUI[0].SetActive(true);
            buttonChoseUI[1].SetActive(false);
            buttonChoseUI[2].SetActive(true);
        }//เหลือ 1
        else
        {
            ChangePhase();
            Debug.Log("Change Phase");
            //เปลี่ยนเฟส
        }//การ์ดหมด
    }
    public void ChoseThisCard(bool i)
    {
        if (!i)
        {
            fightThis = Instantiate(newCard1, showCard[2]);
            fightThis.GetComponent<CardObjScript>().effectCard = effectCardScript;
            if (newCard2 != null)
            {
                dataCardScript.not_fight_now.Add(newCard2.GetComponent<CardObjScript>().thisCardInfo);
            }
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
    }
    public void DontChose()
    {
        dataCardScript.not_fight_now.Add(newCard1.GetComponent<CardObjScript>().thisCardInfo);
        Destroy(newCard1);
        ChangePhase();
    }
    private void ChangePhase()
    {
        //เปลี่ยนเฟส
        if (phase <= 1)
        {
            phase++;
            showPhase(phase);
            dataCardScript.ShuffleCards(dataCardScript.not_fight_now, dataCardScript.fight_card_s);
            DrawCardDangerous();
        }//play nomal
        else
        {
            piratChoseUI.SetActive(true);
            pirateChoseButtom[0].gameObject.SetActive(true);
            pirateChoseButtom[1].gameObject.SetActive(true);
        }//pirat
    }
    public void DrawCardUse()
    {
        if (dataCardScript.my_crad.Count > 0)
        {
            if (!fightPirate)
            {
                if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card > num_to_draw)
                {
                    num_to_draw++;
                    if (dataCardScript.my_crad.Count > 0)
                    {
                        GameObject newCradUseFree = Instantiate(cardPrefad, positionCardFree);
                        newCradUseFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                        newCradUseFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                        newCradUseFree.GetComponent<CardObjScript>().effectCard = effectCardScript;

                        dataCardScript.my_crad[0] = null;
                        dataCardScript.my_crad.RemoveAt(0);
                        UnityEditor.EditorUtility.SetDirty(this);

                        useCardFree.Add(newCradUseFree);

                        if (newCradUseFree.GetComponent<CardObjScript>().thisCardInfo.card_effect_name == "Stop")
                        {
                            num_to_draw = fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card;
                        }//หยุดจั่วจากอายุ
                    }
                }//จั่วฟรี
                else
                {
                    if (dataCardScript.my_crad.Count > 0)
                    {
                        if (getCradFreeFromEffect <= 0 && exchangeEffect <= 0)
                        {
                            //ใส่ผลจากเรือโจรสลัด
                            hp.hp--;
                        }
                        else if (getCradFreeFromEffect > 0)
                        {
                            getCradFreeFromEffect -= 1;
                        }//cardfree
                        else if (exchangeEffect > 0)
                        {
                            exchangeEffect -= 1;
                        }//exchange

                        GameObject newCradUseNotFree = Instantiate(cardPrefad, positionCardNotFree);
                        newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                        newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                        newCradUseNotFree.GetComponent<CardObjScript>().effectCard = effectCardScript;

                        dataCardScript.my_crad[0] = null;
                        dataCardScript.my_crad.RemoveAt(0);
                        UnityEditor.EditorUtility.SetDirty(this);

                        useCardNotFree.Add(newCradUseNotFree);
                    }
                }//จั่วเสียเลือด
            }
            else
            {
                if (fightThis.GetComponent<CardObjPirateScript>().thisCardInfo.take_card > num_to_draw)
                {
                    num_to_draw++;
                    if (dataCardScript.my_crad.Count > 0)
                    {
                        GameObject newCradUseFree = Instantiate(cardPrefad, positionCardFree);
                        newCradUseFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                        newCradUseFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                        newCradUseFree.GetComponent<CardObjScript>().effectCard = effectCardScript;

                        dataCardScript.my_crad[0] = null;
                        dataCardScript.my_crad.RemoveAt(0);
                        UnityEditor.EditorUtility.SetDirty(this);

                        useCardFree.Add(newCradUseFree);

                        if (newCradUseFree.GetComponent<CardObjScript>().thisCardInfo.card_effect_name == "Stop")
                        {
                            num_to_draw = fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card;
                        }//หยุดจั่วจากอายุ
                    }
                }//จั่วฟรี
                else
                {
                    if (dataCardScript.my_crad.Count > 0)
                    {
                        if (getCradFreeFromEffect <= 0 && exchangeEffect <= 0)
                        {
                            //ใส่ผลจากเรือโจรสลัด
                            hp.hp--;
                        }
                        else if (getCradFreeFromEffect > 0)
                        {
                            getCradFreeFromEffect -= 1;
                        }//cardfree
                        else if (exchangeEffect > 0)
                        {
                            exchangeEffect -= 1;
                        }//exchange

                        GameObject newCradUseNotFree = Instantiate(cardPrefad, positionCardNotFree);
                        newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                        newCradUseNotFree.GetComponent<CardObjScript>().thisCardInfo = dataCardScript.my_crad[0];
                        newCradUseNotFree.GetComponent<CardObjScript>().effectCard = effectCardScript;

                        dataCardScript.my_crad[0] = null;
                        dataCardScript.my_crad.RemoveAt(0);
                        UnityEditor.EditorUtility.SetDirty(this);

                        useCardNotFree.Add(newCradUseNotFree);
                    }
                }//จั่วเสียเลือด
            }
            
        }
        else if (dataCardScript.my_crad.Count == 0)
        {
            AddAgeCard();
        }//เพื่อการ์ดอายุ
    }
    public void CheckPower()
    {
        bool canEnd = true;
        warnAgeCardUI.SetActive(false);
        foreach (var item in useCardFree)
        {
            if (item.GetComponent<CardObjScript>().thisCardInfo.must_use_card &&
                item.GetComponent<CardObjScript>().usedEffect)
            {
                canEnd = false;
            }
        }
        foreach (var item in useCardNotFree)
        {
            if (item.GetComponent<CardObjScript>().thisCardInfo.must_use_card &&
                item.GetComponent<CardObjScript>().usedEffect)
            {
                canEnd = false;
            }
        }

        if (canEnd)
        {
            num_to_draw = 0;
            int powerAll = 0;
            List<int> findMax = new List<int>();
            bool highCradSetZero = false;
            foreach (var item in useCardFree)
            {
                if (item.GetComponent<CardObjScript>().thisCardInfo.card_effect_name == "Highcrad =0")
                {
                    highCradSetZero = true;
                }
                if (item.GetComponent<CardObjScript>().thisCardInfo.powerDouble)
                {
                    item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
                    findMax.Add(item.GetComponent<CardObjScript>().thisCardInfo.power_card * 2);
                }
                else
                {
                    findMax.Add(item.GetComponent<CardObjScript>().thisCardInfo.power_card);
                }
            }
            foreach (var item in useCardNotFree)
            {
                if (item.GetComponent<CardObjScript>().thisCardInfo.card_effect_name == "Highcrad =0")
                {
                    highCradSetZero = true;
                }
                findMax.Add(item.GetComponent<CardObjScript>().thisCardInfo.power_card);
            }
            if (highCradSetZero)
            {
                highCradSetZero = false;
                int i = findMax.Max();
                findMax.Remove(i);
            }
            foreach (var item in findMax)
            {
                powerAll += item;
            }
            Debug.Log("powerAll : " + powerAll);


            if (!fightPirate)
            {
                if (fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase - effectDownPhase] <= powerAll)
                {
                    //ผ่าน
                    fightThis.GetComponent<CardObjScript>().thisCardInfo.no_fight_card_in_game = true;
                    dataCardScript.my_crad_used.Add(fightThis.GetComponent<CardObjScript>().thisCardInfo);
                    Destroy(fightThis);
                    CollectCardBack();
                    DrawCardDangerous();
                }
                else
                {
                    dataCardScript.not_fight_now.Add(fightThis.GetComponent<CardObjScript>().thisCardInfo);
                    Destroy(fightThis);
                    hp.hp -= fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase - effectDownPhase] - powerAll;
                    if (fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase - effectDownPhase] - powerAll > 0)
                    {
                        pointToDestroy = fightThis.GetComponent<CardObjScript>().thisCardInfo.power_enemy[phase - effectDownPhase] - powerAll;
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
            }
            else
            {
                if (fightThis.GetComponent<CardObjPirateScript>().thisCardInfo.power_enemy <= powerAll)
                {
                    //not done
                    warnPirateUI.SetActive(false);
                    Debug.Log("Next!!");
                }//การโจรสลัดใบต่อไป
                else
                {
                    warnAgeCardUI.SetActive(false);
                    warnPirateUI.SetActive(true);
                }//บังคับจั่ว
            }
            buttonChoseUI[3].SetActive(false);
            warnUI.SetActive(false);
            effectDownPhase = 0;
        }
        else
        {
            warnPirateUI.SetActive(false);
            warnAgeCardUI.SetActive(true);
        }//not use age crad
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
    private void CollectCardBack()
    {
        foreach (var item in useCardFree)
        {
            item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        UnityEditor.EditorUtility.SetDirty(this);
        useCardFree.Clear();
        UnityEditor.EditorUtility.SetDirty(this);
        foreach (var item in useCardNotFree)
        {
            item.GetComponent<CardObjScript>().thisCardInfo.powerDouble = false;
            dataCardScript.my_crad_used.Add(item.GetComponent<CardObjScript>().thisCardInfo);
            Destroy(item);
        }
        UnityEditor.EditorUtility.SetDirty(this);
        useCardNotFree.Clear();
        UnityEditor.EditorUtility.SetDirty(this);
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
            dataCardScript.age_card_ws.RemoveAt(0);
            dataCardScript.ShuffleCards(dataCardScript.my_crad_used, dataCardScript.my_crad);
        }//age white
        else
        {
            //gameover
        }
    }//no don
    public void CancelDoubleUIButton()
    {
        effectCardScript.checkExchange = 0;
        foreach (var item in destoryList)
        {
            Destroy(item.gameObject);
        }
        destoryList.Clear();
        doubleUIButton.gameObject.SetActive(false);
        doubleUI.SetActive(false);
        useEffect = false;
    }//CancelExchange
    private void showPhase(int p)
    {
        for (int i = 0; i < phaseUI.Length; i++)
        {
            phaseUI[i].SetActive(false);
        }
        phaseUI[phase].SetActive(true);
    }
    public void CheckCradPirateButtom()
    {
        if (!seePirate)
        {
            seePirate = true;
        }
        else
        {
            seePirate = false;
        }
        piratChoseUI.SetActive(seePirate);
    }
    public void ChosePiratButtom(bool left)
    {
        fightPirate = true;
        if (left)
        {
            pirateObj[0].transform.SetParent(showCard[2]);
            pirateObj[0].transform.position = showCard[2].transform.position;
            fightThis = pirateObj[0];
        }
        else
        {
            pirateObj[1].transform.SetParent(showCard[2]);
            pirateObj[1].transform.position = showCard[2].transform.position;
            fightThis = pirateObj[1];
        }
        buttonChoseUI[0].gameObject.SetActive(false);
        buttonChoseUI[1].gameObject.SetActive(false);
        buttonChoseUI[2].gameObject.SetActive(false);
        buttonChoseUI[3].gameObject.SetActive(true);
        piratChoseUI.SetActive(false);
    }
    private void Awake()
    {
        dataCardScript = GetComponent<DataCardScript>();
        effectCardScript = GetComponent<EffectCardScript>();
    }
    private void Start()
    {
        phase = 2;
        showPhase(phase);
        getCradFreeFromEffect = 0;
        useEffect = false;
        effectDownPhase = 0;
        exchangeEffect = 0;

        seePirate = false;
        fightPirate = false;
        pirateChackButtom[0].gameObject.GetComponent<Image>().sprite = dataCardScript.pirate_card_s[0].image;
        pirateChackButtom[1].gameObject.GetComponent<Image>().sprite = dataCardScript.pirate_card_s[1].image;
        pirateChoseButtom[0].gameObject.SetActive(false);
        pirateChoseButtom[1].gameObject.SetActive(false);
        piratChoseUI.SetActive(false);
        GameObject obj = Instantiate(piratePrefad, positionShowPirate[0]);
        obj.GetComponent<CardObjPirateScript>().thisCardInfo = dataCardScript.pirate_card_s[0];
        pirateObj.Add(obj);
        obj = Instantiate(piratePrefad, positionShowPirate[1]);
        obj.GetComponent<CardObjPirateScript>().thisCardInfo = dataCardScript.pirate_card_s[1];
        pirateObj.Add(obj);
        warnPirateUI.SetActive(false);

        foreach (var item in buttonChoseUI)
        {
            item.gameObject.SetActive(false);
        }
        useCardFree.Clear();
        destoryList.Clear();
        num_to_draw = 0;
        warnUI.SetActive(false);
        warnAgeCardUI.SetActive(false);
        destroyCradUI.SetActive(false);
        doubleUI.SetActive(false);
        doubleUIButton.gameObject.SetActive(false);

        DrawCardDangerous();
    }
    private void Update()
    {
        if (fightThis != null)
        {
            if (!fightPirate)
            {
                if (fightThis.GetComponent<CardObjScript>().thisCardInfo.take_card == num_to_draw &&
                    exchangeEffect <= 0 &&
                    getCradFreeFromEffect <= 0)
                {
                    warnUI.SetActive(true);//เตือนว่าจะเสียเลือด
                }//เตือนว่าจะเสียเลือด
                else
                {
                    warnUI.SetActive(false);
                }
            }
            else
            {
                if (fightThis.GetComponent<CardObjPirateScript>().thisCardInfo.take_card == num_to_draw &&
                    exchangeEffect <= 0 &&
                    getCradFreeFromEffect <= 0)
                {
                    warnUI.SetActive(true);//เตือนว่าจะเสียเลือด
                }//เตือนว่าจะเสียเลือด
                else
                {
                    warnUI.SetActive(false);
                }
            }
        }
    }
}
