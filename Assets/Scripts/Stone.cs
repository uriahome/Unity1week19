using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb2d;

    public float directionX;
    [SerializeField] float directionY;

    [SerializeField] Vector2 direction;//= new Vector2(1, 1).normalized;

    public float speed;
    [SerializeField] bool IsFire;//炎の隕石かどうか

    [SerializeField] GameObject ScoreText;
    [SerializeField] Score ScoreM;
    void Start()
    {
        ScoreText = GameObject.Find("Canvas/ScoreText");
        ScoreM = ScoreText.GetComponent<Score>();
        rb2d = this.GetComponent<Rigidbody2D>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GManager.instance.IsBattle){
            Destroy(this.gameObject);
        }
    }
    void Move()
    {
        direction = new Vector2(Mathf.Cos(directionX), Mathf.Sin(directionY)).normalized;
        rb2d.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsFire)
        {
            //Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "IceWall")
            {
                //上にあげる
                directionY += 180.0f;
                directionY = directionY % 360.0f;
                Move();
            }
        }
        else
        {
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
        }

        if (collision.gameObject.tag == "StoneWall")
        {
            directionX += 180.0f;
            directionX = directionX % 360.0f;
            Move();
        }else if(collision.gameObject.tag =="Earth"){
            Debug.Log("Earth Hit!!");
            ScoreM.ChangeScore(-50);
            Destroy(this.gameObject);
        }
    }
}
