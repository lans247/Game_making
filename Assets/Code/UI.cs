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
    public GameObject fade; //fade in out�� ���� ��ü
    public GameObject notice; //ǥ�ø� ���� ��ü



    public GameObject player;

    public GameObject item_image_frame;

    public List<Item_info> item;


    public GameObject inventory;        //�κ��丮
    public bool inven_open = false;
    public int inven_have;              //�κ��� ǥ������ ����

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

        
    }


    public void FixedUpdate()
    {
        item = player.GetComponent<item_manager>().bag;

        if (Input.GetKey(KeyCode.I))        //i������ �� �κ� ����
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

        if (item.Count > inven_have) //���࿡ �����Ǵ� ������ ������ ǥ�� ���� �ͺ��� ���ٸ�
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

















    public void fade_action(string transfer_map, Vector3 position)      //�ڷ�ƾ ȣ���� ���� �Լ�
    {
        StartCoroutine(fade_start(transfer_map, position));
    }

    public IEnumerator fade_start(string transfer_map, Vector3 position) //fade�� ���ÿ� �� �̵�
    {
        notice.GetComponent<TextMeshProUGUI>().text = transfer_map;         //ȭ�鿡 �̵� �� ���� �߱�

        float alpha = 0f;
        while (alpha < 1.0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(transfer_map);           //�� �̵�
        player.transform.position = position;           //�Էµ� ������ �÷��̾� �̵�

        notice.GetComponent<TextMeshProUGUI>().text = string.Empty;         //�̵��� �� ���� ����
        
        while (alpha > 0f)
        {
            fade.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, alpha);
            alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        
        yield break;
    }
    
}
