using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public static ShowText Instacne;

    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Instacne == null)
            Instacne = this;
    }

    private void Update()
    {
        if(text.alpha > 0.9f)
            text.alpha = Mathf.Lerp(text.alpha, 0, 0.0003f);
        else
            text.alpha = Mathf.Lerp(text.alpha, 0, 0.007f);
    }

    public static void ViewText(string s,Color color)
    {
        Instacne.text.text = s;
        Instacne.text.color = new Color(color.r, color.g, color.b, 1);
    }

    public static void ViewText(string s)
    {
        ViewText(s, Color.red);
    }
}
