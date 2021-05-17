using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;//DOTweenを使用するため
using UnityEngine.UI;//UIをいじるため

public class Score : MonoBehaviour
{
    public int ScorePoint;//点数
    Text TargetText;
    public AudioSource audio;
    public AudioClip sound;

    public int[] RankingScore;
    // Start is called before the first frame update
    void Start()
    {
        ScorePoint = 0;
        TargetText = gameObject.GetComponent<Text>();
        DOTween.To(
                //開始の値
                () => 0,
                //更新処理
                x => TargetText.text = $"Score:{x:N0}",
                //完了の値
                ScorePoint,
                //トゥイーンの時間(秒)
                1.0f
            );//通常版の文法を使用することで文章内の一部をトゥイーンさせることが可能

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScoreRefresh()
    {
        ScorePoint = 0;
        DOTween.To(
        //開始の値
        () => 0,
        //更新処理
        x => TargetText.text = $"Score:{x:N0}",
        //完了の値
        ScorePoint,
        //トゥイーンの時間(秒)
        1.0f
    );//通常版の文法を使用することで文章内の一部をトゥイーンさせることが可能
    }
    public void ChangeScore(int AddPoint)
    {
        if (!GManager.instance.IsBattle)
        {
            return;//戦闘中以外は計算しない
        }
        audio.PlayOneShot(sound);//効果音を再生
        int StartScore = ScorePoint;
        ScorePoint += AddPoint;
        if (ScorePoint <= 0)
        {//スコアがマイナスにならないようにする
            ScorePoint = 0;
        }
        DOTween.To(
                //開始の値
                () => StartScore,
                //更新処理
                x => TargetText.text = $"Score:{x:N0}",
                //完了の値
                ScorePoint,
                //トゥイーンの時間(秒)
                1.0f
            );//通常版の文法を使用することで文章内の一部をトゥイーンさせることが可能
    }

    public void GameFinish()
    {//スコアランキングの変動があるか判定
        int i;
        for (i = 0; i < RankingScore.Length; i++)
        {
            if(RankingScore[i] < ScorePoint){
                ChangeRanking(i);
                break;
            }
        }
    }

    void ChangeRanking(int num)
    {//ランキングの更新を行う
    //下の順位から更新していく
        int j;
        for(j=RankingScore.Length-1;j>num;j--){
            RankingScore[j] = RankingScore[j-1];
        }
        RankingScore[num] = ScorePoint;
    }
}
