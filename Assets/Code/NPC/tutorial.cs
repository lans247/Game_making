using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public string Name;             //이름

    public TextMeshProUGUI talk_UI;         //이야기 유아이
    public bool talking = false;            //이야기 중

    public string sentence;                 //읽을 문장.

    public GameObject Player;
    public GameObject Scarecrow;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Scarecrow = GameObject.Find("Scarecrow");
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
        talk_UI = GameObject.Find("UI").transform.Find("talk_ui").Find("talk").GetComponent<TextMeshProUGUI>();
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("반갑다. 신병. 이번에는 기본 조작에 대해서 알려주겠다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("WASD혹은 화살표를 통해서 캐릭터를 움직일 수 있다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("해보도록."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitForSeconds(3f);

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("잘했다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("다음은 공격이다. 마우스 왼쪽 버튼을 누르는 것으로 공격을 진행할 수 있다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("앞에 있는 허수아비를 타격해보도록."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitUntil(() => Scarecrow.GetComponent<Enemy_normal>().HP != 100000);

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("그렇게 하는 거다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("다음은 방어다. 우클릭을 통해서 사용할 수 있지."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("타이밍이 좋으면 적의 공격을 튕겨내고 강력한 반격을 행할 수도 있다."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitUntil(() => Player.GetComponent<Player_combat>().shield.activeSelf);

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("그래. 스킬을 사용해 보도록"));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("키패드를 클릭하여 사용할 수 있다."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("[1]을 누르면 된다. 실시."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitUntil(() => Player.GetComponent<Player_combat>().equips[0].cool != true);      //스킬이 사용해서 사용 불가능 상태일때

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("굿"));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("이제 가서 슬라임을 잡아와라."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("5마리 정도면 적당하겠지."));
        yield return new WaitUntil(() => Input.anyKey);

        this.gameObject.GetComponent<Quest_giver>().Give();
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

    }


    IEnumerator talk_sentence(string c)
    {
        talk_UI.text = "";
        for (int i = 0; i < c.Length; i++)
        {
            talk_UI.text += c[i].ToString();
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
