using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Blocks;

namespace Managers{
    public class StageManager : MonoBehaviour
    {
        private static int stageNum = 1;

        //ステージ作成完了を通知
        private static Subject<Unit> makeStageSubjet = new Subject<Unit>();
        public static IObservable<Unit> madeStage{
            get{return makeStageSubjet;}
        }

        //ステージのリセットを通知
        private static Subject<Unit> resetStageSubjet = new Subject<Unit>();
        public static IObservable<Unit> resetStage{
            get{return resetStageSubjet;}
        }
        
       void Start(){
            PhaseManager.phaseChanged
            .Where(_ => _ == Phase.NEXT)
            .Subscribe(_ => {
                stageReset();
                stageNum++;
                makeStage(stageNum);
            });

            PhaseManager.phaseChanged
            .Where(_ => _ == Phase.WAIT)
            .Subscribe(_ => {
                makeBall();
            });
        }

        public static int getStageNum(){
            return stageNum;
        }

        public static void stageInitialize(){//ステージの初期化
            //リセット
            stageReset();

            //stageNumの初期化
            stageNum = 1;

            //ステージの生成
            makeStage(stageNum);
        }

        private static void stageReset(){//不要になったブロックやボールを削除
            resetStageSubjet.OnNext(Unit.Default);
            //ボールの削除
            //ブロックの削除
        }

        private static void makeStage(int stageNum){//ステージの作成
            //ブロックの生成
            BlockFactory.makeBlock(stageNum);

            //終了を通知
            makeStageSubjet.OnNext(Unit.Default);
        }

        private static void makeBall(){//ボールをついかする
            GameObject obj  = (GameObject)Resources.Load("Prefabs/Ball");
            GameObject clone = Instantiate(obj,new Vector3(0,-3.0f,0),Quaternion.identity);
        }   
    }
}

