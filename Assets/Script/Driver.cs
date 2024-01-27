using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] float boostSpeed = 10f;
    [SerializeField] float boostDuration = 3f;  

    private bool isBoosted = false;
    private float boostTimer = 0f;  

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBoostStatus();

        float currentMoveSpeed = isBoosted ? CalculateBoostedSpeed() : moveSpeed;

        float rotateInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float moveInput = Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -rotateInput);
        transform.Translate(0, moveInput, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedUp"))
        {
            ActivateBoost();
            Debug.Log("Boost");
            Destroy(collision.gameObject);
        }
    }

    float CalculateBoostedSpeed()
    {
        float boostedSpeed = boostSpeed * (1f + (boostTimer / boostDuration));
        return Mathf.Min(boostedSpeed, boostSpeed * 2f);  
    }

    void ActivateBoost()
    {
        isBoosted = true;
        boostTimer = 0f;
    }

    void UpdateBoostStatus()
    {
        if (isBoosted)
        {
            boostTimer += Time.deltaTime;

            if (boostTimer > boostDuration)
            {
                isBoosted = false;
            }
        }
    }
}
