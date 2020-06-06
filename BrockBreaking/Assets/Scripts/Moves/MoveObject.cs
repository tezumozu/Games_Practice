using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moves{
    public interface IMoveable
    {
        
        void move(Vector2 data);

        Vector2 getVector2();

        void setVector2(Vector2 data);
    }

    abstract public class MoveObject : MonoBehaviour,IMoveable{
        protected Vector2 moveData;

        public virtual void move(Vector2 data){

        }
        public Vector2 getVector2(){
            return moveData;
        }

        public void setVector2(Vector2 data){
            this.moveData = data;
        }
    }
}

