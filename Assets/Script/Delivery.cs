using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delivery : MonoBehaviour
{
    [SerializeField] Sprite blurredCustomerSprite; // Assign this in the Inspector
    public TextMeshProUGUI scoreText; // Assign this in the inspector
    public TextMeshProUGUI packageText; // Assign this in the inspector
    private Color deliveryColor;
    private SpriteRenderer carRenderer;
    private bool hasPackage = false;

    // New code starts here
    private static int score = 0; // Tracks the score based on deliveries
    private static int packagesReceived = 0; // Tracks the number of packages picked up

    public static int Score => score;
    public static int PackagesReceived => packagesReceived;
    // New code ends here

    void Start()
    {
        carRenderer = GetComponent<SpriteRenderer>();
        if (carRenderer != null)
        {
            deliveryColor = carRenderer.color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found. Ensure this script is attached to the correct GameObject.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is tagged as "Road"
        if (collision.gameObject.CompareTag("Road"))
        {
            return; // Ignore this collision and exit the method early
        }

        // Handle other collisions
        HandleCollision();
        Debug.Log("Boom!!!");
    }

    void ApplyBlurEffect(GameObject customer)
    {
        var spriteRenderer = customer.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = blurredCustomerSprite;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Package") && !hasPackage)
        {
            PickUpPackage(collision.gameObject);
            packagesReceived++;
            if (packageText == null)
            {
                Debug.LogError("packageText is not assigned in the Inspector!");
            }
            else
            {
                packageText.text = string.Format("Packages: {0}", packagesReceived);
            }

            
        }

        if (collision.CompareTag("Customer") && hasPackage)
        {
            DeliverPackage();
            score++; // Increment the score for successful delivery
            collision.GetComponent<Collider2D>().enabled = false; // Disable the customer's collider
            ApplyBlurEffect(collision.gameObject); // Call the method to apply the blur effect
            packagesReceived--;
            packageText.text = string.Format("Packages: {0}", packagesReceived);
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
    }

    void DeliverPackage()
    {
        hasPackage = false;
        GameEventsManager.instance.ScoreCollected();
        Debug.Log("Package is delivered");
    }

}
