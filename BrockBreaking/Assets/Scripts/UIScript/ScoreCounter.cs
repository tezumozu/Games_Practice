using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UniRx;
using UnityEngine.UI;
public class ScoreCounter : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        ScoreManager.ScoreChanged
            .Subscribe(_ => changeScoreCounter(_));
    }

    private void changeScoreCounter(float data){
        //テキストの取得
        Text text = gameObject.GetComponent<Text>();

        //テキストを変更
        string score = data.ToString();
        text.text = "SCORE : " + score;

    }
}
