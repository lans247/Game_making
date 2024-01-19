using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player_combat : MonoBehaviour
{
    public List<int> Personality = new List<int>();     //성격

    public int Max_HP = 100;
    public int HP;
    public int Max_MP = 100;
    public int MP;
    public int def = 0;
    public float attack_speed = 1.0f;
    public int damage = 10;
    public bool recovering = false;     //재생중
    public int recover_info = 1;            //재생 수치, 기본 1

    public GameObject UI;


    public Image HPbar;
    public TextMeshProUGUI HPnum;


    public GameObject attack_image;
    public bool attacking = false;

    public GameObject indicator;        //알림창, 데미지 혹은 체력 회복시


    void Start()
    {

        HP = Max_HP;
        MP = Max_MP;
        
        UI = GameObject.FindWithTag("UI");  //UI 가져오기

        HPbar = UI.transform.Find("HPbar").GetComponent<Image>();
        HPnum = UI.transform.Find("HPnum").GetComponent<TextMeshProUGUI>();

        

        Personality_reset();                                            //성격 리셋
    }

    
    void FixedUpdate()
    {
        HPbar.fillAmount = HP * 1.0f / Max_HP;   //HP바 가져와서 쓰기
        HPnum.text = HP.ToString();

        attack();

        if(!recovering)     //회복중 아닐때 재생 시행
        {
            StartCoroutine("recover");
        }

        Personality_setting();      //성격에 따른 변화


    }


    void attack()           //공격
    {
        if (Input.GetMouseButton(0) && !attacking)
        {
            StartCoroutine("attack_delay");

            Vector2 ve = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;     //클릭하는 방향
            quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //방향


            attack_image.GetComponent<attack_image>().damage = damage;      //만들이미지에 데미지 넣기
            Instantiate(attack_image, transform.position, normal);         //1칸 띄어서 생성
            attacking = true;
        }
    }

    IEnumerator attack_delay()      //공격딜레이
    {
        yield return new WaitForSeconds(2 - attack_speed);
        attacking = false;
        yield break;
    }



    IEnumerator recover()       //재생
    {
        recovering = true;      //재생중

        int now = HP;           //비교용 HP

        for(int i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(1f);
            if(now < HP)        //피가 기존보다 작아지면(데미지 받으면)
            {
                recovering = false;
                yield break;       //리커버링 꺼버리기
            }
        }
        while(HP < Max_HP)      //최대체력 까지
        {
            HP += recover_info;     //회복
            indicate(recover_info);
            yield return new WaitForSeconds(1f);    //시간마다
        }

    }


    public void Personality_reset()     //초기 세팅
    {
        Debug.Log("innitial");
        Personality.Add(0);       //교만 , 겸손 0
        Personality.Add(0);       //탐욕 , 자선 1
        Personality.Add(0);        //질투 , 친절2
        Personality.Add(0);       //분노 , 인내 3
        Personality.Add(0);        //음욕 , 정결4
        Personality.Add(0);    //탐욕 , 절제    5
        Personality.Add(0);       //나태 , 근면 6
    }

    public void Personality_setting()   //세팅이 따른 변화
    {
        if (Personality[3] > 10)  //분노
        {
            Max_HP = 150;
        }
        else if (Personality[3] < -10)
        {
            def = 10;
        }

        if (Personality[6] > 10)
        {
            attack_speed = 0.8f;
            damage = 20;
        }
        else if (Personality[6] < -10)
        {
            attack_speed = 1.5f;
        }
    }

















    public void indicate(int dama)       //데미지 인디케이터 소환
    {
        indicator.GetComponent<indicator>().type = "recover";
        indicator.GetComponent<indicator>().content = dama;
        Instantiate(indicator, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
