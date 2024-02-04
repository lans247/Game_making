using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Quest_manager : MonoBehaviour
{
    public List<Quest_info> Now_quests = new List<Quest_info>();
    public List<Quest_info> Clear_quests = new List<Quest_info>();


    public int manage_quest_num;        //관리하는 퀘스트 수

    private void Update()
    {
        if(Now_quests.Count > manage_quest_num)     //퀘스트 추가 되었을 경우
        {
            manage_quest_num++;
            initial_quest(manage_quest_num-1);
        }

    }


    public void initial_quest(int num)      //진행도 초기화
    {
        for(int i =0; i< Now_quests[num].data.Count; i++)
        {
            Now_quests[num].progress[i] = 0;
        }
    }


    public void update_progress(int data_type, int id)      //0 : 대화, 1 : 사냥, 2 : 수집, 이 함수는 대화 NPC, 몹, 인벤토리에서 변경 사항 감지시 호출
    {
        for (int i = 0; i < manage_quest_num; i++)          //소지 중인 퀘스트 전부를 점검해서, 해당 업데이트가 있는지체크
        {
            manage(Now_quests[i], data_type, id);
        }
    }

    public void manage(Quest_info a, int data_type, int id)
    {
        int num = a.data.Count;     //퀘스트 숫자

        for(int i = 0; i < num; i++)        //퀘스트 점검 시작
        {
            if(a.data[i].type == data_type && a.data[i].require_id == id)   //이 퀘스트가 수행된 것이 맞으면, 입력 받은 update내용과 퀘스트 목록에 있는 것과 맞다면
            {
                a.progress[i]++;
            }
        }

    }


    public void clear_check()
    {

    }
}
