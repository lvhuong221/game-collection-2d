using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName ="STM_Level", menuName = "Scene Data/STM game")]
public class STMLevelSO : DescriptionBaseSO
{
    public List<STMLevelData> listLevel;

    public STMLevelData GetLevelData(int level)
    {
        foreach(var item in listLevel)
        {
            if(item.level == level) 
                return item;
        }
        return null;
    }

    public bool IsLastLevel(int level)
    {
        int max = 0;
        foreach(var item in listLevel)
        {
            if(item.level > max)
            {
                max = item.level;
            }
        }
        return max == level;
    }
}

[System.Serializable]
public class STMLevelData
{
    public int level;
    public GameSceneSO sceneAsset;
    public int ammoLimit;
}



