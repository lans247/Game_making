using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Boss : Enemy_normal
{
    public GameObject UI;           //플레이어 UI
    public GameObject MY_UI;        //플레이어 유아이에 표시될 보스 정보
    public override void normal_setting()                    //기본 캐릭터 세팅
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;
        attack_speed = info.attack_speed;       //2가 최고 공속

        Player = GameObject.FindWithTag("Player");             //플레이어 받아오기

        //StartCoroutine(before_found_act());                   //플레이어와 만나기전 행동 없음
        //HP_bar = transform.GetChild(1).Find("HPbar");         //HP바 받아오기 필요없음
        
        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //이름 표시
        GetComponent<SpriteRenderer>().sprite = info.Image;

        UI = GameObject.FindWithTag("UI");
    }


    public override void founding()
    {
        if (Place.map_name == Player.GetComponent<Player>().now_map && !founded)        //같은 맵에 있으면
        {
            UI.GetComponent<UI>().BOSS = true;      //보스전 시작
            founded = true;
            MY_UI = UI.GetComponent<UI>().BOSS_UI;      //유아이 가져오기
        }
    }

}
