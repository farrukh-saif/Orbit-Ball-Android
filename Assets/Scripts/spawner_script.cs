using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner_script : MonoBehaviour
{
    public GameObject portal;
    public GameObject[] pivots;
    public int number_of_pivots;
    private int randy;
    private int randx;
    void Start()
    {
        number_of_pivots = 2 * FindObjectOfType<persistent_data_script>().level_no;
        randy = 10;
        randx = 0;
        for (int i = 0; i < number_of_pivots; i++)
        {
            Instantiate(pivots[Random.Range(0,pivots.Length)], new Vector3(randx, i * 12 + randy, 10), new Quaternion(0, 0, 0, 1));
            randx += Random.Range(-7, +7);
            randy += Random.Range(-3, 3);
            //pivot.transform.position = new Vector3(0,i*25,10);
        }
        Instantiate(portal, new Vector3(randx, 12 * number_of_pivots + randy, 10), new Quaternion(0, 0, 0, 1));
    }

}
