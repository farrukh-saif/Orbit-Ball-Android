using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class persistent_data_script : MonoBehaviour
{
    public int level_no;
    public string last_scene;
    public bool test_ad;
    void Awake()
    {
        level_no = 0;//Set initaial value of level_no to whatever the highest level unlocked inis in the appdata   
        //Advertisement.Initialize("3846997", test_ad);
        Application.targetFrameRate = 60;

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                FindObjectOfType<AudioManager>().Play("Menu Theme");
                FindObjectOfType<AudioManager>().Stop("Game Theme");
            }
        }
    }
    public void Set_level_no(int level_no_para)
    {
        level_no = level_no_para;
    }

    public void show_video_ad()
    {
        print("printing");
        //if (Advertisement.IsReady("video"))
        //{
        //    Advertisement.Show("video");
        //}
    }
}
