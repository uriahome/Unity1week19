using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;//DOTweenを使用するため

public class GManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GManager instance = null;
    public bool IsBattle;//戦闘中かどうか
    public GameObject Timer;
    public Text TimerText;
    public float CountTime;//経過時間
    public int seconds;//制限時間
    public bool CheckTime;//切り替えのチェック

    [SerializeField] GameObject ScoreText;
    [SerializeField] Score ScoreM;

    [SerializeField] GameObject StartTextObj;
    [SerializeField] Text StartText;
    [SerializeField] GameObject PlayerObj;
    [SerializeField] Player PlayerM;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Timer = GameObject.Find("Canvas/TimeText");
        TimerText = Timer.GetComponent<Text>();
        ScoreText = GameObject.Find("Canvas/ScoreText");
        StartTextObj = GameObject.Find("Canvas/StartText");
        StartText = StartTextObj.GetComponent<Text>();
        ScoreM = ScoreText.GetComponent<Score>();
        PlayerObj = GameObject.Find("Player");
        PlayerM = PlayerObj.GetComponent<Player>();
        //IsBattle = true;
        FadeTextUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsBattle)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameStart();
            }
        }

        if(Input.GetKeyDown(KeyCode.A)){
            CountTime = 2;
        }

        if (seconds >= 0)
        {
            CountTime -= Time.deltaTime;
             if(CountTime  <= 15.0f){
                if(!CheckTime){
                    CheckTime = true;
                    PlayerM.ChangeAttack();
                    //制限時間が短くなると攻撃パターンが変化
                }
            }
        }
        else if (seconds < 0)
        {
            CountTime = -1;
            if (IsBattle)
            {
                //IsBattle = false;
                Finish();
            }
        }
        seconds = (int)CountTime;
        TimerText.text = "Time:" + seconds.ToString();
    }

    void GameStart()
    {
        IsBattle = true;
        CountTime = 30;
        seconds = (int)CountTime;
        TimerText.text = "Time:" + seconds.ToString();
        ScoreM.ScoreRefresh();
        StartTextObj.gameObject.SetActive(false);
        PlayerM.Restart();
        CheckTime = false;
    }

    void Finish()
    {
        IsBattle = false;
        Debug.Log("終了");
        StartTextObj.gameObject.SetActive(true);
        ScoreM.GameFinish();//ランキングの処理
    }

    public void AddTime(){//制限時間を2秒追加
        CountTime +=2.0f;
    }


    void FadeTextUI(){
        StartText.DOFade(0.1f, 1.0f).SetLoops(-1, LoopType.Yoyo);;
    }
}
