using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EffectCardScript : MonoBehaviour
{
    private PlayCardScript playCardScript;
    private DataCardScript dataCardScript;
    private TextHPScroipt textHPScroipt;

    public GameObject setThreeCardUI;
    public TextMeshProUGUI titleSetThreeCardUI;
    public List<CardPlayScript> threeCard;
    public List<GameObject> threeCardObj;
    [SerializeField] private Transform[] position;
    public Button[] threeCardButton;
    [HideInInspector] public List<CardPlayScript> gust;

    [HideInInspector] public int checkExchange;

    //gethp
    public void GetHP(int i)
    {
        playCardScript.hp.hp += i;
    }
    //getcrad
    public void GetCard(int i)
    {
        playCardScript.getCradFreeFromEffect += i;
    }
    //setthreecard
    public void SetThreeCard()
    {
        if (dataCardScript.my_crad.Count >= 3)
        {
            setThreeCardUI.SetActive(true);
            foreach (var item in threeCardButton)
            {
                item.gameObject.SetActive(true);
            }
            titleSetThreeCardUI.text = "Discard 1 card";
            for (int i = 0; i < 3; i++)
            {
                threeCard.Add(dataCardScript.my_crad[i]);
                GameObject newCard = Instantiate(playCardScript.cardPrefad, position[i]);
                newCard.GetComponent<CardObjScript>().thisCardInfo = threeCard[i];
                threeCardObj.Add(newCard);
                dataCardScript.my_crad.RemoveAt(i);
                int j = i;
                threeCardButton[i].onClick.AddListener(() => DiscardOne(j));
            }            
        }
    }
    private void DiscardOne(int i)
    {
        Debug.Log(i);
        dataCardScript.my_crad_used.Add(threeCard[i]);
        threeCard.RemoveAt(i);
        Destroy(threeCardObj[i]);
        threeCardObj.RemoveAt(i);

        titleSetThreeCardUI.text = "Arrange 3 cards";
        int num = 0;
        foreach (var item in threeCardButton)
        {
            if (item != threeCardButton[i])
            {
                item.onClick.RemoveAllListeners();
                int x = num;
                item.onClick.AddListener(()=> ArrangeCrad(x));
                num++;
                item.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Top";
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }
    private void ArrangeCrad(int i)
    {
        gust.Clear();
        foreach (var item in dataCardScript.my_crad)
        {
            gust.Add(item);
        }
        dataCardScript.my_crad.Clear();
        dataCardScript.my_crad.Add(threeCard[i]);
        if (i == 0)
        {
            dataCardScript.my_crad.Add(threeCard[1]);
        }
        else
        {
            dataCardScript.my_crad.Add(threeCard[0]);
        }
        foreach (var item in gust)
        {
            dataCardScript.my_crad.Add(item);
        }
        threeCard.Clear();
        foreach (var item in threeCardObj)
        {
            Destroy(item);
        }
        threeCardObj.Clear();
        setThreeCardUI.SetActive(false);
    }
    //destorycrad
    public void DestoryOneCrad()
    {
        playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = -1;
        playCardScript.destroyCradUI.SetActive(true);
        playCardScript.useEffect = true;
        int i = 0;
        foreach (var item in playCardScript.useCardFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);

            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(delegate
            {
                playCardScript.DestroyCard(showCrad.gameObject, 0);
            });
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => CountCardDestory(j, true)
            );
            i++;
        }
        foreach (var item in playCardScript.useCardNotFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);

            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(delegate {
                playCardScript.DestroyCard(showCrad.gameObject, 0);
            });
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => CountCardDestory(j, false)
            );
            i++;
        }
    }
    private void CountCardDestory(int i, bool free)
    {
        if (free)
        {
            Destroy(playCardScript.useCardFree[i].gameObject);
            playCardScript.useCardFree.RemoveAt(i);
        }
        else
        {
            i -= playCardScript.useCardFree.Count;
            Destroy(playCardScript.useCardNotFree[i].gameObject);
            playCardScript.useCardNotFree.RemoveAt(i);
        }
        foreach (var item in playCardScript.destoryList)
        {
            Destroy(item.gameObject);
        }
        playCardScript.destoryList.Clear();
    }
    //Insert under pile 1
    public void InsertUnderPile()
    {
        playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = -1;
        playCardScript.destroyCradUI.SetActive(true);
        playCardScript.useEffect = true;
        int i = 0;
        foreach (var item in playCardScript.useCardFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "InsertUnderPile";
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => InsertCrad(j, true)
            );
            i++;
        }
        foreach (var item in playCardScript.useCardNotFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "InsertUnderPile";
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => InsertCrad(j, false)
            );
            i++;
        }
    }
    private void InsertCrad(int i, bool free)
    {
        if (free)
        {
            Debug.Log(i);
            dataCardScript.my_crad.Add(playCardScript.useCardFree[i].GetComponent<CardObjScript>().thisCardInfo);
            Destroy(playCardScript.useCardFree[i].gameObject);
            playCardScript.useCardFree.RemoveAt(i);
            playCardScript.num_to_draw--;
        }
        else
        {
            Debug.Log(i);
            i -= playCardScript.useCardFree.Count;
            dataCardScript.my_crad.Add(playCardScript.useCardNotFree[i].GetComponent<CardObjScript>().thisCardInfo);
            Destroy(playCardScript.useCardNotFree[i].gameObject);
            playCardScript.useCardNotFree.RemoveAt(i);
        }
        foreach (var item in playCardScript.destoryList)
        {
            Destroy(item.gameObject);
        }
        playCardScript.destoryList.Clear();
        playCardScript.destroyCradUI.GetComponent<TextDestroyCradScript>().pointHave = 0;
        playCardScript.destroyCradUI.SetActive(false);
        playCardScript.useEffect = false;
    }
    //Double 1
    public void DoublePower()
    {
        playCardScript.doubleUI.SetActive(true);
        playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().use = 0;
        playCardScript.useEffect = true;
        int i = 0;
        foreach (var item in playCardScript.useCardFree)
        {
            if (!item.GetComponent<CardObjScript>().thisCardInfo.powerDouble)
            {
                GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
                playCardScript.destoryList.Add(showCrad);
                showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
                showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "DoublePower";
                int j = i;
                showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                    () => Double(j, true)
                );
            }
            i++;
        }
        foreach (var item in playCardScript.useCardNotFree)
        {
            if (!item.GetComponent<CardObjScript>().thisCardInfo.powerDouble)
            {
                GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
                playCardScript.destoryList.Add(showCrad);
                showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
                showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "DoublePower";
                int j = i;
                showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                    () => Double(j, false)
                );
            }
            i++;
        }
    }
    private void Double(int i, bool free)
    {
        if (free)
        {
            playCardScript.useCardFree[i].GetComponent<CardObjScript>().thisCardInfo.powerDouble = true;
        }
        else
        {
            i -= playCardScript.useCardFree.Count;
            playCardScript.useCardNotFree[i].GetComponent<CardObjScript>().thisCardInfo.powerDouble = true;
        }
        foreach (var item in playCardScript.destoryList)
        {
            Destroy(item.gameObject);
        }
        playCardScript.destoryList.Clear();
        playCardScript.doubleUI.SetActive(false);
        playCardScript.useEffect = false;
    }
    //Phase -1
    public void DownPhase()
    {
        playCardScript.effectDownPhase += 1;
    }
    //Exchange 2
    public void Exchange(int k)
    {
        playCardScript.doubleUI.SetActive(true);
        playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().use = 1;
        playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().numExCard = k;
        playCardScript.doubleUIButton.gameObject.SetActive(true);
        playCardScript.useEffect = true;
        checkExchange = k;
        int i = 0;
        foreach (var item in playCardScript.useCardFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exchange";
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => ExchangeCrad(j, true)
            );
            Debug.Log(i);
            i++;
        }
        foreach (var item in playCardScript.useCardNotFree)
        {
            GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
            playCardScript.destoryList.Add(showCrad);
            showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
            showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
            showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Exchange";
            int j = i;
            showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                () => ExchangeCrad(j, false)
            );
            Debug.Log(i);
            i++;
        }
    }
    private void ExchangeCrad(int k, bool free)
    {
        if (free)
        {
            dataCardScript.my_crad_used.Add(playCardScript.useCardFree[k].GetComponent<CardObjScript>().thisCardInfo);
            Destroy(playCardScript.useCardFree[k]);
            playCardScript.num_to_draw--;
            Destroy(playCardScript.destoryList[k].gameObject);
            //playCardScript.useCardFree.RemoveAt(k);
        }
        else
        {
            k -= playCardScript.useCardFree.Count;
            dataCardScript.my_crad_used.Add(playCardScript.useCardNotFree[k].GetComponent<CardObjScript>().thisCardInfo);
            Destroy(playCardScript.useCardNotFree[k]);
            playCardScript.exchangeEffect++;
            Destroy(playCardScript.destoryList[k + playCardScript.useCardFree.Count].gameObject);
            //playCardScript.useCardNotFree.RemoveAt(k);
        }

        checkExchange--;
        if (checkExchange == 0)
        {
            foreach (var item in playCardScript.useCardFree)
            {
                if (item == null)
                {
                    playCardScript.useCardFree.Remove(item);
                }
            }
            foreach (var item in playCardScript.useCardNotFree)
            {
                if (item == null)
                {
                    playCardScript.useCardNotFree.Remove(item);
                }
            }
            foreach (var item in playCardScript.destoryList)
            {
                Destroy(item.gameObject);
            }
            playCardScript.destoryList.Clear();
            playCardScript.doubleUI.SetActive(false);
            playCardScript.useEffect = false;
        }
    }
    //Copy
    public void CopyEffectCard(GameObject thisCard)
    {
        playCardScript.doubleUI.SetActive(true);
        playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().use = 2;
        playCardScript.useEffect = true;
        playCardScript.doubleUIButton.gameObject.SetActive(true);
        int i = 0;
        foreach (var item in playCardScript.useCardFree)
        {
            if (item.GetComponent<CardObjScript>().thisCardInfo.card_effect &&
                item.GetComponent<CardObjScript>().thisCardInfo.card_effect_name != "Copy")
            {
                GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
                playCardScript.destoryList.Add(showCrad);
                showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
                showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Copy";
                int j = i;
                showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                    () => ChageEffect(j, true, thisCard)
                );
            }
            i++;
        }
        foreach (var item in playCardScript.useCardNotFree)
        {
            if (item.GetComponent<CardObjScript>().thisCardInfo.card_effect &&
                item.GetComponent<CardObjScript>().thisCardInfo.card_effect_name != "Copy")
            {
                GameObject showCrad = Instantiate(item, playCardScript.doubleUI.GetComponent<TextDoubleUIScript>().showCrad);
                playCardScript.destoryList.Add(showCrad);
                showCrad.GetComponent<CardObjScript>().showDestroyButtom = true;
                showCrad.GetComponent<RectTransform>().transform.localScale = new Vector3(1, 1, 1);
                showCrad.GetComponent<CardObjScript>().destroyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Copy";
                int j = i;
                showCrad.GetComponent<CardObjScript>().destroyButton.onClick.AddListener(
                    () => ChageEffect(j, false, thisCard)
                );
            }
            i++;
        }
    }
    private void ChageEffect(int i, bool free, GameObject card)
    {
        CardObjScript cardObjScript = card.GetComponent<CardObjScript>();
        if (free)
        {
            //name
            cardObjScript.text_effect.text =
                playCardScript.useCardFree[i].GetComponent<CardObjScript>().thisCardInfo.card_effect_name;
            //effect
            cardObjScript.useEffectButton.onClick.RemoveAllListeners();
            cardObjScript.useEffectButton.onClick.AddListener(delegate { cardObjScript.UseEffect(); });
            cardObjScript.AddEffectCard(playCardScript.useCardFree[i].GetComponent<CardObjScript>().thisCardInfo.card_effect_name);
        }
        else
        {
            i -= playCardScript.useCardFree.Count;
            //name
            cardObjScript.text_effect.text =
                playCardScript.useCardNotFree[i].GetComponent<CardObjScript>().thisCardInfo.card_effect_name;
            //effect
            cardObjScript.useEffectButton.onClick.RemoveAllListeners();
            cardObjScript.useEffectButton.onClick.AddListener(delegate { cardObjScript.UseEffect(); });
            cardObjScript.AddEffectCard(playCardScript.useCardNotFree[i].GetComponent<CardObjScript>().thisCardInfo.card_effect_name);
        }
        cardObjScript.DontUseEffect();
        foreach (var item in playCardScript.destoryList)
        {
            Destroy(item.gameObject);
        }
        playCardScript.destoryList.Clear();
        playCardScript.doubleUI.SetActive(false);
        playCardScript.useEffect = false;
    }
    //ageCrad in PlayCardScript
    private void Awake()
    {
        playCardScript = GetComponent<PlayCardScript>();
        dataCardScript = GetComponent<DataCardScript>();
        textHPScroipt = playCardScript.hp;
        setThreeCardUI.SetActive(false);
        checkExchange = 0;
    }
}
