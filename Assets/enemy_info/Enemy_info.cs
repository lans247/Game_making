using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using UnityEngine;
[CreateAssetMenu]
public class Enemy_info : ScriptableObject
{
    public int ID;          //���̵�
    public string Name;     //�� �̸�

    public int damage;      //���ݷ�
    public float attack_speed;  //���ݼӵ�


    public int Max_HP;      //�ִ� ���ݷ�
    public int Max_MP;      //�ִ� ����

    public int Def;         //����
    
    public float speed;     //�̵��ӵ�
    public float view;      //�þ�

    public Sprite Image;    //���

    public List<GameObject> rewards;     //��� �����۵�
}
