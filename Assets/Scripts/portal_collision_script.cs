using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_collision_script : MonoBehaviour
{
    GameObject game_won_overlay;
    private void Start()
    {
        game_won_overlay = GameObject.Find("Game Won Overlay");
        game_won_overlay.SetActive(false);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*FindObjectOfType<persistent_data_script>().level_no++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
        game_won_overlay.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Portal");

    }
}
