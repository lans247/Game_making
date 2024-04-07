using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public string transfer_scene;       //이동 신 이름
    public map_info map;

    public GameObject UI;
    public bool transfer = false; //이동중 체크, 이거 활성화 중, 체크 X


    public Vector3 new_position;
    
    void Start()
    {
        UI = GameObject.Find("UI"); //UI가져오기, UI에서 이동 담당

        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'E' to " + transfer_scene; //화살표 아래, canvus name에다가 이동할 장소 표시
    }



    // Update is called once per frame


    private void OnTriggerStay2D(Collider2D collision) //플레이어와 충돌시
    {
        if(collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && !transfer) //E가 눌렸고, 이동중이 아닐때
            {
                transfer = true;
                collision.GetComponent<Player>().now_map = map.map_name;        //맵 이름 입력
                //collision.GetComponent<Player>().map_range = map.map_range;     //맵 범위
                UI.GetComponent<UI>().map_allert();                                 //맵 이름 띄우기
                UI.GetComponent<UI>().fade_action(transfer_scene, new_position);  //화면전환, UI에서 시행
            } 
        }
    }
   
}
