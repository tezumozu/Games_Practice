using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using Managers;

public class RemainingCounter : MonoBehaviour
{
    // Start is called before the first frame update
    private static Subject<Unit> updateRemainingSubject = new Subject<Unit>();
    public static IObservable<Unit> updateRemaining{
        get{
            return updateRemainingSubject;
        }
    }
    void Start(){
        RemainingManager.remainingNumChenged
            .Subscribe(num => {
                remainingNumChange(num);
            });
    }

    void remainingNumChange(int num){
        //counterのリセット
        updateRemainingSubject.OnNext(Unit.Default);

        //キャンバスの取得
        Canvas canvas = transform.root.gameObject.GetComponent<Canvas>();
        //プレハブの取得
        GameObject pre  = (GameObject)Resources.Load("Prefabs/Remaining");

        for(int i = 0; i < num; i++){
            GameObject clone = Instantiate(pre);
            clone.transform.SetParent(canvas.transform,false);

            //座標調整
            Vector3 pos = clone.transform.localPosition;
            pos.x = pos.x - 10*(float)i;
            clone.transform.localPosition = pos;
        } 
    }


}
