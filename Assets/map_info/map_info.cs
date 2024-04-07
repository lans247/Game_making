using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class map_info : ScriptableObject
{
    public string map_name;

    public char type;               //T:마을, D:던전 Q:퀘스트 특별 구역
    public bool clear;
    public int enemy_num;
}
