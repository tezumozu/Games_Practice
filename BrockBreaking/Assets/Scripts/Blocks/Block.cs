using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UniRx;
using System;

namespace Blocks{
    public enum BlockKind{
        BlueDestructableBlock,
        GreenDestructableBlock,
        RedDestructableBlock,
        YellowDestructableBlock,
        UnDestructableBlock,

        TestBlock,
    }

    abstract public class Block : MonoBehaviour
        {
    // Start is called before the first frame update
        void Start()
        {
            StageManager.resetStage.Subscribe(_ => {
                Destroy(gameObject);
            }).AddTo(gameObject);
        }

    // Update is called once per frame
        void Update()
        {
        
        }
    }
}

