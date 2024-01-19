using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public string transfer_scene;

    public GameObject UI;
    public bool transfer = false; //�̵��� üũ, �̰� Ȱ��ȭ ��, üũ X
    public Vector3 new_position;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("UI"); //UI��������, UI���� �̵� ���

        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Press 'E' to " + transfer_scene; //ȭ��ǥ �Ʒ�, canvus name���ٰ� �̵��� ��� ǥ��
    }



    // Update is called once per frame


    private void OnTriggerStay2D(Collider2D collision) //�÷��̾�� �浹��
    {
        if(collision.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && !transfer) //E�� ���Ȱ�, �̵����� �ƴҶ�
            {
                transfer = true;                    
                UI.GetComponent<UI>().fade_action(transfer_scene, new_position);  //ȭ����ȯ, UI���� ����
            } 
        }
    }
   
}
