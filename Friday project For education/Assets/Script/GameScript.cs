using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public List<CardPlayScript> start_card;
    public List<CardPlayScript> age_card_b;
    public List<CardPlayScript> age_card_w;
    public List<CardPlayScript> fight_card;

    public List<CardPlayScript> my_crad;
    public List<CardPlayScript> age_card_bs;
    public List<CardPlayScript> age_card_ws;
    public List<CardPlayScript> fight_card_s;
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
