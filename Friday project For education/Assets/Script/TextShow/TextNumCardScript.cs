using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNumCardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numCard;
    [SerializeField] private TextMeshProUGUI numCardUsed;
    [SerializeField] private DataCardScript getInfo;
    private void Update()
    {
        numCard.text = getInfo.my_crad.Count.ToString();
        numCardUsed.text = getInfo.my_crad_used.Count.ToString();
    }
}
