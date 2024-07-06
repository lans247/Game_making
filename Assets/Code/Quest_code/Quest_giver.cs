using System.Collections;
using System.Collections.Generic;
using Unity.IO.Archive;
using UnityEngine;

public class Quest_giver : MonoBehaviour
{
    public List<Quest_info> give_quest;
    public GameObject Player;
    public int give_num = 0;
    public void Start(){
        Player = GameObject.Find("Player");
    }


    public void Give()
    {
        Player.GetComponent<Quest_manager>().Now_quests.Add(give_quest[give_num]);         //퀘스트 주기
        give_num++;                                                                        //더 있으면 더 줌.
    }
}
