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
        talk_UI = GameObject.Find("UI").transform.Find("talk_ui").Find("talk").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && !talking)        //�̾߱� ���� �ƴϰ� ��ȣ�ۿ� Ű�� ���� ���Ŀ�
        {
            collision.GetComponent<Player>().can_move = false;      //����
            talking = true;         //�̾߱� ��
            StartCoroutine(talk()); //�̾߱�
            collision.GetComponent<Player>().can_move = true;      //����
        }
    }

    IEnumerator talk()
    {
        int Now = 0;                                        //����� �迭
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;      //UI��� �ϼ�
        GameObject.Find("UI").transform.Find("talk_ui").Find("name").GetComponent<TextMeshProUGUI>().text = Name;


        while (sentence[Now] != string.Empty)               //�Ҹ��� ���� ������
        {
            yield return StartCoroutine(talk_sentence(sentence[Now]));
            yield return new WaitUntil(() => Input.anyKey);
            Now++;
        }
        talk_UI.text = string.Empty;
        talking = false;                                      //�̾߱� ����
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;      //UI����


        yield break;
    }

    IEnumerator talk_sentence(string c)
    {
        talk_UI.text = "";
        for(int i = 0; i < c.Length; i++)
        {
            talk_UI.text += c[i].ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
 

    






}
