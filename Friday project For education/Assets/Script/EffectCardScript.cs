using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class EffectCardScript : MonoBehaviour
{
    private PlayCardScript playCardScript;
    private TextHPScroipt textHPScroipt;

    public GameObject setThreeCardUI;
    public TextMeshProUGUI titleSetThreeCardUI;
    public List<CardPlayScript> threeCard;
    public List<GameObject> threeCardObj;
    [SerializeField] private Transform[] position;
    public Button[] threeCardButton;
    [HideInInspector] public List<CardPlayScript> gust;

    public void GetHP(int i)
    {
        playCardScript.hp.hp += i;
    }
    public void GetCard(int i)
    {
        playCardScript.getCradFreeFromEffect += i;
    }
    public void SetThreeCard()
    {
        if (playCardScript.dataCardScript.my_crad.Count >= 3)
        {
            setThreeCardUI.SetActive(true);
            foreach (var item in threeCardButton)
            {
                item.gameObject.SetActive(true);
            }
            titleSetThreeCardUI.text = "Discard 1 card";
            for (int i = 0; i < 3; i++)
            {
                threeCard.Add(playCardScript.dataCardScript.my_crad[i]);
                GameObject newCard = Instantiate(playCardScript.cardPrefad, position[i]);
                newCard.GetComponent<CardObjScript>().thisCardInfo = threeCard[i];
                threeCardObj.Add(newCard);
                playCardScript.dataCardScript.my_crad.RemoveAt(i);
                int j = i;
                threeCardButton[i].onClick.AddListener(() => DiscardOne(j));
            }            
        }
    }
    private void DiscardOne(int i)
    {
        Debug.Log(i);
        playCardScript.dataCardScript.my_crad_used.Add(threeCard[i]);
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
        foreach (var item in playCardScript.dataCardScript.my_crad)
        {
            gust.Add(item);
        }
        playCardScript.dataCardScript.my_crad.Clear();
        playCardScript.dataCardScript.my_crad.Add(threeCard[i]);
        if (i == 0)
        {
            playCardScript.dataCardScript.my_crad.Add(threeCard[1]);
        }
        else
        {
            playCardScript.dataCardScript.my_crad.Add(threeCard[0]);
        }
        foreach (var item in gust)
        {
            playCardScript.dataCardScript.my_crad.Add(item);
        }
        threeCard.Clear();
        foreach (var item in threeCardObj)
        {
            Destroy(item);
        }
        threeCardObj.Clear();
        setThreeCardUI.SetActive(false);
    }
    //public void CopyEffectCard()
    //{

    //}
    //public void 
    private void Awake()
    {
        playCardScript = GetComponent<PlayCardScript>();
        textHPScroipt = playCardScript.hp;
        setThreeCardUI.SetActive(false);
    }
}
