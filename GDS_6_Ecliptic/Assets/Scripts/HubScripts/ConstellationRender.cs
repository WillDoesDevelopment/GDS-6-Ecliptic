using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationRender : MonoBehaviour
{
   public LineRenderer line;
    public float animSpeed = 5f;
    public Vector3[] linePoints;
    public int pointsCount;

    // Start is called before the first frame update
    void Start()
    {
        /*
        line = GetComponent<LineRenderer>();
        pointsCount = line.positionCount;
        linePoints = new Vector3[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            linePoints[i] = line.GetPosition(i);
        }

        StartCoroutine(AnimateConst());
        */
    }

    void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        pointsCount = line.positionCount;
        linePoints = new Vector3[pointsCount];
        for (int i = 0; i < pointsCount; i++)
        {
            linePoints[i] = line.GetPosition(i);
        }

        StartCoroutine(AnimateConst());
    }

    public IEnumerator AnimateConst()
    {
        float segDuration = animSpeed / pointsCount;
        for(int i = 0; i < pointsCount - 1; i++)
        {
            float startTime = Time.time;
            Vector3 startPos = linePoints[i];
            Vector3 endPos = linePoints[i +1];

            Vector3 pos = startPos;
            while (pos != endPos)
            {
                float t = (Time.time - startTime) / segDuration;
                pos = Vector3.Lerp(startPos, endPos, t);
                for (int j = i + 1; j < pointsCount; j++)
                {
                    line.SetPosition(j, pos);
                }
                yield return null;
            }
        }
    }
}
