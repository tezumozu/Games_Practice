using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Managers;
using UniRx.Triggers;

namespace Blocks{

    public class DestructableBlock : Block
    {
    //自身が破壊されたときに通知
        private Subject<Unit> BlockSubject = new Subject<Unit>();
        public IObservable<Unit> BlockBroken{
            get{return BlockSubject;}
        }


    // Start is called before the first frame update

        void OnCollisionEnter2D(Collision2D other){
            if(other.gameObject.tag == "Ball"){//ボールと衝突したら
                    BlockSubject.OnNext(Unit.Default);//破壊された事を通知し
                    BlockSubject.OnCompleted();//ストリームの終了
                    Destroy(gameObject);//自分を破壊する
            }
        }
    }
}

