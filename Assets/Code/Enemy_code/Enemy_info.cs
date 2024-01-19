using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using UnityEngine;
[CreateAssetMenu]
public class Enemy_info : ScriptableObject
{
    public int ID;          //아이디
    public string Name;     //적 이름

    public int damage;      //공격력
    public float attack_speed;  //공격속도


    public int Max_HP;      //최대 공격력
    public int Max_MP;      //최대 마나

    public int Def;         //방어력
    
    public float speed;     //이동속도
    public float view;      //시야

    public Sprite Image;    //모습

    public List<GameObject> rewards;     //드랍 아이템들
}
