using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class BallPitController : MonoBehaviour
{
   [SerializeField] private BallPitScriptableObject PitThreshold;
   [SerializeField] private TextMeshProUGUI counterText;
   
   private int collectedBallCount = 0;
   private Transform bridge;
   private float bridgeAnimTime = 1;
   private List<GameObject> ballsInPit = new List<GameObject>();

   private void Awake()
   {
      bridge = gameObject.transform.Find("Bridge");
      counterText.text = String.Format("{0}/{1}", collectedBallCount,PitThreshold.ballNumForPass);
   }

   private void OnTriggerEnter(Collider other)
   {
      collectedBallCount++;
      counterText.text = String.Format("{0}/{1}", collectedBallCount,PitThreshold.ballNumForPass);
      ballsInPit.Add(other.gameObject);
      StartCoroutine(Wait());
      if (collectedBallCount >= PitThreshold.ballNumForPass)
      {
         foreach (var ball in ballsInPit)
         {
            Destroy(ball.gameObject);
         }
        
         bridge.DOMoveY(0, bridgeAnimTime);
         StartCoroutine(SetGameStoppedToFalse());
      }
      
   }

   private IEnumerator SetGameStoppedToFalse()
   {
      yield return new WaitForSeconds(bridgeAnimTime);
      GameManager.Instance.gameStopped = false;
   }

   private IEnumerator Wait()
   {
      yield return new WaitForSeconds(GameManager.Instance.pitWaitTime);
   }
   
   
   
}
