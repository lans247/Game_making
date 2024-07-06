using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Player : MonoBehaviour
{

    public float speed = 5.0f;

    public Animator animator;       //플레이어 애니메이션

    public string now_map;
    //public float[] map_range = new float[4]; //-x, x, -y, x 카메라 범위

    public int rolldelay;               //구르기 딜레이
    public bool rolling = false;        //구를 수 있는 상태

    public bool can_move = true;        //움직일 수 있는 상태 인가?


    public GameObject Player_UI;        //캐릭터 주변에 표시될 UI
    public Image roll_delay_image;      //구르기 딜레이를 표시할 인디케이터 이미지

    private void Awake()                //이동시 2개 이상이면 제거
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
        animator = GetComponent<Animator>();        //움직일 떄 애니메이션

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


          
        if (Input.GetKey(KeyCode.LeftShift)) speed = 10.0f;     //달리기.
        else speed = 5.0f; 

        if(Input.GetKey(KeyCode.Space) && !rolling)     //구르는 중이 아닐때 구르기 호출
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
