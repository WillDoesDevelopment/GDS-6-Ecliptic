using UnityEngine;
using System.Collections;

// Applies an explosion force to all nearby rigidbodies
public class ExplosionTest : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
        Physics.IgnoreLayerCollision(0, 10);

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius);
        }
    }
}
