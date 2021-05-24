using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb2d;

    public float directionX;//x軸に対しての移動する力
    [SerializeField] float directionY;///y軸に対しての移動する力

    [SerializeField] Vector2 direction;//移動する方向

    public float speed;
    [SerializeField] bool IsFire;//炎の隕石かどうか

    [SerializeField] GameObject ScoreText;
    [SerializeField] Score ScoreM;
    void Start()
    {
        ScoreText = GameObject.Find("Canvas/ScoreText");
        ScoreM = ScoreText.GetComponent<Score>();
        rb2d = this.GetComponent<Rigidbody2D>();
        Move();//移動する力を一度だけ与える
    }

    // Update is called once per frame
    void Update()
    {
        if (!GManager.instance.IsBattle)
        {//戦闘中以外は削除
            Destroy(this.gameObject);
        }
    }
    void Move()
    {//指定した角度で力を与える
        direction = new Vector2(Mathf.Cos(directionX), Mathf.Sin(directionY)).normalized;
        rb2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (IsFire)
        {
            //Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "IceWall")
            {
                Debug.Log("反射!");
                //上にあげる
                directionY += 180.0f;
                directionY = directionY % 360.0f;
                Move();
            }
        }
        else*/
        //{//同じ処理ではある。プレイヤーの玉か、燃えている隕石に当たると消える
            if (collision.gameObject.tag == "Fire")
            {
                ScoreM.ChangeScore(100);
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.tag == "FireBall")
            {
                ScoreM.ChangeScore(100);
                Destroy(this.gameObject);
            }
        //}

        if (collision.gameObject.tag == "StoneWall")
        {//反射処理
            directionX += 180.0f;
            directionX = directionX % 360.0f;
            Move();
        }
        else if (collision.gameObject.tag == "Earth")
        {//画面下の地球に当たるとスコアが減る
            Debug.Log("Earth Hit!!");
            ScoreM.ChangeScore(-50);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {//反射の処理はプレイヤーに反動がいかないようにTriggerで判定する
        if (collision.gameObject.tag == "IceWall")
        {
            Debug.Log("反射!");
            //上にあげる
            directionY += 180.0f;
            directionY = directionY % 360.0f;
            Move();
        }
    }
}
