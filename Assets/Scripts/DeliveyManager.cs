using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class DeliveyManager : MonoBehaviour
{
    public static DeliveyManager instance{get; private set;}
    [SerializeField] RecipeListSO recipeSOList;
    private List<RecipeSO> waitingRecipesSOList;

    private float spawRecipeTimer;
    private float spawnRecipeTimerMax = 10f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        instance = this;
        waitingRecipesSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawRecipeTimer -= Time.deltaTime;
        if (spawRecipeTimer <= 0f)
        {
            spawRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipesSOList.Count < waitingRecipesMax)
            {
                RecipeSO _waitingRecipeSO = recipeSOList.recipes[Random.Range(0, recipeSOList.recipes.Count)];
                Debug.Log(_waitingRecipeSO.name);
                waitingRecipesSOList.Add(_waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject _plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipesSOList.Count; i++)
        {
            RecipeSO waitingRecipesSo = waitingRecipesSOList[i];
            
            if (waitingRecipesSo.KitchenObjectsList.Count == _plateKitchenObject.GetKitchenObjectsList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObject in _plateKitchenObject.GetKitchenObjectsList())
                {
                    bool ingridientFound = false;
                    foreach (KitchenObjectSO plateKitchenObject in _plateKitchenObject.GetKitchenObjectsList())
                    {
                        if (plateKitchenObject == recipeKitchenObject)
                        {
                            ingridientFound = true;
                            break;
                        }
                    }

                    if (!ingridientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    Debug.Log("Player Delivered the correct recipe");
                    waitingRecipesSOList.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
