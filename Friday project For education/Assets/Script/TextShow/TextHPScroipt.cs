using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextHPScroipt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hp_text;
    public int hp;
    public int max;
    void Update()
    {
        if (hp > max)
        {
            hp = max;
        }
        hp_text.text = hp.ToString() + "/" + max.ToString();
    }
}
