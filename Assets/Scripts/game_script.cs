using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Menu Theme");
        FindObjectOfType<AudioManager>().Play("Game Theme");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
