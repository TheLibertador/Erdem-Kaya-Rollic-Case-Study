using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BallPitController : MonoBehaviour
{
   [SerializeField] private BallPitScriptableObject PitThreshold;
   private int collectedBallCount = 0;
   private Transform bridge;
   private float bridgeAnimTime = 1;

   private void Awake()
   {
      bridge = gameObject.transform.Find("Bridge");
   }

   private void OnTriggerEnter(Collider other)
   {
      collectedBallCount++;
      StartCoroutine(Wait());
      if (collectedBallCount >= PitThreshold.ballNumForPass)
      {
         bridge.DOMoveY(0, bridgeAnimTime);
         StartCoroutine(SetGameStoppedToFalse());
      }
      
   }

   private IEnumerator SetGameStoppedToFalse()
   {
      Debug.Log("GAME STOP BEFORE COROUTİNE ===== " + GameManager.Instance.gameStopped);
      yield return new WaitForSeconds(bridgeAnimTime);
      GameManager.Instance.gameStopped = false;
      Debug.Log("GAME STOP AFTER COROUTİNE ===== " + GameManager.Instance.gameStopped);
   }

   private IEnumerator Wait()
   {
      yield return new WaitForSeconds(GameManager.Instance.pitWaitTime);
   }
   
   
   
}
