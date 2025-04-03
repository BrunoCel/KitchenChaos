using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    void Start()
    {
        
        DeliveyManager.instance.OnRecipeCompleted += UpdateVisual_Completed;
        DeliveyManager.instance.OnRecipeSpawned += UpdateVisual_Spawned;
        
        UpdateVisual();
        
    }

    private void UpdateVisual_Spawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual_Completed(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if(child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSo in DeliveyManager.instance.GetWaitingRecipesSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            
        }
    }
}
