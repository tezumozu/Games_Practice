using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Blocks;

namespace Managers{
    public class BlockManager : MonoBehaviour
    {
    // Start is called before the first frame update

        private static int blockNum = 0;

        //ブロックが壊れたことをマネージャ周りに通知
        private static Subject<int> BlockManagerSubject = new Subject<int>();
        public static IObservable<int> BlockBroken{
            get{return BlockManagerSubject;}
        }

        void Start()
        {
            StageManager.resetStage.Subscribe(_ => {
                blockNum = 0;
            });
        }

        public static void addSubjectBlocks(DestructableBlock db){//観測するBlockをついかする
                    
            db.BlockBroken.Subscribe(_ => {
                blockNum--;
                BlockManagerSubject.OnNext(blockNum);
            });
            blockNum++;
            
        }

        public static int getBlockNum(){
            return blockNum;
        }
    }
}
