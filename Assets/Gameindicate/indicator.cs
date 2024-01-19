using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class indicator : MonoBehaviour
{
    public int content;
    public TextMeshPro _text;

    public string type;

    private void Start()
    {
        _text = GetComponent<TextMeshPro>(); 
        _text.text = content.ToString();
        StartCoroutine("disappear");                //시간 지나면 사라짐

        if (type == "damage") 
        {
            _text.color = Color.red;
        }

        if(type == "recover")
        {
            _text.color = Color.green;
        }
    }
    void FixedUpdate()
    {
        

    }

    IEnumerator disappear()
    {
        for(int i = 0; i<10;i++)
        {
            yield return new WaitForSeconds(0.1f);
            transform.Translate(0, 0.1f, 0f);       //조금씩 올라감
            _text.alpha -= 0.1f;                    //흐려지며 사라짐
        }
       
        Destroy(this.gameObject);

        yield return null;
    }
}
