using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTweenを使用するため

public class StoneFactory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] ShotObject;//発射するオブジェクト
    [SerializeField] float MaxTime;//弾の発射感覚の最大値
    [SerializeField] float MinTime;//弾の発射感覚の最小値
    [SerializeField] float span;//弾がたまるまでの間隔
    [SerializeField] float delta;//溜めている時間

    [SerializeField] int[] Direction = { 0, 30, 45, 60, 90, 105, 120, 150, 180 };
    [SerializeField] int[] Speeds = { 2, 3, 5, 8 };
    //[SerialilzeField] Stone StoneScript;
    void Start()
    {
        delta = 0;
        Move();
        Debug.Log(Speeds.Length);
        Debug.Log(Direction.Length);
        span = SetRandomSpan();//次の発射までの時間を決定する
        //InvokeRepeating("MakeStone",0.5f,1f);//0.5秒後に,MakeStoneを実行し、1秒置きに再実行し続ける
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
            MakeStone();//隕石を1つ出す
            span = SetRandomSpan();//次の発射までの時間を決定する
        }
    }

    float SetRandomSpan()
    {//弾の発生感覚を決める
        float a,b;//一時的に使用する変数
        a = Random.Range(MinTime, MaxTime);
        b = Random.Range(MinTime, MaxTime);

        //return Random.Range(MinTime, MaxTime);//そのまま返す場合はこっち
        return (a+b)/2.0f;//正規分布に従って値を返す場合はこっち
    }

    void MakeStone()
    {
        int SummonNum;
        SummonNum = Random.Range(0, 100);
        SummonNum %= ShotObject.Length;

        GameObject StoneObj = Instantiate(ShotObject[SummonNum]) as GameObject;//弾の生成
        StoneObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
        Stone StoneScript = StoneObj.GetComponent<Stone>();
        int DirectionNum;
        int r;//乱数
        r = Random.Range(0, 100);
        DirectionNum = r % Direction.Length;
        StoneScript.directionX = Direction[DirectionNum];//x軸の力を決める
        int s;//乱数
        int SpeedNum;
        s = Random.Range(0, 100);
        SpeedNum = s % Speeds.Length;
        StoneScript.speed = Speeds[SpeedNum];//隕石の速さを決める

    }

    void Move()
    {//特定の地点を周回する
       int SelectMove;
        SelectMove = Random.Range(0, 100);
        SelectMove %= 2;
        switch (SelectMove)
        {
            case 0://広い範囲で周回する
                DOTween.Sequence().Append(transform.DOLocalMove(new Vector3(-7, 4.57f, 0), 1.0f))
                .Append(transform.DOLocalMove(new Vector3(0, 4.57f, 0), 1.0f))
                .Append(transform.DOLocalMove(new Vector3(7, 4.57f, 0), 1.0f))
                .OnComplete(Move);
                break;
            case 1://狭い範囲で周回する
               DOTween.Sequence().Append(transform.DOLocalMove(new Vector3(-3, 4.57f, 0), 1.0f))
                .Append(transform.DOLocalMove(new Vector3(0, 4.57f, 0), 1.0f))
                .Append(transform.DOLocalMove(new Vector3(3, 4.57f, 0), 1.0f))
                .OnComplete(Move);
                break;

            default:
            break;

        }
    }
}
