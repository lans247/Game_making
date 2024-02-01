using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    public Animator animator;

    public string now_map;
    public float[] map_range = new float[4]; //-x, x, -y, x
    
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
        animator = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        move();
    }


    void move()
    {

        float x_move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y_move = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(x_move, y_move, 0);

        if(y_move > 0)
        {
            animator.SetTrigger("up");
        }
        else if (y_move < 0)
        {
            animator.SetTrigger("down");
        }
        else if (x_move > 0)
        {
            animator.SetTrigger("move_r");
        }
        else if (x_move < 0)
        {
            animator.SetTrigger("move_l");
        }
        else if (x_move == y_move && y_move == 0)
        {
            animator.SetTrigger("normal");
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        else { speed = 5.0f; }
    }
}
