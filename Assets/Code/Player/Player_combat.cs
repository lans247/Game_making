using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player_combat : MonoBehaviour
{
    public List<int> Personality = new List<int>();     //����

    public int Max_HP = 100;
    public int HP;
    public int Max_MP = 100;
    public int MP;
    public int def = 0;
    public float attack_speed = 1.0f;
    public int damage = 10;
    public bool recovering = false;     //�����
    public int recover_info = 1;            //��� ��ġ, �⺻ 1

    public GameObject UI;


    public UnityEngine.UI.Image HPbar;
    public TextMeshProUGUI HPnum;


    public List<GameObject> attack_image;
    public bool attacking = false;

    public GameObject indicator;        //�˸�â, ������ Ȥ�� ü�� ȸ����


    void Start()
    {

        HP = Max_HP;
        MP = Max_MP;
        
        UI = GameObject.FindWithTag("UI");  //UI ��������

        HPbar = UI.transform.Find("HPbar").GetComponent<UnityEngine.UI.Image>();
        HPnum = UI.transform.Find("HPnum").GetComponent<TextMeshProUGUI>();

        

        Personality_reset();                                            //���� ����
    }

    
    void FixedUpdate()
    {
        HPbar.fillAmount = HP * 1.0f / Max_HP;   //HP�� �����ͼ� ����
        HPnum.text = HP.ToString();

        attack();

        if(!recovering)     //ȸ���� �ƴҶ� ��� ����
        {
            StartCoroutine("recover");
        }

        Personality_setting();      //���ݿ� ���� ��ȭ


    }

    

    void attack()           //���� / ������ ȣ�� -> �����̿��� �޺� ȣ�� -> �޺� ������ ����� �ٽ� ����
    {
        if (Input.GetMouseButton(0) && !attacking)
        {
            attacking = true;
            StartCoroutine(attack_delay());
        }
    }

    IEnumerator attack_delay()      //���ݵ�����
    {
        yield return StartCoroutine(combo_attack());
        Debug.Log("�ʼ��� ����");
        yield return new WaitForSeconds(2 - attack_speed);
        attacking = false;
    }


    IEnumerator combo_attack()
    {
        int combo_num = 0;
        float combo_delay = attack_speed * 0.5f;
        int max_combo = attack_image.Count;

        while(combo_num < max_combo)
        {
            UnityEngine.Vector2 ve = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;     //Ŭ���ϴ� ����
            quaternion normal = quaternion.Euler(0, 0, Mathf.Atan2(ve.y, ve.x) - 3.14f);                //����


            attack_image[combo_num].GetComponent<attack_image>().damage = damage;      //�����̹����� ������ �ֱ�
            Instantiate(attack_image[combo_num], transform.position, normal);         //1ĭ �� ����

            combo_num++;


            //���� �޺� ����
            yield return new WaitForSeconds(1 - combo_delay);
            if(!Input.GetMouseButton(0))        //�޺������� ���̵��� �Է��� ������ �޺����� ���
            {
                break;
            }
        }

    }








    IEnumerator recover()       //���
    {
        recovering = true;      //�����

        int now = HP;           //�񱳿� HP

        for(int i = 0;i<10;i++)
        {
            yield return new WaitForSeconds(1f);
            if(now < HP)        //�ǰ� �������� �۾�����(������ ������)
            {
                recovering = false;
                yield break;       //��Ŀ���� ��������
            }
        }
        while(HP < Max_HP)      //�ִ�ü�� ����
        {
            HP += recover_info;     //ȸ��
            indicate(recover_info);
            yield return new WaitForSeconds(1f);    //�ð�����
        }

    }


    public void Personality_reset()     //�ʱ� ����
    {
        Debug.Log("innitial");
        Personality.Add(0);       //���� , ��� 0
        Personality.Add(0);       //Ž�� , �ڼ� 1
        Personality.Add(0);        //���� , ģ��2
        Personality.Add(0);       //�г� , �γ� 3
        Personality.Add(0);        //���� , ����4
        Personality.Add(0);    //Ž�� , ����    5
        Personality.Add(0);       //���� , �ٸ� 6
    }

    public void Personality_setting()   //������ ���� ��ȭ
    {
        
    }

















    public void indicate(int dama)       //������ �ε������� ��ȯ, ��Ŀ������ ����
    {
        indicator.GetComponent<indicator>().type = "recover";
        indicator.GetComponent<indicator>().content = dama;
        Instantiate(indicator, transform.position, UnityEngine.Quaternion.Euler(0, 0, 0));
    }
}
