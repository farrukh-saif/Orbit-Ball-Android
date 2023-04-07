using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_script : MonoBehaviour
{
    // Start is called before the first frame update
    public Text level_txt;
    void Start()
    {
        level_txt.text = "Level " + FindObjectOfType<persistent_data_script>().level_no.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
