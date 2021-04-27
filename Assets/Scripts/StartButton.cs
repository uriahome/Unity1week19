using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.3f,1.0f).SetEase(Ease.OutElastic).SetLoops(-1,LoopType.Restart);//拡大縮小を繰り返す
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        StartCoroutine("LoadScene");
    }
    IEnumerator LoadScene(){
        Debug.Log("ロード中");
        yield return SceneManager.LoadSceneAsync(1);
    }
}
