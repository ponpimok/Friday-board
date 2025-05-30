using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNumCardScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numCard;
    [SerializeField] private GameScript getInfo;
    private void Start()
    {
        numCard.text = getInfo.my_crad.Count.ToString();
    }

    void Update()
    {
        
    }
}
