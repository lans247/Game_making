using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;


    
    private void Awake()//�̵��� 2�� �̻��̸� ����
    {
        var obj = FindObjectsOfType<Player>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x_move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y_move = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(x_move, y_move, 0);



        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        else { speed = 5.0f; }
    }

}
