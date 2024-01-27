using UnityEngine;
using UnityEngine.SceneManagement;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color pickupColor = Color.green;
    private Color deliveryColor;
    private SpriteRenderer carRenderer;
    private bool hasPackage = false;

    void Start()
    {
        carRenderer = GetComponent<SpriteRenderer>();
        deliveryColor = carRenderer.color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision();
        Debug.Log("Boom!!!");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            PickUpPackage(collision.gameObject);
        }

        if (collision.CompareTag("Customer") && hasPackage)
        {
            DeliverPackage();
        }
    }

    void HandleCollision()
    {
        SceneManager.LoadScene(0);
    }

    void PickUpPackage(GameObject package)
    {
        hasPackage = true;
        Debug.Log("Package picked up");

        Destroy(package);
        SetCarColor(pickupColor);
    }

    void DeliverPackage()
    {
        hasPackage = false;
        Debug.Log("Package is delivered");

        ResetCarColor();
    }

    void SetCarColor(Color color)
    {
        carRenderer.color = color;
    }

    void ResetCarColor()
    {
        carRenderer.color = deliveryColor;
    }
}
