using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet_rotator_script : MonoBehaviour
{
    // Update is called once per frame
    public float rotation_rate_up;
    public float rotation_rate_forward;

    private void Start()
    {
        transform.eulerAngles = (new Vector3(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)));
    }
    void Update()
    {
        transform.Rotate(Vector3.up,rotation_rate_up * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotation_rate_forward * Time.deltaTime);
    }
}
