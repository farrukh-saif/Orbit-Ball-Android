using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class score_script : MonoBehaviour
{
    public Text score_text;
    public static int attached;
    public float attached_penalty;
    float score;
    public float[] initial_score;
    public float rate_of_decrease_in_score;
    int index;
    bool first;
    private void Start()
    {
        index = FindObjectOfType<persistent_data_script>().level_no - 1;
        if (initial_score.Length < index + 1)
        {
            Debug.LogWarning("NO INITIAL SCORE AVAILABLE");
            score = 100+attached_penalty;
        }
        else
        {
            score = initial_score[index] + attached_penalty;
        }
        attached = 0;
        first = true;
    }
    void Update()
    {
        if (!player_movement_script.won && score >= 0)
        {
            score -= Time.deltaTime * rate_of_decrease_in_score + attached * attached_penalty;
        }
        else if (first)
        {
            currency_script.currency = score;
            currency_script.currency_text.text = (currency_script.currency + PlayerPrefs.GetFloat("currency")).ToString("0");
            PlayerPrefs.SetFloat("currency", currency_script.currency + PlayerPrefs.GetFloat("currency"));
            first = false;
        }

        if (initial_score.Length >= index + 1)
        {
            score = Mathf.Clamp(score, 0, initial_score[index] + attached_penalty);
        }
        
        attached = 0;
        score_text.text = score.ToString("0");
    }
}
