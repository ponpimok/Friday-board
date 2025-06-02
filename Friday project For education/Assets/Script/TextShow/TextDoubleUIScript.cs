using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDoubleUIScript : MonoBehaviour
{
    public Transform showCrad;
    public int use;//0 = Double, 1 = Exchange,
    public int numExCard;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {

    }
    private void Update()
    {
        switch (use)
        {
            case 0:
                text.text = "Double Power";
                break;
            case 1:
                text.text = "Exchange " + numExCard + " Card";
                break;
            default:
                break;
        }
    }
}
