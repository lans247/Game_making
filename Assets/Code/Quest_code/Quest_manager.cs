using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest_manager : MonoBehaviour
{
    public List<Quest_info> Now_quests = new List<Quest_info>();
    public List<Quest_info> Clear_quests = new List<Quest_info>();


    public int manage_quest_num;        //�����ϴ� ����Ʈ ��

    private void Update()
    {
        if(Now_quests.Count > manage_quest_num)     //����Ʈ �߰� �Ǿ��� ���
        {
            manage_quest_num++;
            initial_quest(manage_quest_num-1);
        }

    }


    public void initial_quest(int num)      //���൵ �ʱ�ȭ
    {
        for(int i =0; i< Now_quests[num].data.Count; i++)
        {
            Now_quests[num].progress[i] = 0;
        }
    }


    public void update_progress(int data_type, int id)      //0 : ��ȭ, 1 : ���, 2 : ����, �� �Լ��� ��ȭ NPC, ��, �κ��丮���� ���� ���� ������ ȣ��
    {
        for (int i = 0; i < manage_quest_num; i++)          //���� ���� ����Ʈ ���θ� �����ؼ�, �ش� ������Ʈ�� �ִ���üũ
        {
            manage(Now_quests[i], data_type, id);
        }
    }

    public void manage(Quest_info a, int data_type, int id)
    {
        int num = a.data.Count;     //����Ʈ ����

        for(int i = 0; i < num; i++)        //����Ʈ ���� ����
        {
            if(a.data[i].type == data_type && a.data[i].require_id == id)   //�� ����Ʈ�� ����� ���� ������, �Է� ���� update����� ����Ʈ ��Ͽ� �ִ� �Ͱ� �´ٸ�
            {
                a.progress[i]++;
            }
        }

    }


    public void clear_check()
    {

    }
}
