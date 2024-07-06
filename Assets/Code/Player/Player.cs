using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;

    public Animator animator;       //�÷��̾� �ִϸ��̼�

    public string now_map;
    //public float[] map_range = new float[4]; //-x, x, -y, x ī�޶� ����

    public int rolldelay;               //������ ������
    public bool rolling = false;        //���� �� �ִ� ����

    public bool can_move = true;        //������ �� �ִ� ���� �ΰ�?


    public GameObject Player_UI;        //ĳ���� �ֺ��� ǥ�õ� UI
    public Image roll_delay_image;      //������ �����̸� ǥ���� �ε������� �̹���

    private void Awake()                //�̵��� 2�� �̻��̸� ����
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
        animator = GetComponent<Animator>();        //������ �� �ִϸ��̼�

        Player_UI = transform.Find("indicate").gameObject;
        roll_delay_image = Player_UI.transform.Find("roll_delay").GetComponent<Image>();

    }

    
    void FixedUpdate()
    {
        if (can_move)
        {
            move();
        }
    }


    
    void move()
    {
        float x_move = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float y_move = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(x_move, y_move, 0);

        if(y_move > 0) animator.SetTrigger("up");
        else if (y_move < 0) animator.SetTrigger("down");
        else if (x_move > 0) animator.SetTrigger("move_r");
        else if (x_move < 0) animator.SetTrigger("move_l");
        else if (x_move == y_move && y_move == 0) animator.SetTrigger("normal");


          
        if (Input.GetKey(KeyCode.LeftShift)) speed = 10.0f;     //�޸���.
        else speed = 5.0f; 

        if(Input.GetKey(KeyCode.Space) && !rolling)     //������ ���� �ƴҶ� ������ ȣ��
        {
            rolling = true;
            StartCoroutine(roll_delay());
            roll(x_move, y_move);
        }
    }




    void roll(float x, float y)
    {
        if (y > 0)  animator.SetTrigger("uproll");
        else  animator.SetTrigger("downroll");

        GetComponent<Rigidbody2D>().AddForce(new Vector2(x * 50, y * 50), ForceMode2D.Impulse);

    }

    IEnumerator roll_delay()
    {
        roll_delay_image.fillAmount = 0;

        while(roll_delay_image.fillAmount < 1)
        {
            roll_delay_image.fillAmount += 0.01f;
            yield return new WaitForSeconds(rolldelay * 0.01f);

        }

        roll_delay_image.fillAmount = 0;
        rolling = false;
    }
}
