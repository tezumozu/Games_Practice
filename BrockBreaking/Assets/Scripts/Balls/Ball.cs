using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Managers;
using System;
using Moves;

namespace Balls{
    public class Ball : MoveObject
    {
    // Start is called before the first frame update
        private static int BallNum = 0;
        private static Subject<int> BallLossSubject = new Subject<int>();
        public static IObservable<int> BallLoss{
            get{return BallLossSubject;}
        }

        void Start()
        {
            //移動量の初期化
            moveData = new Vector2(3.0f,-3.0f);

            Rigidbody2D rgi = gameObject.GetComponent<Rigidbody2D>();
            rgi.velocity = Vector2.zero;

            BallNum += 1;
            StageManager.resetStage.Subscribe(_ => {
                BallNum -= 1;
                Destroy(gameObject);
            })
            .AddTo(gameObject);

            //Playになったら動く
            PhaseManager.phaseChanged
            .Where(_ => _ == Phase.PLAY)
            .Subscribe(_ => {
                move(moveData);
            })
            .AddTo(gameObject);
            
            //Update
            this.UpdateAsObservable()//barよりｙが低くなったら
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && transform.position.y < -4.5f)
                .Subscribe(_ => {
                    BallNum--;
                    BallLossSubject.OnNext(BallNum);
                    Destroy(gameObject);
                }); 
            
            this.UpdateAsObservable()//ボールのベクトルが垂直水平になったら
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && transform.position.y < -4.5f)
                .Subscribe(_ => {
                    /*if(){

                    }else if(){

                    }*/
                });
        }
        void OnCollisionEnter2D(Collision2D target){//衝突時反射
            Rigidbody2D rgi = gameObject.GetComponent<Rigidbody2D>();
            moveData = rgi.velocity;//反射したらベクトルを保存
            Debug.Log(moveData);
        }

        public override void move(Vector2 data){
            Rigidbody2D rgi = gameObject.GetComponent<Rigidbody2D>();
            rgi.velocity = moveData;
        }
    }

}

