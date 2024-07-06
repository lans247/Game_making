using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Quest_manager : MonoBehaviour
{
    public GameObject UI;

    public List<Quest_info> Now_quests = new List<Quest_info>();
    public List<Quest_info> Clear_quests = new List<Quest_info>();


    public int manage_quest_num;        //�����ϴ� ����Ʈ ��

    private void Start()
    {
        UI = GameObject.Find("UI").gameObject;
    }


    private void Update()
    {
        if(Now_quests.Count > manage_quest_num)     //����Ʈ �߰� �Ǿ��� ���
        {
            manage_quest_num++;
            initial_quest(manage_quest_num-1);
        }

        if(manage_quest_num!=0) MainQuest();                               //����Ʈ ǩ;
    }


    public void initial_quest(int num)      //���൵ �ʱ�ȭ
    {
        for(int i =0; i< Now_quests[num].data.Count; i++)
        {
            Now_quests[num].clear = false;                  //Ŭ���� false
            Now_quests[num].progress[i] = 0;                //���൵ 0

            if (Now_quests[num].data[i].type == 2){         //���ο� ����Ʈ�� ������ ���, �κ��丮�� üũ�ؾ���.
                inven_check(num, i);
            }
        }
    }
    public void inven_check(int num, int i){          //id�� ���ؼ� inven�� ����ִ��� üũ.
        List<Item_info> bag = this.gameObject.GetComponent<item_manager>().bag;

        i = 0;
        while(i < bag.Count)
        {
            if (bag[i].id == Now_quests[num].data[i].require_id){           //�κ��丮 �ȿ� ���ϴ� �������� �ִٸ�, ���൵ ����.
                Now_quests[num].progress[i]++;
            }
        }

    }

    public void update_progress(int data_type, int id)      //0 : ��ȭ, 1 : ���, 2 : ����, �� �Լ��� ��ȭ NPC, ��, �κ��丮���� ���� ���� ������ ȣ��
    {
        for (int i = 0; i < manage_quest_num; i++)          //���� ���� ����Ʈ ���θ� �����ؼ�, �ش� ������Ʈ�� �ִ���üũ
        {
            manage(Now_quests[i], data_type, id);
        }
    }

    public void manage(Quest_info a, int data_type, int id)         //���ο� ���������� ����Ʈ ������ ��Ī
    {
        int num = a.data.Count;     //����Ʈ ���� ����(���� 1)
        bool Quest_prog = false;    //�� ����Ʈ�� �� �ѹ��̶� ����� ���� �ִٸ�.

        for(int i = 0; i < num; i++)        //����Ʈ ���� ����
        {
            if(a.data[i].type == data_type && a.data[i].require_id == id)   //�� ����Ʈ�� ����� ���� ������, �Է� ���� update����� ����Ʈ ��Ͽ� �ִ� �Ͱ� �´ٸ�
            {
                Quest_prog = true;
                a.progress[i]++;                //���൵ �߰�.
            }
        }

        //���൵�� �߰� �Ǿ����� Ŭ���� üũ�ϱ�.
        if (Quest_prog) clear_check(a);
    }


    public void clear_check(Quest_info Q)
    {
        int n = Q.data.Count;         //����Ʈ�� ������ �ִ� �޼���ǥ��
        for (int i = 0; i < n; i++){
            if (Q.data[i].amount > Q.progress[i]){           //��ǥ �̴޼���
                return;
            }
        }
        //���⼭���ʹ� ����Ʈ �޼��Ϸ�
        Q.clear = true;
        this.gameObject.GetComponent<item_manager>().get_item(Q.reward);      //���� ����
        Clear_quests.Add(Q);                                                  //Ŭ������ ����Ʈ�� üũ�ϰ�.
        Now_quests.Remove(Q);                                                 //������ ����Ʈ���� ����
    }

    int rep = 0;
    public void MainQuest()
    {
        if (Now_quests.Count == 0) return;
        TextMeshProUGUI quest_UI = UI.transform.Find("Quest_info").GetComponent<TextMeshProUGUI>();
        quest_UI.text = "<size=80%>" + "Quest : " + Now_quests[rep].Description.ToString() + "\n";
        for (int i =0; i < Now_quests[rep].data.Count; i++)
        {
            quest_UI.text += Now_quests[rep].data[i].information.ToString();
            quest_UI.text += "(" + Now_quests[rep].progress[i] + "/" + Now_quests[rep].data[i].amount + ")";
            quest_UI.text += "\n";
        }
         
    }
}
