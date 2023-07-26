using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCircleHandler : MonoBehaviour
{
    [Header("VFX References")]
    public ParticleSystem.MainModule[] trailsMain;
    public ParticleSystem.TrailModule[] trailsTrail;

    public ParticleSystem.MainModule[] ringsMain;
    public ParticleSystem.TrailModule[] ringsTrail;

    public Material[] scrollMats;
    public Material[] emissMat;

    [Header("Colours for Trails")]
    public Color[] TstartCol;
    public Gradient[] TtrailGrad;

    [Header("Colours for Rings")]
    public Color[] RstartCol;
    public Gradient[] RtrailGrad;
    public Color[] RemissCol;

    [Header("Colours for Fire Scroll")]
    public Color[] ScrollCol;

    private void Awake()
    {
        //References
        trailsMain[0] = new ParticleSystem.MainModule();
        trailsMain[1] = new ParticleSystem.MainModule();
        trailsTrail[0] = new ParticleSystem.TrailModule();
        trailsTrail[1] = new ParticleSystem.TrailModule();

        ringsMain[0] = new ParticleSystem.MainModule();
        ringsMain[1] = new ParticleSystem.MainModule();
        ringsTrail[0] = new ParticleSystem.TrailModule();
        ringsTrail[1] = new ParticleSystem.TrailModule();

        scrollMats[0].GetColor("_Circle_Color");
        scrollMats[0].GetColor("_Emission");
        scrollMats[1].GetColor("_Circle_Color");
        scrollMats[1].GetColor("_Emission");

        emissMat[0].GetColor("_Base_Color");
        emissMat[0].GetColor("_Emission");
        emissMat[1].GetColor("_Base_Color");
        emissMat[1].GetColor("_Emission");
    }

    private void Start()
    {
        //Clockwise PS
        trailsMain[0].startColor = TstartCol[0];
        trailsTrail[0].colorOverLifetime = TtrailGrad[0];

        //Anti Clockwise PS
        trailsMain[1].startColor = TstartCol[1];
        trailsTrail[1].colorOverLifetime = TtrailGrad[1];

        //Ring Inner
        ringsMain[0].startColor = RstartCol[0];
        ringsTrail[0].colorOverLifetime = RtrailGrad[0];
        emissMat[0].SetColor("_Base_Color", RemissCol[0]);
        emissMat[0].SetColor("_Emission", RemissCol[1]);

        //Rings Outer
        ringsMain[0].startColor = RstartCol[1];
        ringsTrail[0].colorOverLifetime = RtrailGrad[1];
        emissMat[0].SetColor("_Base_Color", RemissCol[2]);
        emissMat[0].SetColor("_Emission", RemissCol[3]);

        //Scroll Clockwise
        scrollMats[0].SetColor("_Circle_Color", ScrollCol[0]);
        scrollMats[0].SetColor("_Emission", ScrollCol[1]);

        //Scroll Anti Clockwise
        scrollMats[1].SetColor("_Circle_Color", ScrollCol[2]);
        scrollMats[1].SetColor("_Emission", ScrollCol[3]);


    }

}
