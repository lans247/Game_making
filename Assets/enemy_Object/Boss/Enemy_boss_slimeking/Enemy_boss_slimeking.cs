using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_boss_slimeking : MonoBehaviour
{
    public Enemy_info info; //�ҷ��� ���׹� ����, �Һ�


    public int HP;          //���ϴ� ����
    public int MP;
    
    public float speed;
    public float attack_speed;

    public GameObject Player;
    public GameObject UI;

    public GameObject MY_UI;        //�÷��̾� �����̿� ǥ�õ� ���� ����

    public bool founded = false;       //�÷��̾� ã�Ҵ���

    [SerializeField]
    string now_map;     //���� �� �̸�

    

    private void Start()
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = 2;
        attack_speed = info.attack_speed;       //2�� �ְ� ����

        Player = GameObject.FindWithTag("Player");
        UI = GameObject.FindWithTag("UI");

        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //�̸� ǥ��
        GetComponent<SpriteRenderer>().sprite = info.Image;
    }

    public void FixedUpdate()
    {
        if (now_map == Player.GetComponent<Player>().now_map && !founded)        //���� �ʿ� ������
        {
            UI.GetComponent<UI>().BOSS = true;      //������ ����
            founded = true;
            MY_UI = UI.GetComponent<UI>().BOSS_UI;      //������ ��������
        }
        if (founded)
        {
            combat();
        }
    }

    public void combat()
    {
        MY_UI.transform.Find("name").GetComponent<TextMeshProUGUI>().text = info.name;
        MY_UI.transform.Find("HPbar").GetComponent<Image>().fillAmount = HP*1.0f / info.Max_HP;
    }
}
