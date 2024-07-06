using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Enemy_Boss : Enemy_normal                  //노말을 상속.
{
    public GameObject UI;           //플레이어 UI
    public GameObject MY_UI;        //플레이어 유아이에 표시될 보스 정보
    public override void initial_setting()                    //기본 캐릭터 세팅
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;
        attack_speed = info.attack_speed;       //2가 최고 공속

        Player = GameObject.FindWithTag("Player");             //플레이어 받아오기

        //StartCoroutine(before_found_act());                   //플레이어와 만나기전 행동 없음
        //HP_bar = transform.GetChild(1).Find("HPbar");         //HP바 받아오기 필요없음
        
        //transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;    보스는 안함.
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
            MY_UI.transform.Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;      //이륾 표시
            StartCoroutine(attack_move());              //공격시작
        }
    }
    public override void HP_bar_indicate()
    {
       MY_UI.transform.Find("HPbar").GetComponent<Image>().fillAmount = HP * 1.0f / info.Max_HP; //체력감소 표시
    }

    public override IEnumerator attack_move()
    {
        yield return new WaitForSeconds(2f);


        //스킬 1 사용.
        Debug.Log("돌진");
        yield return StartCoroutine(skill_1());
       
        StartCoroutine(attack_move());
    }


    //돌진 스킬.
    public bool skill1_variable = false;    //충돌 체크
    public virtual IEnumerator skill_1()
    {
        Vector3 a = (transform.position - Player.transform.position).normalized * 0.01f;

        skill1_variable = false;
        while (!skill1_variable)
        {
            yield return new WaitForSeconds(0.001f);
            transform.position -= a;
        }
    }

    public virtual IEnumerator skill_2()
    {
        yield break;
    }
    public virtual IEnumerator skill_3()
    {
        yield break;
    }



    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        //충돌하면 체크
        if (!skill1_variable)
        {
            skill1_variable = true;
        }
    }



    public override void died()
    {
        UI.GetComponent<UI>().BOSS = false;
        base.died();

    }



}
