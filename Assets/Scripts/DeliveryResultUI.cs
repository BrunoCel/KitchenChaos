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

    

    private void Start()
    {
        DeliveyManager.instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveyManager.instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        iconImage.sprite = failImage;
        backgroundImage.color = failColor;
        message.text = "Delivery\nFailed";
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        iconImage.sprite = sucessImage;
        backgroundImage.color = sucsessColor;
        message.text = "Delivery\nSuccess";
    }
}
