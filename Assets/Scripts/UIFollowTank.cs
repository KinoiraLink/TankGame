using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTank : MonoBehaviour
{
   public Transform objectToFollow;
   private RectTransform rectTransform;

   private void Awake()
   {
      rectTransform = GetComponent<RectTransform>();
   }

   private void Update()
   {
      if (objectToFollow != null)
         rectTransform.anchoredPosition = objectToFollow.localPosition;
   }
}
