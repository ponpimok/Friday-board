using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCardScript : MonoBehaviour
{
    [SerializeField] private List<CardPlayScript> start_card;
    [SerializeField] private List<CardPlayScript> age_card_b;
    [SerializeField] private List<CardPlayScript> age_card_w;
    [SerializeField] private List<CardPlayScript> fight_card;

    public List<CardPlayScript> my_crad;
    public List<CardPlayScript> my_crad_used;
    public List<CardPlayScript> age_card_bs;
    public List<CardPlayScript> age_card_ws;
    public List<CardPlayScript> fight_card_s;
    public List<CardPlayScript> not_fight_now;
    private void ShuffleCards(List<CardPlayScript> deck, List<CardPlayScript> addto)
    {
        int num = deck.Count;
        for (int i = 0; i < num; i++)
        {
            int num_new = deck.Count;
            int r = Random.Range(0, num_new);
            addto.Add(deck[r]);
            deck.RemoveAt(r);
        }
    }
    private void Awake()
    {
        my_crad.Clear();
        my_crad_used.Clear();
        age_card_bs.Clear();
        age_card_ws.Clear();
        fight_card_s.Clear();
        not_fight_now.Clear();

        ShuffleCards(start_card, my_crad);
        ShuffleCards(age_card_b, age_card_bs);
        ShuffleCards(age_card_w, age_card_ws);
        ShuffleCards(fight_card, fight_card_s);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
