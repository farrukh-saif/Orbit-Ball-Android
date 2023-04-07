using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class find_important_object : MonoBehaviour
{
    public void on_click_find(int level_no_para)
    {
        FindObjectOfType<persistent_data_script>().Set_level_no(level_no_para);
    }

    public void on_click_play(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void on_click_stop(string name)
    {
        FindObjectOfType<AudioManager>().Stop(name);
    }
}
