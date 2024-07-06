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
    public GameObject fade; //fade in out�� ���� ��ü
    public GameObject notice; //�� ǥ�ø� ���� ��ü
    
    public GameObject BOSS_UI;      //���� UI
    public bool BOSS = false;       //���� �ִ��� üũ

    public GameObject talk_UI;      //talk_ui
    public bool isTalking = false;  //��ŷ üũ

    public GameObject player;

    public GameObject Map_UI;          //�̴ϸ�
    public bool Map_open = false;       //�̴ϸ� üũ

    public GameObject Camera;


    public List<Item_info> item;        //������ ������ ����Ǵ� ����Ʈ
    
    public GameObject inventory;        //�κ��丮
    public bool inven_open = false;
    public int inven_have;              //�κ��� ǥ������ ����

    public GameObject item_image_frame;         //�κ��丮�� ǥ�õǴ� ������ �̹����� ������
    
    
    public GameObject Skill_UI;                         //��ų UI
    public Skill_info[] equips = new Skill_info[5];     //���� ���� ��ų



    private void Awake() //�̵��� 2�� �̻��̸� ����
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
        player = GameObject.FindWithTag("Player"); //�÷��̾� ��������
        inventory = transform.Find("Inventory").gameObject;     //�κ���������

        fade = transform.GetChild(0).gameObject;               //fade
        notice = transform.GetChild(1).gameObject;             //mapǥ�� ��, �˸� ����
        item = player.GetComponent<item_manager>().bag;

        BOSS_UI = transform.Find("BOSS").gameObject;
        talk_UI = transform.Find("talk_ui").gameObject;
        Skill_UI = transform.Find("Skill").gameObject;
        Map_UI = transform.Find("Map").gameObject;

        Camera = GameObject.Find("Main Camera");        //ī�޶� ��������

    }


    public void Update()
    {
        item = player.GetComponent<item_manager>().bag;

        if (Input.GetKeyDown(KeyCode.I))        //i������ �� �κ� ����
        {
            inven_open = !inven_open;
            inventory.SetActive(inven_open);
        }


        BOSS_UI.SetActive(BOSS);
        talk_UI.SetActive(isTalking);

        skill_manage();         //��ų ����

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
        Camera.transform.GetChild(0).GetComponent<Camera>().orthographicSize = 15;          //�̴ϸ� ī�޶��� �þ߰� ����.

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
        GameObject nw = Instantiate(item_image_frame);                                      //���ο� ������ ������Ʈ�� ����
        nw.GetComponent<UnityEngine.UI.Image>().sprite = item[n].image;            //������ �Է��ϰ�. 
        nw.transform.parent = inventory.transform.GetChild(0).GetChild(0);                  //�� ������Ʈ�� inventory�� �ڽ����� �ֱ�.
    }




    public void skill_manage()
    {
        for(int i =0;i<5;i++)
        {
            if (equips[i] != null)          //����Ǿ��ִٸ�, ��ų �̹��� ������Ʈ�� Ȱ��ȭ. 
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



    public IEnumerator cool_manage(int number)         //�ش��ϴ� ��ȣ�� ��ų�� ��Ÿ�� ������
    {   
        equips[number].cool = false;            //�� ����

        int cooltime = equips[number].cooltime;
        Image cool_ui = Skill_UI.transform.GetChild(number).GetComponent<Image>();

        cool_ui.fillAmount = 0;

        while (cool_ui.fillAmount != 1)         //��Ÿ�� ����
        {
            cool_ui.fillAmount += 0.01f;
            yield return new WaitForSeconds(cooltime * 0.01f);
        }

        equips[number].cool = true;             //�� �Ϸ�
        yield return null;
    }
    























    public void map_allert()        //�� ����
    {
        StopCoroutine(map_allert_act());        //���� �� �˸� ����
        StartCoroutine(map_allert_act());       //���ο� �� �˸�
    }

    public IEnumerator map_allert_act()     //�� �˸�
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

    public void fade_action(string transfer_map, Vector3 position)      //�ڷ�ƾ ȣ���� ���� �Լ�
    {
        StartCoroutine(fade_start(transfer_map, position));
    }

    public IEnumerator fade_start(string transfer_map, Vector3 position) //fade�� ���ÿ� �� �̵�
    {
        float alpha = 0f;
        while (alpha < 1.0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(transfer_map);           //�� �̵�
        player.transform.position = position;           //�Էµ� ������ �÷��̾� �̵�
        while (alpha > 0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
        yield break;
    }
    
}
