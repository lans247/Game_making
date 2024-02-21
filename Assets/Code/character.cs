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
        talk_UI = GameObject.Find("UI").transform.Find("talk_ui").Find("talk").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKey(KeyCode.E) && !talking)        //이야기 중이 아니고 상호작용 키를 누른 이후에
        {
            collision.GetComponent<Player>().can_move = false;      //정지
            talking = true;         //이야기 중
            StartCoroutine(talk()); //이야기
            collision.GetComponent<Player>().can_move = true;      //정지
        }
    }

    IEnumerator talk()
    {
        int Now = 0;                                        //대사할 배열
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;      //UI출력 하셈
        GameObject.Find("UI").transform.Find("talk_ui").Find("name").GetComponent<TextMeshProUGUI>().text = Name;


        while (sentence[Now] != string.Empty)               //할말이 없을 떄까지
        {
            yield return StartCoroutine(talk_sentence(sentence[Now]));
            yield return new WaitUntil(() => Input.anyKey);
            Now++;
        }
        talk_UI.text = string.Empty;
        talking = false;                                      //이야기 종료
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;      //UI끄셈


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
