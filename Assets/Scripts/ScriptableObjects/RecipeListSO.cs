using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [CreateAssetMenu()]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;

    public RecipeSO GetRandomRecipeSO() {
        return recipeSOList[Random.Range(0, recipeSOList.Count)];
    }
}
