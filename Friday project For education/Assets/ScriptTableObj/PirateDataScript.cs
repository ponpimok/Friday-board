using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/Create Pirate Card")]
public class PirateDataScript : ScriptableObject
{
    public int take_card;
    public int power_enemy;//พลังของอุปสรรค
    public bool have_effect;
    public string effect_text;
    public Sprite image;
}
