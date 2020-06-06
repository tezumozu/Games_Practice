using System.Collections;
using System;
using Blocks;

namespace Managers
{
    public struct BlockData
    {
        public BlockKind kind;
        public float x;
        public float y;
    }
    public class BlockDataManager
    {
        
        public static BlockData[,] getBlockData(int stageNum){
            BlockData[,] result = getTestData();
            
            //本来は何かしらでデータをとってくる処理
            return result;
        }

        private static BlockData[,] getTestData(){
            BlockData[,] result = new BlockData[10,10];
            /*BlockData data = new BlockData();
            data.x = 1f;
            data.y = 1f;
            result[0,0] = data;*/
            for(int i = 0; i < 10; i++){
                for(int j = 0; j < 10; j++){
                    BlockData data = new BlockData();

                    data.x = (float)j;
                    data.y = (float)i;
                    if(i < 2){
                       data.kind  = BlockKind.TestBlock;
                    }else if(i < 4){
                        data.kind = BlockKind.GreenDestructableBlock;
                    }else if(i < 6){
                        data.kind = BlockKind.UnDestructableBlock;
                    }else if(i < 8){
                        data.kind = BlockKind.YellowDestructableBlock;
                    }else {
                        data.kind = BlockKind.RedDestructableBlock;
                    }

                    result[i,j] = data;
                }
            }
            return result;
        }
    }
}