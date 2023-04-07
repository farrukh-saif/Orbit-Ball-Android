using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency_script : MonoBehaviour
{
    public static Text currency_text;
    public static float currency;
    // Start is called before the first frame update
    void Start()
    {
        currency_text = GetComponent<Text>();
        currency_text.text = PlayerPrefs.GetFloat("currency", 0).ToString("0");
        currency = PlayerPrefs.GetFloat("currency", 0);
    }

}
