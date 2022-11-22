using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    [SerializeField] RectTransform gear;
    [SerializeField] Image image;

    private void Start()
    {
        
        Debug.Log("포지션 : " + image.transform.position);
        Debug.Log("렉트 포지션 : " + image.GetComponent<RectTransform>().position);
    }
}
