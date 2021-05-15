using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTweenを使用するため

public class ItemFactory : MonoBehaviour
{
    [SerializeField] GameObject[] ItemObject;//発射するオブジェクト
    [SerializeField] float span;//アイテムがたまるまでの間隔
    [SerializeField] float delta;//溜めている時間
    // Start is called before the first frame update
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
            MakeItem();//アイテムを1つ出す
        }
    }

    void MakeItem()
    {
        int SummonNum;
        SummonNum = Random.Range(0, 100);
        SummonNum %= ItemObject.Length;

        GameObject ItemObj = Instantiate(ItemObject[SummonNum]) as GameObject;//弾の生成
        ItemObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);//自分の場所に出す
    }

    void Move()
    {//特定の地点を周回する
        DOTween.Sequence().Append(transform.DOLocalMove(new Vector3(-7, 4.57f, 0), 1.0f))
        .Append(transform.DOLocalMove(new Vector3(0, 4.57f, 0), 1.0f))
        .Append(transform.DOLocalMove(new Vector3(7, 4.57f, 0), 1.0f))
        .OnComplete(Move);
    }
}
