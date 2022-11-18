using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardPlayerSpeed = 5f;
    [SerializeField] private float touchSpeed = 5f;
    [SerializeField] private Vector3 clampsLimit;


    private void FixedUpdate()
    {
        if (GameManager.Instance.gameStopped != true)
        {
            transform.Translate(Vector3.up * forwardPlayerSpeed * Time.deltaTime);
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x + touch.deltaPosition.x * touchSpeed * Time.fixedDeltaTime, -clampsLimit.x, clampsLimit.x),
                        transform.position.y,
                        transform.position.z +touch.deltaPosition.y * touchSpeed * Time.fixedDeltaTime);
                }
            }
        }
       
    }
    
     private void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("Collectible"))
         {
             other.GetComponent<Rigidbody>().AddForce(transform.position - other.GetComponent<Rigidbody>().position * 60f * Time.fixedDeltaTime);

         }
         
     }
}
