using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Remaining : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        RemainingCounter.updateRemaining
        .Subscribe(_ => Destroy(gameObject))
        .AddTo(gameObject);
    }
}
