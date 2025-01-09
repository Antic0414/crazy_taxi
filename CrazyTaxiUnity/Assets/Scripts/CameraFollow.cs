using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject taxiGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(taxiGO.transform.position);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(taxiGO.transform.position.x, taxiGO.transform.position.y,
                                         transform.position.z);
    }
}
