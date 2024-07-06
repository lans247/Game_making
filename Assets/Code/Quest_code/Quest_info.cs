using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]


public class Quest_info : ScriptableObject
{
    int ID;                         //관리를 위한 ID
    

    [System.Serializable]
    public struct quest_data
    {
        public string information;              //단일 퀘스트 설명
        [Tooltip("대화 0, 사냥 1, 수집 2 ")]
        [Range(0, 2)]
        public int type;                      //대화 0, 사냥 1, 수집 2 
        public int require_id;                //대화할, 잡을, 수집할 아이템ID
        public int amount;                    //클리어하기 위한 수
    }
    //다중 퀘스트(하나의 퀘스트에 여러 명령) 관리를 위한 구분
    public List<quest_data> data;

    public List<int> progress;      //이 친구는 수정해야 해서 struct에서 제외

    public string Description;          //퀘스트 총 설명

    public string Title;                //퀘스트 이름.
    public Item_info reward;               //보상

    public bool clear = false;          //클리어 여부

    public bool can_give;               //줄 수 있나 없나 체크
}

