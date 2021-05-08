using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    /*[SerializeField] Rigidbody2D rigid2d;
    [SerializeField] float walkForce = 30.0f;
    [SerializeField] float maxWalkSpeed = 2.0f;
    //[SerializeField] int key;

    [SerializeField] float speed;
    */

    [SerializeField] GameObject ShotObject;//発射するオブジェクト
    [SerializeField] float span;//弾がたまるまでの間隔
    [SerializeField] float delta;//溜めている時間

    [SerializeField] Vector3 MoveVelocity;//進むべき方向

    [SerializeField] GameObject IceWall;//子オブジェクトの壁
    [SerializeField] bool IsFire;//trueなら炎のモード、falseなら氷のモード

    [SerializeField] int BoostCount;
    [SerializeField] bool IsBoost;//trueだと倍速で移動する
    [SerializeField] float BoostSpan;
    [SerializeField] float BoostDelta;

    [SerializeField] int[] Direction = { 0, 25, -25 };//弾の発射角度

    [SerializeField] int AttackPattern;

    public float speed;//速度

    void Start()
    {
        //this.rigid2d = GetComponent<Rigidbody2D>();
        Debug.Log("nyaa");
        delta = 0;
        IsFire = true;
        IceWall = this.transform.GetChild(0).gameObject;
        IsBoost = false;
        BoostCount = 0;
        AttackPattern = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GManager.instance.IsBattle)
        {
            return;
        }
        //簡易移動制御
        if (this.transform.position.x >= 7.0f)
        {
            this.gameObject.transform.position = new Vector3(7.0f, this.transform.position.y, 0.0f);
        }
        if (this.transform.position.x <= -7.0f)
        {
            this.gameObject.transform.position = new Vector3(-7.0f, this.transform.position.y, 0.0f);
        }
        /*key = 0;

        if(Input.GetKey(KeyCode.RightArrow)){
            key = 1;
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            key = -1;
        }
        */
        //IceWall.gameObject.SetActive(!IsFire);//でもいいけど分かりづらい
        if (IsFire)
        {
            IceWall.gameObject.SetActive(false);
        }
        else
        {
            IceWall.gameObject.SetActive(true);
        }


        MoveVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        if (!IsBoost && BoostCount < 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsBoost = true;
                BoostCount++;
                BoostDelta = 0;
            }
        }
        if (IsBoost)
        {
            BoostDelta += Time.deltaTime;
            if (BoostSpan < BoostDelta)
            {
                IsBoost = false;
                BoostDelta = 0;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (IsBoost)
            {
                MoveVelocity = new Vector3(speed * 2, 0.0f, 0.0f);

            }
            else
            {
                //transform.Translate(1, 0, 0);
                MoveVelocity = new Vector3(speed, 0.0f, 0.0f);
            }

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsBoost)
            {
                MoveVelocity = new Vector3(-speed * 2, 0.0f, 0.0f);

            }
            else
            {
                //transform.Translate(1, 0, 0);
                MoveVelocity = new Vector3(-speed, 0.0f, 0.0f);
            }
        }

        /*speed = Mathf.Abs(this.rigid2d.velocity.x);
        if(speed < this.maxWalkSpeed){
            this.rigid2d.AddForce(transform.right*key*this.walkForce);
        }*/
        delta += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsFire = !IsFire;//反転
        }
        if (IsFire)
        {
            if (span < delta)
            {
                delta = 0;
                //DoubleAttack();
                Attack();
            }
        }

        Move();
    }

    void Move()
    {
        gameObject.transform.position += MoveVelocity * Time.deltaTime;
    }

    void Attack()
    {
        for (int i = 0; i < AttackPattern; i++)
        {
            GameObject FireObj = Instantiate(ShotObject) as GameObject;//弾の生成
            FireObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
            FireObj.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Direction[i]);
        }
        //Debug.Log("oaaa");
    }

    void DoubleAttack()
    {
        GameObject FireObj_Left = Instantiate(ShotObject) as GameObject;//弾の生成(左側)
        FireObj_Left.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
        FireObj_Left.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 25.0f);//角度調整

        GameObject FireObj_Right = Instantiate(ShotObject) as GameObject;//弾の生成(右側)
        FireObj_Right.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
        FireObj_Right.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -25.0f);//角度調整
    }

    public void Restart()
    {
        IsBoost = false;
        BoostCount = 0;
        AttackPattern = 1;
    }

    public void ChangeAttack(){
        AttackPattern = 3;
    }
}
