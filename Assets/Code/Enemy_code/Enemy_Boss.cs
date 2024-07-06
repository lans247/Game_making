using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Enemy_Boss : Enemy_normal                  //�븻�� ���.
{
    public GameObject UI;           //�÷��̾� UI
    public GameObject MY_UI;        //�÷��̾� �����̿� ǥ�õ� ���� ����
    public override void initial_setting()                    //�⺻ ĳ���� ����
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;
        attack_speed = info.attack_speed;       //2�� �ְ� ����

        Player = GameObject.FindWithTag("Player");             //�÷��̾� �޾ƿ���

        //StartCoroutine(before_found_act());                   //�÷��̾�� �������� �ൿ ����
        //HP_bar = transform.GetChild(1).Find("HPbar");         //HP�� �޾ƿ��� �ʿ����
        
        //transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;    ������ ����.
        GetComponent<SpriteRenderer>().sprite = info.Image;

        UI = GameObject.FindWithTag("UI");
    }


    public override void founding()
    {
        if (Place.map_name == Player.GetComponent<Player>().now_map && !founded)        //���� �ʿ� ������
        {
            UI.GetComponent<UI>().BOSS = true;      //������ ����
            founded = true;
            MY_UI = UI.GetComponent<UI>().BOSS_UI;      //������ ��������
            MY_UI.transform.Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;      //�̐a ǥ��
            StartCoroutine(attack_move());              //���ݽ���
        }
    }
    public override void HP_bar_indicate()
    {
       MY_UI.transform.Find("HPbar").GetComponent<Image>().fillAmount = HP * 1.0f / info.Max_HP; //ü�°��� ǥ��
    }

    public override IEnumerator attack_move()
    {
        yield return new WaitForSeconds(2f);


        //��ų 1 ���.
        Debug.Log("����");
        yield return StartCoroutine(skill_1());
       
        StartCoroutine(attack_move());
    }


    //���� ��ų.
    public bool skill1_variable = false;    //�浹 üũ
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

        //�浹�ϸ� üũ
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
