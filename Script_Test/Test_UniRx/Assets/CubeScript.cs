using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    // Start is called before the first frame update

    private Subject<Unit> OnPressedSpace = new Subject<Unit>();
    private Subject<Unit> OnPressedSpace2 = new Subject<Unit>();
    void Start()
    {
        StartCoroutine(testCoroutine());

        OnPressedSpace.Subscribe( x => {
            Debug.Log(x);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator testCoroutine(){
        while(true){
            if(Input.GetKey(KeyCode.Space)){
                break;
            }
            yield return null; 
        }

        OnPressedSpace.OnNext(Unit.Default);
        yield return null; 
    }
}
