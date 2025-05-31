using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAgeScript : MonoBehaviour
{
    [SerializeField] private DataCardScript getInfo;
    [SerializeField] private TextMeshProUGUI show_num_age_black;
    [SerializeField] private TextMeshProUGUI show_num_age_white;
    [SerializeField] private GameObject age_black;
    private void Start()
    {
        age_black.SetActive(true);
        show_num_age_black.text = getInfo.age_card_bs.Count.ToString();
        show_num_age_white.text = getInfo.age_card_ws.Count.ToString();
    }

    void Update()
    {
        if (getInfo.age_card_bs.Count == 0)
        {
            age_black.SetActive(false);
        }
    }
}
