using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    public float Speed = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.down * Speed, ForceMode.Acceleration);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.GetComponent<Rigidbody>();
            playerRigidbody.AddForce(Vector3.down * Speed, ForceMode.Acceleration);

        }
    }

}
