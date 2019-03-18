using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody BulletRigidbody;

    void Start()
    {
        transform.parent = null;
        transform.localScale = new Vector3(0.1f, 0.2f, 0.1f);
        BulletRigidbody.AddRelativeForce(Vector3.up * 20, ForceMode.Impulse);
        Destroy(gameObject, 5.0f);
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
