using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3.8f;
    [SerializeField] float rotateSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotateInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
        float moveInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Rotate(0, 0, -rotateInput);

        transform.Translate(0, moveInput, 0);
    }
}
