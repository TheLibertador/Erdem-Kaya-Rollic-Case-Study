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
        transform.Translate(Vector3.up * forwardPlayerSpeed * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            var _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x + _touch.deltaPosition.x * touchSpeed * Time.deltaTime, -clampsLimit.x, clampsLimit.x),
                     transform.position.y,
                    transform.position.z + _touch.deltaPosition.y * touchSpeed * Time.deltaTime);
            }



        }
    }
}
