﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card",menuName = "Card/Create New Card")]
public class CardPlayScript : ScriptableObject
{
    public bool no_fight_card; //ใช้การ์ดเริ่มต้นหรือเปล่า //ไม่แก้ค่า
    [HideInInspector] public bool no_fight_card_in_game; //ใช้การ์ดเริ่มต้นหรือเปล่า
    public bool must_use_card; //ใช้การ์ดเริ่มต้นหรือเปล่า
    public bool card_effect; //การ์ดมีความสามารถไหม
    public string card_effect_name; //ชื่อความสามารถไหม
    public int effect_value;
    public int power_card; //ค่าพลังเท่าไร
    public bool powerDouble;
    public string name_use_card; //ชื่ออะไรตอนใช้งานฝ่ายเรา
    public int use_hp_to_destroy; //จำนวนพลังชีวิตที่ใช้ในการทำลาย

    public string name_fight_card; //ชื่ออุปสรรค
    public int take_card; //จำนวนการ์ดที่สามารถใช้สูดได้
    public int[] power_enemy;//พลังของอุปสรรค

    public bool ageCrad;

    public Sprite artCard;
}
