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
    
    private void Awake()//이동시 2개 이상이면 제거
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
        animator.SetInteger("move_how", 0);

        float x_move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y_move = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(x_move, y_move, 0);

        if(y_move > 0)
        {
            animator.SetInteger("move_how", 1);
        }
        if(x_move > 0)
        {
            animator.SetInteger("move_how", 2);
        }
        if (y_move < 0)
        {
            animator.SetInteger("move_how", 3);
        }



        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10.0f;
        }
        else { speed = 5.0f; }
    }
}
