using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDoubleUIScript : MonoBehaviour
{
    public Transform showCrad;
    public int use;//0 = Double, 1 = Exchange, 2 = copy
    public int numExCard;
    [SerializeField] private TextMeshProUGUI text;
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
            case 2:
                text.text = "Copy Effect 1 Crad";
                break;
            default:
                break;
        }
    }
}
