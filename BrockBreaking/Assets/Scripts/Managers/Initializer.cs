using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

namespace Managers
{
    public class Initializer : MonoBehaviour
    {
        private static Subject<Unit> InitializeSubject = new Subject<Unit>();
        public static IObservable<Unit> InitializeFinished {
            get { return InitializeSubject;}
        }

        void Start()
        {
            PhaseManager.phaseChanged
                .Where(x => x == Phase.INITIALIZE)
                .Subscribe(_ => Initialize());
        }

        static IEnumerator rap(Action a,IObserver<Unit> observer){
            a();
            yield return null;
            observer.OnCompleted();
        }

        private void Initialize(){
            Observable
            .WhenAll(
                //ステージの初期化
                Observable.FromCoroutine<Unit>(y => rap(() => StageManager.stageInitialize(),y)),
                //スコアの初期化
                Observable.FromCoroutine<Unit>(y => rap(() => ScoreManager.scoreInitialize() ,y)),
                //残機の初期化
                Observable.FromCoroutine<Unit>(y =>rap(() => RemainingManager.RemainingInitialize() ,y))
            )
            .Subscribe(y => InitializeSubject.OnNext(Unit.Default));            
        }

    }
}