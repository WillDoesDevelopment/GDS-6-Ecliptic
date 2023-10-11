using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationTrigger : MonoBehaviour
{
    public ConstellationRender[] constRend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Aries()
    {
        constRend[0].Const();
    }

    public void Taurus()
    {
        constRend[1].Const();
    }

    public void Cancer()
    {
        constRend[2].Const();
    }

    public void Scorpio()
    {
        constRend[3].Const();
    }

    public void Virgo()
    {
        constRend[4].Const();
    }
}
