using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0.0f, vert);

        rb.AddForce(move * speed);
    }
}