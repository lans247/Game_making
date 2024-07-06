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


    public int manage_quest_num;        //관리하는 퀘스트 수

    private void Start()
    {
        UI = GameObject.Find("UI").gameObject;
    }


    private void Update()
    {
        if(Now_quests.Count > manage_quest_num)     //퀘스트 추가 되었을 경우
        {
            manage_quest_num++;
            initial_quest(manage_quest_num-1);
        }

        if(manage_quest_num!=0) MainQuest();                               //퀘스트 푯;
    }


    public void initial_quest(int num)      //진행도 초기화
    {
        for(int i =0; i< Now_quests[num].data.Count; i++)
        {
            Now_quests[num].clear = false;                  //클리어 false
            Now_quests[num].progress[i] = 0;                //진행도 0

            if (Now_quests[num].data[i].type == 2){         //새로운 퀘스트가 수집인 경우, 인벤토리를 체크해야함.
                inven_check(num, i);
            }
        }
    }
    public void inven_check(int num, int i){          //id를 통해서 inven에 들어있는지 체크.
        List<Item_info> bag = this.gameObject.GetComponent<item_manager>().bag;

        i = 0;
        while(i < bag.Count)
        {
            if (bag[i].id == Now_quests[num].data[i].require_id){           //인벤토리 안에 원하는 아이템이 있다면, 진행도 증가.
                Now_quests[num].progress[i]++;
            }
        }

    }

    public void update_progress(int data_type, int id)      //0 : 대화, 1 : 사냥, 2 : 수집, 이 함수는 대화 NPC, 몹, 인벤토리에서 변경 사항 감지시 호출
    {
        for (int i = 0; i < manage_quest_num; i++)          //소지 중인 퀘스트 전부를 점검해서, 해당 업데이트가 있는지체크
        {
            manage(Now_quests[i], data_type, id);
        }
    }

    public void manage(Quest_info a, int data_type, int id)         //새로운 진행정보와 퀘스트 정보랑 매칭
    {
        int num = a.data.Count;     //퀘스트 내부 숫자(보통 1)
        bool Quest_prog = false;    //이 퀘스트가 단 한번이라도 진행된 적이 있다면.

        for(int i = 0; i < num; i++)        //퀘스트 점검 시작
        {
            if(a.data[i].type == data_type && a.data[i].require_id == id)   //이 퀘스트가 수행된 것이 맞으면, 입력 받은 update내용과 퀘스트 목록에 있는 것과 맞다면
            {
                Quest_prog = true;
                a.progress[i]++;                //진행도 추가.
            }
        }

        //진행도가 추가 되었으니 클리어 체크하기.
        if (Quest_prog) clear_check(a);
    }


    public void clear_check(Quest_info Q)
    {
        int n = Q.data.Count;         //퀘스트가 가지고 있는 달성목표수
        for (int i = 0; i < n; i++){
            if (Q.data[i].amount > Q.progress[i]){           //목표 미달성시
                return;
            }
        }
        //여기서부터는 퀘스트 달성완료
        Q.clear = true;
        this.gameObject.GetComponent<item_manager>().get_item(Q.reward);      //보싱 수여
        Clear_quests.Add(Q);                                                  //클리어한 퀘스트를 체크하고.
        Now_quests.Remove(Q);                                                 //실행중 퀘스트에서 제거
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
