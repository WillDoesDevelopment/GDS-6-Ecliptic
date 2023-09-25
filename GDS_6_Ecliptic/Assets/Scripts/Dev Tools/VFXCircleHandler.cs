using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCircleHandler : MonoBehaviour
{
    [Header("VFX References")] //References for the children in the VFX Circle GameObj
    public ParticleSystem[] trails;
    public ParticleSystem[] rings;
    public Material[] scrollMats;
    public Material[] emissMat;

    [Header("Colours for Trails")] //The main larger trails Colours
    public Color[] TstartCol;
    public Gradient[] TtrailGrad;

    [Header("Colours for Rings")] //The smaller emissive trails Colours
    public Color[] RstartCol;
    public Gradient[] RtrailGrad;
    public Color[] RemissCol;

    [Header("Colours for Fire Scroll")] //Sets the 3D fire/scroll Colours
    public Color[] ScrollCol;

    [Header("Game Objects")]
    public GameObject[] objs;

    private void Awake()
    {
        //References for particle systems
        trails[0].GetComponents<ParticleSystem>();
        trails[1].GetComponents<ParticleSystem>();
        rings[0].GetComponents<ParticleSystem>();
        rings[1].GetComponents<ParticleSystem>();

        //References for scroll shader
        scrollMats[0].GetColor("_Circle_Color");
        scrollMats[0].GetColor("_Emission");
        scrollMats[1].GetColor("_Circle_Color");
        scrollMats[1].GetColor("_Emission");

        //emission mat for rings
        emissMat[0].GetColor("_EmissionColor");

        //Clockwise PS
        //references for actual sections of particle system
        var main1 = trails[0].main;
        var trails1 = trails[0].trails;
        var emit1 = trails[0].emission;

        //Editing the colours in the sections
        TstartCol[0].a = 0;
        main1.startColor = TstartCol[0];
        trails1.colorOverLifetime = TtrailGrad[1];
        emit1.enabled = true;

        //Anti Clockwise PS
        //references for actual sections of particle system
        var main2 = trails[1].main;
        var trails2 = trails[1].trails;
        var emit2 = trails[1].emission;
        emit2.enabled = true;

        //Editing the colours in the sections
        TstartCol[1].a = 0;
        main2.startColor = TstartCol[1];
        trails2.colorOverLifetime = TtrailGrad[1];


        //Ring Inner
        //references for actual sections of particle system
        var RMain1 = rings[0].main;
        var RTrails1 = rings[0].trails;
        var Remit1 = rings[0].emission;

        //ediing the colours in the sections
        RstartCol[0].a = 0;
        RMain1.startColor = RstartCol[0];
        RTrails1.colorOverLifetime = RtrailGrad[0];

        //editing the colours in the emissive material in renderer
        emissMat[0].color = RemissCol[1];
        emissMat[0].SetColor("_Base_Color", RemissCol[0]);
        emissMat[0].SetColor("_Emission", RemissCol[1]);


        //Rings Outer
        //references for actual sections of particle system
        var RMain2 = rings[1].main;
        var RTrails2 = rings[1].trails;
        var Remit2 = rings[1].emission;

        //ediing the colours in the sections
        RstartCol[1].a = 0;
        RMain2.startColor = RstartCol[1];
        RTrails2.colorOverLifetime = RtrailGrad[1];

        //editing the colours in the emissive material in renderer
        emissMat[0].SetColor("_Base_Color", RemissCol[0]);
        emissMat[0].SetColor("_Emission", RemissCol[1]);


        //Scroll Clockwise
        scrollMats[0].SetColor("_Circle_Color", ScrollCol[0]);
        scrollMats[0].SetColor("_Emission", ScrollCol[1]);

        //Scroll Anti Clockwise
        scrollMats[1].SetColor("_Circle_Color", ScrollCol[1]);
        scrollMats[1].SetColor("_Emission", ScrollCol[0]);

    }

    private void Start()
    {
        
    }


    public void circleVFXStart()
    {
        //SCROLL SHADER OBJS
        objs[0].SetActive(true);
        objs[1].SetActive(true);
        objs[2].SetActive(true);
        //objs[3].SetActive(true);

        //TRAIL ALPHA
        var main = trails[0].main;
        var main1 = trails[1].main;

        TstartCol[0].a = 1;
        TstartCol[1].a = 1;

        main.startColor = TstartCol[0];
        main1.startColor = TstartCol[1];

        //RING ALPHA 
        var RMain = rings[0].main;
        var RMain1 = rings[1].main;

        RstartCol[0].a = 1;
        RstartCol[1].a = 1;

        RMain.startColor = RstartCol[0];
        RMain1.startColor = RstartCol[1];

    }


}
