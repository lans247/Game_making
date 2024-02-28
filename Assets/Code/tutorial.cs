using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    public string Name;             //�̸�
    public Vector2 posi;

    public TextMeshProUGUI talk_UI;         //�̾߱� ������
    public bool talking = false;            //�̾߱� ��

    public string sentence;

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
        yield return StartCoroutine(talk_sentence("�ݰ���. �ź�. �̹����� �⺻ ���ۿ� ���ؼ� �˷��ְڴ�."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("WASDȤ�� ȭ��ǥ�� ���ؼ� ĳ���͸� ������ �� �ִ�."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("�غ�����."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitForSeconds(3f);

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("���ߴ�."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("������ �����̴�. ���콺 ���� ��ư�� ������ ������ ������ ������ �� �ִ�."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("�տ� �ִ� ����ƺ� Ÿ���غ�����."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;

        yield return new WaitUntil(() => Scarecrow.GetComponent<Enemy_normal>().HP != 100000);

        Player.GetComponent<Player>().can_move = false;
        GameObject.Find("UI").GetComponent<UI>().isTalking = true;
        yield return StartCoroutine(talk_sentence("�׷��� �ϴ� �Ŵ�."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("������ ����. ��Ŭ���� ���ؼ� ����� �� ����."));
        yield return new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(talk_sentence("Ÿ�̹��� ������ ���� ������ ƨ�ܳ��� ������ �ݰ��� ���� ���� �ִ�."));
        yield return new WaitUntil(() => Input.anyKey);
        GameObject.Find("UI").GetComponent<UI>().isTalking = false;
        Player.GetComponent<Player>().can_move = true;
























    }


    IEnumerator talk_sentence(string c)
    {
        talk_UI.text = "";
        for (int i = 0; i < c.Length; i++)
        {
            talk_UI.text += c[i].ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
