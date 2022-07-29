using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Text question = null;
    [SerializeField] private List<OptionButton> buttonList = null;

    public void Construct(Question q, Action<OptionButton> callback)
    {
        question.text = q.text;

        for (int n = 0; n < buttonList.Count; n++)
        {
            buttonList[n].Construct(q.options[n], callback);
        }
    }
}
