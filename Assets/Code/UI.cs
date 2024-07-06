using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UI : MonoBehaviour
{
    public GameObject fade; //fade in out을 위한 객체
    public GameObject notice; //맵 표시를 위한 객체
    
    public GameObject BOSS_UI;      //보스 UI
    public bool BOSS = false;       //보스 있는지 체크

    public GameObject talk_UI;      //talk_ui
    public bool isTalking = false;  //토킹 체크

    public GameObject player;

    public GameObject Map_UI;          //미니맵
    public bool Map_open = false;       //미니맵 체크

    public GameObject Camera;


    public List<Item_info> item;        //아이템 정보가 저장되는 리스트
    
    public GameObject inventory;        //인벤토리
    public bool inven_open = false;
    public int inven_have;              //인벤에 표시중인 갯수

    public GameObject item_image_frame;         //인벤토리에 표시되는 아이템 이미지의 프레임
    
    
    public GameObject Skill_UI;                         //스킬 UI
    public Skill_info[] equips = new Skill_info[5];     //장착 중인 스킬



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

        BOSS_UI = transform.Find("BOSS").gameObject;
        talk_UI = transform.Find("talk_ui").gameObject;
        Skill_UI = transform.Find("Skill").gameObject;
        Map_UI = transform.Find("Map").gameObject;

        Camera = GameObject.Find("Main Camera");        //카메라 가져오기

    }


    public void Update()
    {
        item = player.GetComponent<item_manager>().bag;

        if (Input.GetKeyDown(KeyCode.I))        //i눌렀을 때 인벤 열기
        {
            inven_open = !inven_open;
            inventory.SetActive(inven_open);
        }


        BOSS_UI.SetActive(BOSS);
        talk_UI.SetActive(isTalking);

        skill_manage();         //스킬 관리

        if(Map_open && Input.GetKeyDown(KeyCode.M)) {
            Map_open = false;
            minimap_close();
        }
        else if (!Map_open && Input.GetKeyDown(KeyCode.M))
        {
            Map_open = true;
            minimap_open();
        }
    }

    public void minimap_close() {
        Camera.transform.GetChild(0).GetComponent<Camera>().orthographicSize = 15;          //미니맵 카메라의 시야각 조절.

        Map_UI.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, -400);
        Map_UI.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

        Map_UI.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
    }
    public void minimap_open(){
        Camera.transform.GetChild(0).GetComponent<Camera>().orthographicSize = 60;


        Map_UI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        Map_UI.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 600);

        Map_UI.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(600, 600);
    }



    public void UI_inven_add(int n)
    {
        GameObject nw = Instantiate(item_image_frame);                                      //새로운 아이템 오브젝트를 만들어서
        nw.GetComponent<UnityEngine.UI.Image>().sprite = item[n].image;            //정보를 입력하고. 
        nw.transform.parent = inventory.transform.GetChild(0).GetChild(0);                  //그 오브젝트를 inventory에 자식으로 넣기.
    }




    public void skill_manage()
    {
        for(int i =0;i<5;i++)
        {
            if (equips[i] != null)          //착용되어있다면, 스킬 이미지 오브젝트를 활성화. 
            {
                Skill_UI.transform.GetChild(i).gameObject.SetActive(true);
                Skill_UI.transform.GetChild(i).GetComponent<Image>().sprite = equips[i].imagine;
            }
            else
            {
                Skill_UI.transform.GetChild(i).gameObject.SetActive(false);
            }
        }




    }



    public IEnumerator cool_manage(int number)         //해당하는 번호의 스킬의 쿨타임 돌리기
    {   
        equips[number].cool = false;            //쿨 시작

        int cooltime = equips[number].cooltime;
        Image cool_ui = Skill_UI.transform.GetChild(number).GetComponent<Image>();

        cool_ui.fillAmount = 0;

        while (cool_ui.fillAmount != 1)         //쿨타임 진행
        {
            cool_ui.fillAmount += 0.01f;
            yield return new WaitForSeconds(cooltime * 0.01f);
        }

        equips[number].cool = true;             //쿨 완료
        yield return null;
    }
    























    public void map_allert()        //맵 띄우기
    {
        StopCoroutine(map_allert_act());        //기존 맵 알림 끄고
        StartCoroutine(map_allert_act());       //새로운 맵 알림
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
