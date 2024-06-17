using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
   [SerializeField] private List<Image> imageList;
   [SerializeField] private List<TMP_Text> textList;
   [SerializeField] private float fadeDuration = .5f;
   
   public IEnumerator Fade()
   {
      float fadeTime = 0;
      while (fadeTime < 2)
      {
         if(imageList.Count!=0) 
            foreach (Image image in imageList) 
               image.color = new Color(image.color.r,image.color.g,image.color.b , fadeTime);
         if(textList.Count!=0) 
            foreach (TMP_Text text in textList) 
               text.color = new Color(text.color.r,text.color.g,text.color.b , fadeTime);
        
         fadeTime += Time.deltaTime /fadeDuration;
         yield return null;
      }
   }
}

