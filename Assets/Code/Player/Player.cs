using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    public Animator animator;

    public string now_map;
    public float[] map_range = new float[4]; //-x, x, -y, x

    public int rolldelay;
    public bool rolling = false;


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
        rolldelay = 4;
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

        if(Input.GetKey(KeyCode.Space) && !rolling)
        {
            rolling = true;
            StartCoroutine(roll_delay());
            roll(x_move, y_move);
        }
    }



    void roll(float x, float y)
    {
        if (y > 0)
        {
            animator.SetTrigger("uproll");
        }
        else
        {
            animator.SetTrigger("downroll");
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(x * 50, y * 50), ForceMode2D.Impulse);

    }

    IEnumerator roll_delay()
    {
        yield return new WaitForSeconds(rolldelay);
        rolling = false;
    }
}
