using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="FruitCollection", menuName ="Gameplay Data/FruitCollection")]
public class FruitCollectionSO : DescriptionBaseSO
{
    [SerializeField] private List<FruitSO> listFruits = new List<FruitSO>();
    [SerializeField] private int canSpawnIndex = 4;

    public FruitSO GetRandomFruitToSpawn()
    {
        int randomIndex = Random.Range(0, canSpawnIndex);

        return listFruits[randomIndex];
    }
}