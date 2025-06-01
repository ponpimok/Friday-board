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
        textPoint.text = "Point Have : " + pointHave.ToString();
    }
}
