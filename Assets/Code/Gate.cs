using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{

    [SerializeField]
    Vector2 transfer;

    [SerializeField]
    string map_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = new Vector3(collision.transform.position.x + transfer.x, collision.transform.position.y + transfer.y, 0);
            collision.GetComponent<Player>().now_map = map_name;            //∏  ≥◊¿” ≥—∞‹¡÷±‚
            GameObject.Find("UI").GetComponent<UI>().map_allert();          //∏  ∂ÁæÓ¡÷ººø‰
        }






    }
}
