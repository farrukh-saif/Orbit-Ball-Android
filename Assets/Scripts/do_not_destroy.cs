using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class do_not_destroy : MonoBehaviour
{
     public static do_not_destroy instance;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
