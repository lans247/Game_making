using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu]
public class map_info : ScriptableObject
{
    public string map_name;

    public char type;               //T:����, D:���� Q:����Ʈ Ư�� ����
    public bool clear;
    public int enemy_num;
}
