using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GManager instance = null;
    public bool IsBattle;//戦闘中かどうか
    public GameObject Timer;
    public Text TimerText;
    public float CountTime;//経過時間
    public int seconds;//制限時間

    [SerializeField] GameObject ScoreText;
    [SerializeField] Score ScoreM;

    [SerializeField] GameObject StartText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Timer = GameObject.Find("Canvas/TimeText");
        TimerText = Timer.GetComponent<Text>();
        ScoreText = GameObject.Find("Canvas/ScoreText");
        StartText = GameObject.Find("Canvas/StartText");
        ScoreM = ScoreText.GetComponent<Score>();
        //IsBattle = true;

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

        if (seconds >= 0)
        {
            CountTime -= Time.deltaTime;
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
        StartText.gameObject.SetActive(false);
    }

    void Finish()
    {
        IsBattle = false;
        Debug.Log("終了");
        StartText.gameObject.SetActive(true);
    }
}
