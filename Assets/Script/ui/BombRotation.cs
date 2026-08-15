using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
