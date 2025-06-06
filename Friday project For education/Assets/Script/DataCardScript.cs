﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataCardScript : MonoBehaviour
{
    [SerializeField] private List<CardPlayScript> start_card;
    [SerializeField] private List<CardPlayScript> age_card_b;
    [SerializeField] private List<CardPlayScript> age_card_w;
    [SerializeField] private List<CardPlayScript> fight_card;
    [SerializeField] private List<PirateDataScript> pirate_crad;

    public List<CardPlayScript> my_crad;
    public List<CardPlayScript> my_crad_used;
    public List<CardPlayScript> age_card_bs;
    public List<CardPlayScript> age_card_ws;
    public List<CardPlayScript> fight_card_s;
    public List<CardPlayScript> not_fight_now;
    public List<PirateDataScript> pirate_card_s;//มีแค่ 2 ใบ
    public void ShuffleCards(List<CardPlayScript> deck, List<CardPlayScript> addto)
    {
        int num = deck.Count;
        for (int i = 0; i < num; i++)
        {
            int num_new = deck.Count;
            int r = Random.Range(0, num_new);
            deck[r].powerDouble = false;
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
        pirate_card_s.Clear();

        ShuffleCards(start_card, my_crad);
        ShuffleCards(age_card_b, age_card_bs);
        ShuffleCards(age_card_w, age_card_ws);
        ShuffleCards(fight_card, fight_card_s);
        int num = pirate_crad.Count;
        for (int i = 0; i < num; i++)
        {
            int r = Random.Range(0, pirate_crad.Count);
            pirate_card_s.Add(pirate_crad[r]);
            pirate_crad.RemoveAt(r);
        }


        foreach (var item in fight_card_s)
        {
            item.no_fight_card = false;
            item.no_fight_card_in_game = false;
        }
        foreach (var item in age_card_bs)
        {
            item.no_fight_card = true;
            item.no_fight_card_in_game = true;
        }
        foreach (var item in age_card_ws)
        {
            item.no_fight_card = true;
            item.no_fight_card_in_game = true;
        }
        foreach (var item in my_crad)
        {
            item.no_fight_card_in_game = true;
        }
    }
}
