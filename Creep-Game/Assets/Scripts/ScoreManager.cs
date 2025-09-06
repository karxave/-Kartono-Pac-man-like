using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    
    private int _scoreIntValue = 0;
    private int _maxScoreIntValue = 0;

 
    public void UpdateUI_scoreText()
    {
        Debug.Log(" UpdateUi_scoreText  --> _scoreIntValue :" + _scoreIntValue );
        _scoreText.text = "Score : " + _scoreIntValue + " / " + _maxScoreIntValue;
    }


    public void SetMaxScore(int scoreValue)
    {
        _maxScoreIntValue = scoreValue;
        Debug.Log(" set max score :" + scoreValue);
        UpdateUI_scoreText();
    }

    public void AddScore(int value)
    {
        //  Debug.Log(" _scoreIntValue before addition:     " + _scoreIntValue);
        //  _scoreIntValue = _scoreIntValue + value;
        //_scoreIntValue++;
        //  Debug.Log(" AddScore added _scoreUntValue :" + _scoreIntValue);
        //  UpdateUI_scoreText();

        Debug.Log($"AddScore called, value={value}. Current score={_scoreIntValue}", this);
        Debug.Log(Environment.StackTrace); // shows the call stack
        _scoreIntValue += value;
        UpdateUI_scoreText();

    }

   private void Start()
   {
       UpdateUI_scoreText();
   }



}
