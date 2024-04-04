using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
    public int points = 1;
    public float timeLimit = 60;
    public float timeRun = 0;
}
