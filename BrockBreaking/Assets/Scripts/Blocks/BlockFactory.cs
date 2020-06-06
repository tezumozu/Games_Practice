using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Blocks{
    public class BlockFactory: MonoBehaviour
    {
        public static void makeBlock(int stageNum){
            foreach(BlockData block in BlockDataManager.getBlockData(stageNum)){//ブロックのデータを取得
                GameObject obj  = (GameObject)Resources.Load("Prefabs/" + block.kind.ToString());
                //座標計算
                float w = obj.GetComponent<SpriteRenderer>().bounds.size.x;
                float h = obj.GetComponent<SpriteRenderer>().bounds.size.y;
                float u_x = w*block.x - (w*5.0f - w/2.0f);//unityでの座標x
                float u_y = h*block.y - (h*5.0f - h/2.0f) + 1.0f;//unityでの座標y

                //オブジェクト生成
                GameObject clone = Instantiate(obj,new Vector3(u_x,u_y,0),Quaternion.identity);

                DestructableBlock db = clone.GetComponent<DestructableBlock>();
                if(db != null){//Destructableを持っていたら
                    BlockManager.addSubjectBlocks(db);//BlockManagerに追加
                }
            }
        }
    }
}

