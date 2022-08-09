using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float ejeX;
    [SerializeField] private float ejeY;
    
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(ejeX,ejeY)*Time.deltaTime,img.uvRect.size);
    }
}
