using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_script : MonoBehaviour
{
    public void change_scene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
    public void quit_game()
    {
        Application.Quit();
    }

    public void open_url(string url)
    {
        Application.OpenURL(url);
    }

    public void next_level()
    {
        FindObjectOfType<persistent_data_script>().level_no++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void reset_progress()
    {
        PlayerPrefs.SetFloat("currency",0);
    }
}
