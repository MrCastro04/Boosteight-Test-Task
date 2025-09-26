using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorChangerAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _timeToChangeColor;
    
    private Color _originalColor = Color.white;
    private Color _redColor = Color.red;
    private Color _currentColor;

    private IEnumerator Start()
    {
        while (true)
        {
            _text.color = _originalColor;

            yield return new WaitForSeconds(_timeToChangeColor);
                
            _text.color = _redColor;
                
            yield return new WaitForSeconds(_timeToChangeColor);
        }
    }
}
