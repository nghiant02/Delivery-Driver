using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage = false;
    [SerializeField] float destroyDelay;
    [SerializeField] Color pickupColor = Color.green;   
    [SerializeField] Color deliveryColor = Color.blue;

    private SpriteRenderer carRenderer;

    void Start()
    {
        carRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boom!!!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package" && hasPackage == false)
        {
            hasPackage = true;
            Debug.Log("Package pick up");

            Destroy(collision.gameObject, destroyDelay);

            carRenderer.color = pickupColor;
        }

        if (collision.tag == "Customer" && hasPackage == true)
        {
            hasPackage = false;
            Debug.Log("Package is delivered");

            carRenderer.color = deliveryColor;
        }
    }
}
