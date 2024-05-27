using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstTrig : MonoBehaviour
{
    //very simple script so that it can be referenced by the orrery/hub scripts

    public GameObject[] constellations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Aries()
    {
        constellations[0].SetActive(true);
    }

    public void Taurus()
    {
        constellations[1].SetActive(true);

    }

    public void Cancer()
    {
        constellations[2].SetActive(true);

    }

    public void Scorpio()
    {
        constellations[3].SetActive(true);

    }

    public void Virgo()
    {
        constellations[4].SetActive(true);

    }
}
