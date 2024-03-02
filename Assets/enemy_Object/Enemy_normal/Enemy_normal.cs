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


    public int HP;          //변하는 정보
    public int MP;
    public Transform HP_bar;
    public float speed;
    public float attack_speed;


    public bool hitted;         //에네미 무적 상태 유지를 위한 오브젝트, true일때 0.5초 사이로 타격을 받은 이후

    public bool founded = false; //적(주인공)을 찾았는가?

    public GameObject Player;


    private void Start()
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;    
        attack_speed = info.attack_speed;       //2가 최고 공속

        HP_bar = transform.GetChild(1).Find("HPbar");         //HP바 받아오기;

        Player = GameObject.FindWithTag("Player");
        StartCoroutine("random_act");

        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //이름 표시
        GetComponent<SpriteRenderer>().sprite = info.Image;
        
    }

    public void FixedUpdate()
    {
        HP_bar.GetComponent<UnityEngine.UI.Image>().fillAmount = HP * 1.0f / info.Max_HP; //체력감소 표시



        if (!founded) { founding(); }   //못찾으면 찾기
        


        if (HP <= 0) //체력 없으면 사망
        {
            reward_drop();      //보상드랍

            Player.GetComponent<Quest_manager>().update_progress(1, info.ID);       //퀘스트 조건 충족 -> 넘김

            Destroy(this.gameObject); 
        }      

    }




    public void founding()      //플레이어 찾기
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= info.view)        //플레이어가 시야보다 가까히 있으면
        {
            founded = true;                 //찾음
            StartCoroutine("attack_move");      //공격개시
        }
    }




    public void reward_drop()       //보상 떨구기
    {
        if(info.rewards.Count > 0)
        {
            int r = Random.Range(0, info.rewards.Count);
            Instantiate(info.rewards[r], transform.position, Quaternion.Euler(0, 0, 0));
        } 
    }








    

    public IEnumerator random_act()             //랜덤으로 움직임
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

    public IEnumerator attack_move()        //공격 움직임  ,, 여기를 나중에 분해(각자의 공격방식과 연관)
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
