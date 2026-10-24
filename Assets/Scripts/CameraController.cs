using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject aircraft;
    private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - aircraft.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = aircraft.transform.position + offset;
    }
}
