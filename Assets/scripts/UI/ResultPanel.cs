using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public Button replay;
    public Button next;
    // Start is called before the first frame update
    void Start()
    {
        replay.onClick.AddListener(GameManager.Instance.ReplayScene);
        next.onClick.AddListener(GameManager.Instance.NextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
