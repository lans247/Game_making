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

    public Enemy_info info; //�ҷ��� ���׹� ����, �Һ�


    public int HP;          //���ϴ� ����
    public int MP;
    public Transform HP_bar;
    public float speed;
    public float attack_speed;


    public bool hitted;         //���׹� ���� ���� ������ ���� ������Ʈ, true�϶� 0.5�� ���̷� Ÿ���� ���� ����

    public bool founded = false; //��(���ΰ�)�� ã�Ҵ°�?

    public GameObject Player;


    private void Start()
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;    
        attack_speed = info.attack_speed;       //2�� �ְ� ����

        HP_bar = transform.GetChild(1).Find("HPbar");         //HP�� �޾ƿ���;

        Player = GameObject.FindWithTag("Player");
        StartCoroutine("random_act");

        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //�̸� ǥ��
        GetComponent<SpriteRenderer>().sprite = info.Image;
        
    }

    public void FixedUpdate()
    {
        HP_bar.GetComponent<UnityEngine.UI.Image>().fillAmount = HP * 1.0f / info.Max_HP; //ü�°��� ǥ��



        if (!founded) { founding(); }   //��ã���� ã��
        


        if (HP <= 0) //ü�� ������ ���
        {
            reward_drop();      //������

            Player.GetComponent<Quest_manager>().update_progress(1, info.ID);       //����Ʈ ���� ���� -> �ѱ�

            Destroy(this.gameObject); 
        }      

    }




    public void founding()      //�÷��̾� ã��
    {
        if(Vector2.Distance(Player.transform.position, transform.position) <= info.view)        //�÷��̾ �þߺ��� ������ ������
        {
            founded = true;                 //ã��
            StartCoroutine("attack_move");      //���ݰ���
        }
    }




    public void reward_drop()       //���� ������
    {
        if(info.rewards.Count > 0)
        {
            int r = Random.Range(0, info.rewards.Count);
            Instantiate(info.rewards[r], transform.position, Quaternion.Euler(0, 0, 0));
        } 
    }








    

    public IEnumerator random_act()             //�������� ������
    {
        while (!founded)
        {
            float random_ro = Random.Range(0.0f, 114.0f);

            Vector2 move = new Vector2(Mathf.Cos(random_ro), Mathf.Sin(random_ro));     //������

            for (int i = 0; i < (1/Time.deltaTime); i++)
            {
                transform.Translate(speed * move * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            yield return new WaitForSeconds(attack_speed);
        }    
    }

    public IEnumerator attack_move()        //���� ������  ,, ���⸦ ���߿� ����(������ ���ݹ�İ� ����)
    {
        while(founded)
        {
            for (int i = 0; i < (1 / Time.deltaTime); i++)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);        //�������� �̵�
                yield return new WaitForSeconds(Time.deltaTime);
            }
            yield return new WaitForSeconds(attack_speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)              //�÷��̾�� �ߵ���
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 a = (Player.transform.position - transform.position).normalized;        //��ġ���� ��ġ���� ���͸� ����ȭ
            Player.GetComponent<Rigidbody2D>().AddForce(a * 40, ForceMode2D.Impulse);
            Player.GetComponent<Player_combat>().HP -= damage_cal(info.damage);
        }
    }


    public int damage_cal(int damage)       //���� ������ ���
    {
        damage -= Player.GetComponent<Player_combat>().def;
        return damage;
    }
}
