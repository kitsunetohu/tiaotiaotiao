using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerClick(PointerEventData PointerEventData)
    {
        GetComponent<CanvasGroup>().DOFade(0, 2).OnComplete(() => gameObject.SetActive(false));
    }

}
