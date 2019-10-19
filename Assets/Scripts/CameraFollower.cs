using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    

    [SerializeField]
    private float smoothDamped = 2f;
    private Vector3 originalPos;

    void Start() 
    {
        originalPos = transform.position;
    }
    
    void Update()
    {
        Vector3 offsetPos = originalPos + target.transform.position;
        transform.position = Vector3.Lerp(Camera.main.transform.position, 
                                                    offsetPos, smoothDamped * Time.deltaTime);
    }
}
