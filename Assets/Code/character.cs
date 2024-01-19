using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class character : MonoBehaviour
{
    public string Name;             //이름
    public Vector2 posi;

    public TextMeshProUGUI talk_UI;         //이야기 유아이
    public bool talking = false;            //이야기 중

    public string[] sentence  = new string[100]; //대사집
    
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
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && !talking)        //이야기 중이 아니고 상호작용 키를 누른 이후에
        {
            talking = true;         //이야기 중
            StartCoroutine("talk"); //이야기
        }
    }

    IEnumerator talk()
    {
        int Now = 0;                                        //대사할 배열
        while (sentence[Now] != string.Empty)               //할말이 없을 떄까지
        {
            talk_UI.text = sentence[Now];
            Now++;
            yield return new WaitForSeconds(3);
        }
        talk_UI.text = string.Empty;
        talking = false;                                      //이야기 종료
        yield break;
    }
 
}
