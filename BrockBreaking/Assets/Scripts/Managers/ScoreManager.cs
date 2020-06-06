using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Bars;

namespace Managers
{
   public class ScoreManager : MonoBehaviour
    {

        private static ReactiveProperty<float> score = new ReactiveProperty<float>(0);
        public static IReadOnlyReactiveProperty<float> ScoreChanged{
            get { return score; }
        } //外部はSubscribeのみ みえるようにする

        private static float bonus;
        void Start(){
            //ブロックが壊れたらスコアを加算する
            BlockManager.BlockBroken
                .Subscribe(_ => {
                    score.Value = score.Value + 100*bonus;
                    bonus += 0.1f;
                });

            //バーにボールが触れたらボーナスをリセット
            Bar.BarRefrected
                .Subscribe(_ => bonus = 1f);
        }

        //スコアのget関数
        public static float getScore(){
            return score.Value;
        }

        public static void scoreInitialize(){
            
            score.Value = 0f;
            bonus = 1f;
        }
    }
 
}
