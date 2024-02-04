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
        public string information;
        public int type;     //대화 0, 사냥 1, 수집 2 
        public int require_id;
        public int amount;      //클리어하기 위한 수
    }
    //다중 퀘스트(하나의 퀘스트에 여러 명령) 관리를 위한 구분
    public List<quest_data> data;

    public List<int> progress;      //이 친구는 수정해야 해서 struct에서 제외

    public string Description;

    public string Title;
    public string reward;

    public GameObject[] previous;       //필요한 선행 퀘스트
    public bool can_give;               //줄 수 있나 없나 체크
}

