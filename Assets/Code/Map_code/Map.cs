using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    public map_info this_map;
    public int num;

    public void Start()
    {
        num = this_map.enemy_num;
    }
    IEnumerator dungeon_check()         //������ Ŭ���� ���� üũ
    {
        yield return new WaitUntil(() => num == 0);
        this_map.clear = true;
    }
}
