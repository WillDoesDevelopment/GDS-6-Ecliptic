
using UnityEngine;

public class tester : MonoBehaviour
{
    public GameObject coob;

    public void Tester()
    {
        var pos = new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
        var rot = new Quaternion(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f), 0);
        Instantiate(coob, pos, rot);
    }

}
