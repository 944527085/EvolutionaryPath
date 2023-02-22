using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ViewPrefabDicData", menuName = "ScriptableObject/UI��Դ����", order = 0)]
public class ViewPrefabDic : ScriptableObject
{
    private void OnBeforeSerialize()
    {

    }
    public List<ViewPrefabData> _ViewPrefabDic;
}
[System.Serializable]
public struct ViewPrefabData
{
    public int ViewID;
    public string ViewPaht;
}