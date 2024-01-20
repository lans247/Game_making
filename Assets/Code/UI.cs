using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public GameObject fade; //fade in out을 위한 객체
    public GameObject notice; //맵 표시를 위한 객체



    public GameObject player;

    public GameObject item_image_frame;

    public List<Item_info> item;


    public GameObject inventory;        //인벤토리
    public bool inven_open = false;
    public int inven_have;              //인벤에 표시중인 갯수

    private void Awake() //이동시 2개 이상이면 제거
    {
        var obj = FindObjectsOfType<UI>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    
    private void Start()
    {
        player = GameObject.FindWithTag("Player"); //플레이어 가져오기
        inventory = transform.Find("Inventory").gameObject;     //인벤가져오기

        fade = transform.GetChild(0).gameObject;               //fade
        notice = transform.GetChild(1).gameObject;             //map표시 및, 알림 설정
        item = player.GetComponent<item_manager>().bag;

        
    }


    public void FixedUpdate()
    {
        item = player.GetComponent<item_manager>().bag;

        if (Input.GetKey(KeyCode.I))        //i눌렀을 때 인벤 열기
        {
            if (!inven_open)
            {
                inventory.SetActive(true);
                inven_open = true;
            }
            else
            {
                inventory.SetActive(false);
                inven_open = false;
            }
        }

        if (item.Count > inven_have) //만약에 관측되는 아이템 갯수가 표시 중인 것보다 많다면
        {
            bag_manage();
        }
    }





    public void bag_manage()
    {
        GameObject nw = Instantiate(item_image_frame);
        nw.GetComponent<UnityEngine.UI.Image>().sprite = item[inven_have].image;
        nw.transform.parent = inventory.transform.GetChild(0).GetChild(0);
        inven_have++;

    }





    public void map_allert()        //맵 띄우기
    {
        StopCoroutine("map_allert_act");        //기존 맵 알림 끄고
        StartCoroutine("map_allert_act");       //새로운 맵 알림
    }

    public IEnumerator map_allert_act()     //멥 알림
    {
        
        yield return new WaitForSeconds(1f);
        notice.GetComponent<TextMeshProUGUI>().text = player.GetComponent<Player>().now_map;

        float alpha = 0f;
        while(alpha < 1f)
        {
            alpha += 0.1f;
            notice.GetComponent<TextMeshProUGUI>().alpha = alpha;
            yield return new WaitForSeconds(0.05f);
        }
        while(alpha > 0f)
        {
            alpha -= 0.1f;
            notice.GetComponent<TextMeshProUGUI>().alpha = alpha;
            yield return new WaitForSeconds(0.05f);
        }



        yield break;
    }









    public void fade_action(string transfer_map, Vector3 position)      //코루틴 호출을 위한 함수
    {
        StartCoroutine(fade_start(transfer_map, position));
    }

    public IEnumerator fade_start(string transfer_map, Vector3 position) //fade와 동시에 씬 이동
    {
        float alpha = 0f;
        while (alpha < 1.0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(transfer_map);           //씬 이동
        player.transform.position = position;           //입력된 곳으로 플레이어 이동
        while (alpha > 0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
        yield break;
    }
    
}
