using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

   
    


    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    // 4s co 1 recipe
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int successfulRecipesAmount;
    private int playerScore;
    private int scoreCompletedLevel;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
        successfulRecipesAmount = 0;
        playerScore = 0;

       if (SceneManager.GetActiveScene().name == KitchenGameManager.LEVEL1)
        {
            scoreCompletedLevel = GameLevelManager.scoreCompletedLevel1;
        }
        else if (SceneManager.GetActiveScene().name == KitchenGameManager.LEVEL2)
        {
            scoreCompletedLevel = GameLevelManager.scoreCompletedLevel2;
        }
        else if (SceneManager.GetActiveScene().name == KitchenGameManager.LEVEL3)
        {
            scoreCompletedLevel = GameLevelManager.scoreCompletedLevel3;
        }
        else
        {
            scoreCompletedLevel = 999;
        }

    }
    private void Start() {
         
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

                // đếm ngược, hết thời gian thì xóa ra khỏi list 
                StartCoroutine(DestroyRecipe(waitingRecipeSO, timeLimit));

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);

            }

        }

        if (playerScore >= scoreCompletedLevel)
        {
            // change stated action overgame
            KitchenGameManager.Instance.GameOver();
        }
    }
    private IEnumerator DestroyRecipe(RecipeSO waitingRecipeSO, float timeLimit)
    {
        // time limit đếm ngược xong thì xóa ra khỏi list
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

    internal object GetScoreCompletedLevel()
    {
        return scoreCompletedLevel;
    }
}
