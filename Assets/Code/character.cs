using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class character : MonoBehaviour
{
    public string Name;             //�̸�
    public Vector2 posi;

    public TextMeshProUGUI talk_UI;         //�̾߱� ������
    public bool talking = false;            //�̾߱� ��

    public string[] sentence  = new string[100]; //�����
    
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
        talk_UI = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();

        talk_UI.text = "'E' Conversation";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {   
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && !talking)        //�̾߱� ���� �ƴϰ� ��ȣ�ۿ� Ű�� ���� ���Ŀ�
        {
            talking = true;         //�̾߱� ��
            StartCoroutine("talk"); //�̾߱�
        }
    }

    IEnumerator talk()
    {
        int Now = 0;                                        //����� �迭
        while (sentence[Now] != string.Empty)               //�Ҹ��� ���� ������
        {
            talk_UI.text = sentence[Now];
            Now++;
            yield return new WaitForSeconds(3);
        }
        talk_UI.text = string.Empty;
        talking = false;                                      //�̾߱� ����
        yield break;
    }
 
}
