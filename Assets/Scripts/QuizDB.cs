using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuizDB : MonoBehaviour
{
    [SerializeField] private List<Question> questionList = null;

    [SerializeField] private List<Question> backup = null;

    private void Awake()
    {
        backup = questionList.ToList();
    }
    public Question GetRandom(bool remove = true)
    {
        if (questionList.Count == 0)
            RestoreBackup(); 

        int index = Random.Range(0, questionList.Count);

        if (!remove)
            return questionList[index];
    
        Question q = questionList[index];
        questionList.RemoveAt(index);

        return q;
    }

    private void RestoreBackup() 
    {
        questionList = backup.ToList();
    }
}
