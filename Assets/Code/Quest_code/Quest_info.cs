using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Quest : ScriptableObject
{
    int ID;
    public string Description;
    public int type;

    public string Title;
    public string reward;

    public GameObject[] previous;       //�ʿ��� ���� ����Ʈ
}
