using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
   [SerializeField] private Transform target;
   [SerializeField] private Vector3 cameraOffset;

   private void LateUpdate()
   {
      var cameraPosition = transform.position;
      transform.position = Vector3.Lerp(cameraPosition, new Vector3(cameraPosition.x, cameraPosition.y, target.position.z) + cameraOffset, Time.deltaTime);
   }
}
