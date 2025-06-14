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
    [SerializeField] private GameObject age_white;
    private void Start()
    {
        age_white.SetActive(false);
        age_black.SetActive(true);
    }

    private void Update()
    {
        show_num_age_black.text = getInfo.age_card_bs.Count.ToString();
        show_num_age_white.text = getInfo.age_card_ws.Count.ToString();
        if (getInfo.age_card_bs.Count == 0)
        {
            age_black.SetActive(false);
            age_white.SetActive(true);
        }
    }
}
