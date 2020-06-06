using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UniRx;
using UniRx.Triggers;
using Moves;
using System;


namespace Bars{
    public class Bar : MoveObject{

        private static Subject<Unit> BarRefrectSubject = new Subject<Unit>();
        public static IObservable<Unit> BarRefrected{
            get {return BarRefrectSubject;}
        }  
        void Start()
        {
            moveData = new Vector2(0f,0f);
            //右矢印
            this.UpdateAsObservable()
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && Input.GetKeyDown(KeyCode.RightArrow))
                .Subscribe(_ => moveData.x += 0.1f);
             this.UpdateAsObservable()
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && Input.GetKeyUp(KeyCode.RightArrow))
                .Subscribe(_ => moveData.x -= 0.1f);    

            //左矢印
            this.UpdateAsObservable()
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && Input.GetKeyDown(KeyCode.LeftArrow))
                .Subscribe(_ => moveData.x -= 0.1f);
            this.UpdateAsObservable()
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY && Input.GetKeyUp(KeyCode.LeftArrow))
                .Subscribe(_ => moveData.x += 0.1f);
            
            //upDate
            this.UpdateAsObservable()
                .Where(_ => PhaseManager.getPhase() == Phase.PLAY)
                .Subscribe(_ => {
                    move(moveData);
                    });

            PhaseManager.phaseChanged
                .Where(_ => _ == Phase.WAIT)
                .Subscribe(_ => barInitialize());
        }

        public override void move(Vector2 moveData){//移動時の処理
            this.transform.Translate ( moveData.x,moveData.y,0f);

            //min:画面左下 max:画面右上 各座標を取得
            Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
            Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
            
            //barの幅を取得
            float w = this.GetComponent<SpriteRenderer>().bounds.size.x;
            
            //はみ出していたら修正
            Vector3 pos = transform.position;
            if(pos.x - w/2 < min.x ||  max.x < pos.x + w/2){
                this.transform.Translate ( -moveData.x,moveData.y,0f);
            }
        }

        private void barInitialize(){
            this.transform.position = new Vector3(0f,-4,0);
            moveData = new Vector2(0f,0f);
        }

        void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.tag == "Ball"){//ボールと衝突したら
                BarRefrectSubject.OnNext(Unit.Default);//跳ね返したことを通知
            }
        }
    }
}

