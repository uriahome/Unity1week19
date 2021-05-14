using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Vector2 direction;//移動する方向
    public float speed;

    public int ItemNum;//アイテムとしての番号
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        Move();//移動する力を一度だけ与える
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {//真下に落ちていく
        direction = new Vector2(0,-1.0f);
        rb2d.velocity = direction * speed;
    }
}
