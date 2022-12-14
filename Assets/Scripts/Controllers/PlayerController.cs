using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardPlayerSpeed = 5f;
    [SerializeField] private float touchSpeed = 5f;
    [SerializeField] private Vector3 clampsLimit;

    private Vector3 initialPlayerPosition;
    private Vector3 initialCameraPosition;

    private void Start()
    {
        initialCameraPosition = GameObject.FindWithTag("MainCamera").transform.position;
        initialPlayerPosition = this.transform.position;
    }

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
                        transform.position.z);
                }
            }
        }
       
    }
    
     private void OnTriggerStay(Collider other)
     {
         if (other.CompareTag("Collectible"))
         {
             if(GameManager.Instance.gameStopped)
             {
                 other.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
             }
             else
             {
                 other.GetComponent<Rigidbody>().AddForce(transform.position - other.GetComponent<Rigidbody>().position * 100f * Time.fixedDeltaTime);
             }
             
         }
     }

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("PlayerStopPoint"))
         {
             GameManager.Instance.gameStopped = true;
             other.gameObject.SetActive(false);
             StartCoroutine(CheckGameFail());
         }

         if (other.CompareTag("Finish"))
         {
             other.gameObject.SetActive(false);
             GameManager.Instance.gameFinished = true;
             GameManager.Instance.gameStopped = true;
             UIManager.Instance.ActivateSuccessPanel();
             if (initialCameraPosition != null)
             {
                 GameObject.FindWithTag("MainCamera").transform.position = initialCameraPosition;
             }
             gameObject.transform.position = initialPlayerPosition;
         }
     }

     private IEnumerator CheckGameFail()
     {
         var checkWaitTime = GameManager.Instance.pitWaitTime + 2f;
         yield return new WaitForSeconds(checkWaitTime);
         if (GameManager.Instance.gameStopped && !GameManager.Instance.gameFinished)
         {
             GameManager.Instance.gameFinished = true;
             UIManager.Instance.ActivateFailPanel();
             if (initialCameraPosition != null)
             {
                 GameObject.FindWithTag("MainCamera").transform.position = initialCameraPosition;
             }
             gameObject.transform.position = initialPlayerPosition;
         }
       
     }

    
}
