using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPad : MonoBehaviour
{
    public GameObject sphere;
    public Color toColor;
    Color originalColor;

    private void Start()
    {
        originalColor = sphere.GetComponent<Renderer>().material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            sphere.GetComponent<Renderer>().material.color = toColor;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sphere.transform.localScale += Vector3.one * 0.01f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sphere.GetComponent<Renderer>().material.color = originalColor;
            sphere.transform.localScale = Vector3.one;
        }
    }
}
