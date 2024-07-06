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
        public string information;              //���� ����Ʈ ����
        [Tooltip("��ȭ 0, ��� 1, ���� 2 ")]
        [Range(0, 2)]
        public int type;                      //��ȭ 0, ��� 1, ���� 2 
        public int require_id;                //��ȭ��, ����, ������ ������ID
        public int amount;                    //Ŭ�����ϱ� ���� ��
    }
    //���� ����Ʈ(�ϳ��� ����Ʈ�� ���� ���) ������ ���� ����
    public List<quest_data> data;

    public List<int> progress;      //�� ģ���� �����ؾ� �ؼ� struct���� ����

    public string Description;          //����Ʈ �� ����

    public string Title;                //����Ʈ �̸�.
    public Item_info reward;               //����

    public bool clear = false;          //Ŭ���� ����

    public bool can_give;               //�� �� �ֳ� ���� üũ
}

