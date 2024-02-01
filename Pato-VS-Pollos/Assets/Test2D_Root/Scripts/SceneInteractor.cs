using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInteractor : MonoBehaviour
{
    public int musicIndex;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(musicIndex);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
