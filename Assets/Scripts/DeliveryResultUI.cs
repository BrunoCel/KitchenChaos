using System;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image iconImage;
    [SerializeField] private UnityEngine.UI.Image backgroundImage;

    [SerializeField] private TextMeshProUGUI message;

    [SerializeField] private Sprite sucessImage;
    [SerializeField] private Sprite failImage;

    [SerializeField] private Color sucsessColor;
    [SerializeField] private Color failColor;

    private Animator animator;
    const string PopUpAnimationTrigger = "PopUp";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveyManager.instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveyManager.instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        iconImage.sprite = failImage;
        backgroundImage.color = failColor;
        message.text = "Delivery\nFailed";
        animator.SetTrigger(PopUpAnimationTrigger);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        iconImage.sprite = sucessImage;
        backgroundImage.color = sucsessColor;
        message.text = "Delivery\nSuccess";
        animator.SetTrigger(PopUpAnimationTrigger);
    }
}
