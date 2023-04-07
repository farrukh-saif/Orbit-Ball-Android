using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_script : MonoBehaviour
{

    public Transform target;
    public float smooth_speed;
    public Vector3 offset;


    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desired_position = target.position + offset;
        Vector3 smoothed_position = Vector3.Lerp(transform.position, desired_position, smooth_speed*Time.deltaTime);
        transform.position = smoothed_position; 
    }
}
