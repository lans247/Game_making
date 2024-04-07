using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy_normal : MonoBehaviour
{

    public Enemy_info info; //불러올 에네미 정보, 불변

    public int HP;          //변하는 정보, 개인 값, 인포의 값을 불러어 덮어쒸움
    public int MP;
    public Transform HP_bar;
    public float speed;
    public float attack_speed;


    public bool hitted;         //에네미 무적 상태 유지를 위한 오브젝트, true일때 0.5초 사이로 타격을 받은 이후

    public bool founded = false; //적(주인공)을 찾았는가?

    public GameObject Player;   //플레이어 찾기

    public map_info Place;       //적이 존재하고 있는 맵 정보

    public void Start()
    {
        normal_setting();
    }

    public virtual void normal_setting()                    //기본 캐릭터 세팅
    {
        HP = info.Max_HP;                       
        MP = info.Max_MP;
        speed = info.speed;
        attack_speed = info.attack_speed;       //2가 최고 공속

        HP_bar = transform.GetChild(1).Find("HPbar");         //HP바 받아오기;

        Player = GameObject.FindWithTag("Player");             //플레이어 받아오기
        StartCoroutine(before_found_act());                          

        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //이름 표시
        GetComponent<SpriteRenderer>().sprite = info.Image;
    }

    public void FixedUpdate()
    {
        HP_bar.GetComponent<UnityEngine.UI.Image>().fillAmount = HP * 1.0f / info.Max_HP; //체력감소 표시



        if (!founded) { founding(); }   //플레이어가 범위에 없으면 찾기
        

        if (HP <= 0) //체력 없으면 사망
        {
            reward_drop();      //보상드랍

            Player.GetComponent<Quest_manager>().update_progress(1, info.ID);                    //퀘스트 조건 충족 -> 넘김
            GameObject.Find("Map_manager").GetComponent<Map_manager>().enemy_died(Place);       //맵 매니저에게 맵에 있는 에너미 사망을 보고

            Destroy(this.gameObject); 
        }      

    }




    public virtual void founding()      //플레이어 찾기, 일반적으로 시야, 보스는 같은 맵
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= info.view)        //플레이어가 시야보다 가까히 있으면
        {
            founded = true;                 //찾음
            StartCoroutine(attack_move());      //공격개시
        }
    }




    public virtual void reward_drop()       //보상 떨구기
    {
        if(info.rewards.Count > 0)
        {
            int r = Random.Range(0, info.rewards.Count);
            Instantiate(info.rewards[r], transform.position, Quaternion.Euler(0, 0, 0));
        } 
    }



    public virtual IEnumerator before_found_act()             //플레이어 찾기 전의 행동
    {
        while (!founded)
        {
            float random_ro = Random.Range(0.0f, 114.0f);

            Vector2 move = new Vector2(Mathf.Cos(random_ro), Mathf.Sin(random_ro));     //랜덤값

            for (int i = 0; i < (1/Time.deltaTime); i++)
            {
                transform.Translate(speed * move * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return new WaitForSeconds(attack_speed);
        }    
    }

    public virtual IEnumerator attack_move()        //공격 움직임  ,, 여기를 나중에 분해(각자의 공격방식과 연관)
    {
        while(founded)
        {
            for (int i = 0; i < (1 / Time.deltaTime); i++)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);        //방향으로 이동
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(attack_speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)              //플레이어와 추돌시
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 a = (Player.transform.position - transform.position).normalized;        //위치에서 위치까지 벡터를 정규화
            Player.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);
            Player.GetComponent<Player_combat>().HP -= damage_cal(info.damage);
        }
    }


    public int damage_cal(int damage)       //실질 데미지 계산
    {
        damage -= Player.GetComponent<Player_combat>().def;
        return damage;
    }
}
