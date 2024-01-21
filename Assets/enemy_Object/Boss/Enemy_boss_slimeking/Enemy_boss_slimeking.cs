using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_boss_slimeking : MonoBehaviour
{
    public Enemy_info info; //불러올 에네미 정보, 불변


    public int HP;          //변하는 정보
    public int MP;
    
    public float speed;
    public float attack_speed;

    public GameObject Player;
    public GameObject UI;

    public GameObject MY_UI;        //플레이어 유아이에 표시될 보스 정보

    public bool founded = false;       //플레이어 찾았는지

    [SerializeField]
    string now_map;     //현재 맵 이름

    

    private void Start()
    {
        HP = info.Max_HP;
        MP = info.Max_MP;
        speed = 2;
        attack_speed = info.attack_speed;       //2가 최고 공속

        Player = GameObject.FindWithTag("Player");
        UI = GameObject.FindWithTag("UI");

        transform.GetChild(1).Find("name").GetComponent<TextMeshProUGUI>().text = info.Name;     //이름 표시
        GetComponent<SpriteRenderer>().sprite = info.Image;
    }

    public void FixedUpdate()
    {
        if (now_map == Player.GetComponent<Player>().now_map && !founded)        //같은 맵에 있으면
        {
            UI.GetComponent<UI>().BOSS = true;      //보스전 시작
            founded = true;
            MY_UI = UI.GetComponent<UI>().BOSS_UI;      //유아이 가져오기
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
