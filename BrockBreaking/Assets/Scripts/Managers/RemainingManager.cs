using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Balls;

namespace Managers{
    
    public class RemainingManager : MonoBehaviour
    {
        //残機数が変化したとき
        private static ReactiveProperty<int> remainingNum =new  ReactiveProperty<int>(0);
        public static IReadOnlyReactiveProperty<int> remainingNumChenged {
            get{return remainingNum;}
        }


        private static Subject<int> remainingManagerSubject = new Subject<int>();
        public static IObservable<int> remainingDecreased{
            get{return remainingManagerSubject;}
        }
    // Start is called before the first frame update
        void Start()
        {
            //BallにSubscribeする
            Ball.BallLoss
            .Where(num => num == 0)
            .Subscribe(_ => {
                remainingNum.Value -= 1;
                remainingManagerSubject.OnNext(remainingNum.Value);
            });   
        }

    // Update is called once per frame
        void Update()
        {
        
        }

        public static void RemainingInitialize(){
            //残機を3に
            remainingNum.Value = 3;
        }

        public static int getRimainingNum(){
            return remainingNum.Value;
        }
    }
}
