using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField]private AudioClip correctSound = null;
    [SerializeField] private AudioClip incorrectSound = null;
    [SerializeField] private Color correctColor = Color.black;
    [SerializeField] private Color incorrectColor = Color.black;
    [SerializeField] private float waitTime = 0.0f;

    private QuizDB quizDB = null;
    private QuizUI quizUI = null;
    private AudioSource audioSource = null;

    private void Start()
    {
        quizDB = GameObject.FindObjectOfType<QuizDB>();
        quizUI = GameObject.FindObjectOfType<QuizUI>();
        audioSource = GetComponent<AudioSource>();

        NextQuestion();
    }

    private void NextQuestion() {
        quizUI.Construct(quizDB.GetRandom(),GiveAnswer);
    }

    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    private IEnumerator GiveAnswerRoutine(OptionButton optionButton) 
    {
        if (audioSource.isPlaying) 
            audioSource.Stop();

        audioSource.clip = optionButton.Option.correct ? correctSound : incorrectSound;
        optionButton.SetColor(optionButton.Option.correct ? correctColor : incorrectColor);

        audioSource.Play();

        yield return new WaitForSeconds(waitTime);

        if (optionButton.Option.correct)
            NextQuestion();
        else
            GameOver();
    }

    private void GameOver() 
    {
        SceneManager.LoadScene(0);
    }
}
