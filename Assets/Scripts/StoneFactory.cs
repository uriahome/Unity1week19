using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTweenを使用するため

public class StoneFactory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] ShotObject;//発射するオブジェクト
    [SerializeField] float span;//弾がたまるまでの間隔
    [SerializeField] float delta;//溜めている時間

    [SerializeField] int[] Direction = { 0, 30, 45, 60, 90, 105, 120, 150, 180 };
    //[SerialilzeField] Stone StoneScript;
    void Start()
    {
        delta = 0;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GManager.instance.IsBattle)
        {
            return;
        }
        delta += Time.deltaTime;
        if (span < delta)
        {
            delta = 0;
            MakeStone();
        }
    }

    void MakeStone()
    {
        int SummonNum;
        SummonNum = Random.Range(0, 100);
        SummonNum %= 3;

        GameObject StoneObj = Instantiate(ShotObject[SummonNum]) as GameObject;//弾の生成
        StoneObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
        Stone StoneScript = StoneObj.GetComponent<Stone>();
        int DirectionNum;
        int r;
        r = Random.Range(0, 100);
        DirectionNum = r % 9;
        StoneScript.directionX = Direction[DirectionNum];

    }

    void Move()
    {
        DOTween.Sequence().Append(transform.DOLocalMove(new Vector3(7, 4.57f, 0), 1.0f))
        .Append(transform.DOLocalMove(new Vector3(0, 4.57f, 0), 1.0f))
        .Append(transform.DOLocalMove(new Vector3(-7, 4.57f, 0), 1.0f))
        .OnComplete(Move);
    }
}
