 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    // 4s co 1 recipe
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount;
    private int playerScore;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        successfulRecipesAmount = 0;
        playerScore = 0;
    }
    private void Update()
    {

        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = Instantiate(recipeListSO.GetRandomRecipeSO());
                // thời gian hoàn thành món 
                float timeLimit = waitingRecipeSO.timeLimit;
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                // đếm ngược, hết thời gian thì xóa ra khỏi list và trừ điểm
                StartCoroutine(DestroyRecipe(waitingRecipeSO, timeLimit));

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

            }

        }
    }
    private IEnumerator DestroyRecipe(RecipeSO waitingRecipeSO, float timeLimit)
    {
        // time limit đếm ngược xong thì xóa ra khỏi lit và trừ điểm
        yield return new WaitForSeconds(timeLimit);
        if (waitingRecipeSOList.Contains(waitingRecipeSO))
        {
            waitingRecipeSOList.Remove(waitingRecipeSO);
            if (playerScore < 0)
            {
                playerScore = 0;
            }

            // OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    waitingRecipeSOList.RemoveAt(i);
                    successfulRecipesAmount++;
                    playerScore += waitingRecipeSO.points;
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }

        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);

    }


    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
    public int GetPlayerScore()
    {
        return playerScore;
    }
}
