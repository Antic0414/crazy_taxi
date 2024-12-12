using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.01f;
    [SerializeField] float rotateSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, moveSpeed, 0);
        transform.Rotate(0, 0, rotateSpeed);
    }
}
