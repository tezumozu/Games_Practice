using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Managers{
    //ゲームの状態を表すフェーズ
    public enum Phase{
        //初期化
        INITIALIZE,
        //ゲーム開始までの待ち状態
        WAIT,
        //ゲーム中
        PLAY,
        //次の面へすすむ
        NEXT,
        //ゲーム終了
        END,
    }


    public class PhaseManager : MonoBehaviour{
    
        private static ReactiveProperty<Phase> currentPhase = new ReactiveProperty<Phase>();

        public static IReadOnlyReactiveProperty<Phase> phaseChanged{
            get { return currentPhase; }
        } //外部はSubscribeのみ みえるようにする

        void Start()
        {
            //[初期化]に変更したのでメッセージが発行される
            currentPhase.Value = Phase.INITIALIZE;   

            //初期化が終了したので[WAIT]に変更
            Initializer.InitializeFinished
                .Where(_ => currentPhase.Value == Phase.INITIALIZE)
                .Subscribe(_ => {
                    currentPhase.Value = Phase.WAIT;
                });

            //プレイヤーがSpaceキーを押したらゲームを開始（[Play]にへんこうする）
            this.UpdateAsObservable()
                .Where(_ => currentPhase.Value == Phase.WAIT)
                .Subscribe(_ => {
                    if(Input.GetKey(KeyCode.Space)){
                        currentPhase.Value = Phase.PLAY;
                    }
                });

            //
            //ブロックがなくなった時[Next]に変更
            BlockManager.BlockBroken
                //もしブロックがなくなったら
                .Where(_ => _ == 0 && currentPhase.Value == Phase.PLAY)
                //変更
                .Subscribe(_ => {
                    currentPhase.Value = Phase.NEXT;
                    Debug.Log("Next");
                });

            //ステージの作成が終了したらNEXTからWAITへ
            StageManager.madeStage
                .Where(_ => currentPhase.Value == Phase.NEXT)
                .Subscribe(_ => currentPhase.Value = Phase.WAIT);

            //残機が減ったら[WAIT]へ
            RemainingManager.remainingDecreased
                .Where(_ => _ != 0 && currentPhase.Value == Phase.PLAY)
                .Subscribe(_ => currentPhase.Value = Phase.WAIT);

            //残機がなくなっ場合[End]へ
            RemainingManager.remainingDecreased
                .Where(_ => _ == 0 && currentPhase.Value == Phase.PLAY)
                .Subscribe(_ => {
                    currentPhase.Value = Phase.END;
                });
            

        }
        public static Phase getPhase(){
            return currentPhase.Value;
        }

    }
}

