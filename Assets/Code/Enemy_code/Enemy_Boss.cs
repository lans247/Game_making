using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Boss : Enemy_normal
{
    public GameObject UI;           //�÷��̾� UI
    public GameObject MY_UI;        //�÷��̾� �����̿� ǥ�õ� ���� ����
    public override void normal_setting()                    //�⺻ ĳ���� ����
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = info.speed;
        attack_speed = info.attack_speed;       //2�� �ְ� ����

        Player = GameObject.FindWithTag("Player");             //�÷��̾� �޾ƿ���

        //StartCoroutine(before_found_act());                   //�÷��̾�� �������� �ൿ ����
        //HP_bar = transform.GetChild(1).Find("HPbar");         //HP�� �޾ƿ��� �ʿ����
        
        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //�̸� ǥ��
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
        }
    }

}
