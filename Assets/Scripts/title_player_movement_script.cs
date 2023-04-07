using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class title_player_movement_script : MonoBehaviour
{
    GameObject title_pivot;
    Vector3 title_pivot_position;
    public float radius;
    bool gyro_enabled = true;

    void Start()
    {
        title_pivot = GameObject.Find("Title Pivot");
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            if (Input.gyro.enabled)
            {
                gyro_enabled = true;
            }
        }
        else
        {
            //Case when gyro is not supported
        }
        //gyro_enabled = false; (for farrukh's gyroless phones)
        Debug.Log(gyro_enabled);
    }

    void Update()
    {
        title_pivot_position = title_pivot.transform.position;
        if (gyro_enabled)
        {
            SetAngle(GetAngle());
        }
        else
        {
            SetAngle(Time.time * 2);
        }


    }

    void SetAngle(float angle)
    {

        transform.position = new Vector3(title_pivot_position.x + radius * Mathf.Sin(angle), title_pivot_position.y + radius * -Mathf.Cos(angle), title_pivot_position.z);
    }

    float GetAngle()
    {
        Vector3 gravity = Input.gyro.gravity.normalized;
        //Debug.Log(gravity);
        float angle = Mathf.Asin(Vector3.Dot(gravity, Vector3.right)) * Mathf.Sign(gravity.magnitude);
        return angle;


    }
}

 