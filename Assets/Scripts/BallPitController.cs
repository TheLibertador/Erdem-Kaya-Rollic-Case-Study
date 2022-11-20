using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPitController : MonoBehaviour
{
   private int collectedBallCount = 0;

   private void OnTriggerEnter(Collider other)
   {
      collectedBallCount++;
      Debug.Log(collectedBallCount);
   }
}
