using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player_combat : MonoBehaviour
{
    public List<int> Personality = new List<int>();     //성격

    //실제 적용 스텟
    public int Max_HP;
    public int HP;
    public int Max_MP;
    public int MP;
    public int def;
    public float attack_speed;
    public int damage;
    public int recover_info;            //재생 수치, 기본 1

    //기본 스텟
    public int normal_Max_HP = 100;
    public int normal_Max_MP = 100;
    public int normal_def = 0;
    public float normal_attack_speed = 1.0f;
    public int normal_damage = 10;
    public int normal_recover_info;

    //버프 스텟
    public int buff_Max_HP;
    public int buff_Max_MP;
    public int buff_def;
    public float buff_attack_speed = 1;             //공속 버프는 곱연산 기본 1
    public int buff_damage;
    public int buff_recover_info;



    public UnityEngine.UI.Image attack_delay_image;     //공격 딜레이를 표시할 인디케이터 이미지

   
    public bool recovering = false;     //재생중
    


    public GameObject shield;       //쉴드


    public GameObject UI;               //화면에 표시되는 UI

    public GameObject Player_UI;        //캐릭터 주변에 표시될 UI


    public UnityEngine.UI.Image HPbar;
    public TextMeshProUGUI HPnum;


    public List<GameObject> attack_image;   //공격 이미지
    public bool attacking = false;

    public GameObject indicator;        //알림창, 데미지 혹은 체력 회복시




    public Skill_info[] equips = new Skill_info[5];     //스킬 데이터



    void Start()
    {
        stat_manage();

        HP = Max_HP;
        MP = Max_MP;



        //화면 UI 가져오기
        UI = GameObject.FindWithTag("UI"); 
        HPbar = UI.transform.Find("HPbar").GetComponent<UnityEngine.UI.Image>();
        HPnum = UI.transform.Find("HPnum").GetComponent<TextMeshProUGUI>();


        Player_UI = transform.Find("indicate").gameObject;
        attack_delay_image = Player_UI.transform.Find("attack_delay").GetComponent<UnityEngine.UI.Image>();

        shield = transform.Find("shield").gameObject;
        shield.SetActive(false);
        

        Personality_reset();                                            //성격 리셋
    }

    public void stat_manage()           //버프와 기타 등등에 대한 스텟 관리
    {
        //기본 수치와 버프 스텟을 실 스텟에 적용 
        Max_HP = normal_Max_HP + buff_Max_HP;
        Max_MP = normal_Max_MP + buff_Max_MP;
        def = normal_def + buff_def;
        attack_speed = normal_attack_speed * buff_attack_speed;
        damage = normal_damage + buff_damage;
        recover_info = normal_recover_info + buff_recover_info;
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

        skill_manage();



        //버프 체크
        stat_manage();



    }

    void Update()
    {
        if(Input.GetMouseButton(1) && shield.activeSelf == false)       //우클릭, 쉴드중 아닐때
        {
            StartCoroutine(shield_act());
        }
    }


    IEnumerator shield_act()
    { 
        shield.SetActive(true);


        float normal_speed = GetComponent<Player>().speed;
        GetComponent<Player>().can_move = false;                   //쉴드 중에는 정지


        //마우스 땔때까지 계속 유지
        while (true)
        {
            //쉴드 생성
            UnityEngine.Vector2 ve = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;     //클릭하는 방향
            quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //방향
            shield.transform.rotation = normal;         //클릭 방향으로 회전

            if (Input.GetMouseButton(1) == false)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<Player>().can_move = true;        //다시 움직임
        shield.SetActive(false);

    }




    void attack()           //공격 / 딜레이 호출 -> 딜레이에서 콤보 호출 -> 콤보 끝나면 대기후 다시 복귀
    {
        if (Input.GetMouseButton(0) && !attacking)
        {
            attacking = true;
            StartCoroutine(attack_delay());
        }
    }

    IEnumerator attack_delay()      //공격딜레이
    {
        yield return StartCoroutine(combo_attack());

        float attack_delay_time = 2 - attack_speed; //실제로 기다리는 시간

        while(attack_delay_image.fillAmount != 1)     
        {
            attack_delay_image.fillAmount += 0.01f;
            yield return new WaitForSeconds(attack_delay_time * 0.01f);      
        }

        attack_delay_image.fillAmount = 0;
        attacking = false;
    }


    IEnumerator combo_attack()          //여러번 공격, 기본적으로 1번 공격도 이것이 호출
    {
        int combo_num = 0;
        float combo_delay = attack_speed * 0.5f;
        int max_combo = attack_image.Count;

        while(combo_num < max_combo)
        {
            UnityEngine.Vector2 ve = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;     //클릭하는 방향
            quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //방향


            attack_image[combo_num].GetComponent<attack_image>().damage = damage;      //만들이미지에 데미지 넣기
            Instantiate(attack_image[combo_num], transform.position, normal);         //1칸 띄어서 생성

            combo_num++;


            //다음 콤보 연계
            yield return new WaitForSeconds(1 - combo_delay);
            if(!Input.GetMouseButton(0))        //콤보딜레이 사이동안 입력이 없으면 콤보어택 취소
            {
                break;
            }
        }

    }


    /**
     * 스킬 파트
     * 착용 중인 스킬을 ID를 통해서 사용
     * skill_info에는 id와 이름, 쿨타임을 넣고.
     * skill를 사용하면 UI에서 쿨타임을 계산하고, skill manage에서 스킬을 발동
     */


    void skill_manage()
    {
        UI.GetComponent<UI>().equips = equips;


        //cool이 true일때 사용가능한 상태. 스킬번호와 키패드와 1 차이가 남. 신경써야 함
        if (equips[0] != null && Input.GetKey(KeyCode.Alpha1) && equips[0].cool)
        {
            skill_using(equips[0].id);                                                          //장착한 스킬의 ID를 스킬 유징으로 넘김
            UI.GetComponent<UI>().StartCoroutine(UI.GetComponent<UI>().cool_manage(0));         //UI에 있는 쿨타임 매니지 시작
        }
        if (equips[1] != null && Input.GetKey(KeyCode.Alpha2) && equips[1].cool)
        {
            Debug.Log("skill2 발동");
            UI.GetComponent<UI>().StartCoroutine(UI.GetComponent<UI>().cool_manage(1));
        }
        if (equips[2] != null && Input.GetKey(KeyCode.Alpha3) && equips[2].cool)
        {
            Debug.Log("skill3 발동");
            UI.GetComponent<UI>().StartCoroutine(UI.GetComponent<UI>().cool_manage(2));
        }
        if (equips[3] != null && Input.GetKey(KeyCode.Alpha4) && equips[3].cool)
        {
            Debug.Log("skill4 발동");
            UI.GetComponent<UI>().StartCoroutine(UI.GetComponent<UI>().cool_manage(3));
        }
        if (equips[4] != null && Input.GetKey(KeyCode.Alpha5) && equips[4].cool)
        {
            Debug.Log("skill5 발동");
            UI.GetComponent<UI>().StartCoroutine(UI.GetComponent<UI>().cool_manage(4));
        }

    }


    void skill_using(int id)            //해당하는 아이디의 스킬 시작
    {
        switch(id)
        {
            case 0:
                StartCoroutine(skill_0());
                break;



            default:
                break;
        }


    }

    IEnumerator skill_0()
    {
        buff_damage += 10;               //데미지 증가
        buff_attack_speed *= 1.5f;       //공격 속도 1.5배
        yield return new WaitForSeconds(2f);
        buff_damage -= 10;
        buff_attack_speed /= 1.5f;
        yield return null;
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
        
    }

















    public void indicate(int dama)       //데미지 인디케이터 소환, 리커버리도 포함
    {
        indicator.GetComponent<indicator>().type = "recover";
        indicator.GetComponent<indicator>().content = dama;
        Instantiate(indicator, transform.position, UnityEngine.Quaternion.Euler(0, 0, 0));
    }
}
