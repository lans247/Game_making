using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]


public class Quest_info : ScriptableObject
{
    int ID;                         //������ ���� ID


    [System.Serializable]
    public struct quest_data
    {
        public string information;
        public int type;     //��ȭ 0, ��� 1, ���� 2 
        public int require_id;
        public int amount;      //Ŭ�����ϱ� ���� ��
    }
    //���� ����Ʈ(�ϳ��� ����Ʈ�� ���� ���) ������ ���� ����
    public List<quest_data> data;

    public List<int> progress;      //�� ģ���� �����ؾ� �ؼ� struct���� ����

    public string Description;

    public string Title;
    public string reward;

    public GameObject[] previous;       //�ʿ��� ���� ����Ʈ
    public bool can_give;               //�� �� �ֳ� ���� üũ
}

