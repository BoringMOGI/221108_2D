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
        
        Debug.Log("������ : " + image.transform.position);
        Debug.Log("��Ʈ ������ : " + image.GetComponent<RectTransform>().position);
    }
}
