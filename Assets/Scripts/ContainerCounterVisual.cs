using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
   private const string OPEN_CLOSE = "OpenClose";
   [SerializeField] private ContainerCounter countainerCounter;
   private Animator animator;
   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   void Start()
   {
      countainerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
   }

   private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e)
   {
      animator.SetTrigger(OPEN_CLOSE);
   }
   
}
