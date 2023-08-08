using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrreryRotation : MonoBehaviour
{

    public GameObject[] OrreryArms;
    public float[] RandomRotations;
    // Start is called before the first frame update
    void Start()
    {
        RandomRotations = new float[OrreryArms.Length];
        for (int i = 0; i< OrreryArms.Length; i++)
        {
            RandomRotations[i] = Random.Range(-.1f, .1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < OrreryArms.Length; i++)
        {
            OrreryArms[i].transform.eulerAngles += new Vector3(0, RandomRotations[i], 0);
        }
    }
}
