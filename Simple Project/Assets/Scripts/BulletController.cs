using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody BulletRigidbody;

    void Start()
    {
        transform.parent = null;
        BulletRigidbody.AddRelativeForce(Vector3.up * 20, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 20, ForceMode.Impulse);
        }

        Destroy(gameObject);
    }
}
