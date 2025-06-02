using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDestroyCradScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPoint;
    [HideInInspector] public int pointHave;
    public Transform showCrad;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (pointHave >=0)
        {
            textPoint.text = "Point Have : " + pointHave.ToString();
        }
        else
        {
            textPoint.text = "Destory One Crad";
        }
    }
}
