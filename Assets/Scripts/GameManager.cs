using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using System.Security.Cryptography;
using DentedPixel;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip correctSound = null;
    [SerializeField] private AudioClip incorrectSound = null;
    [SerializeField] private AudioClip winSound = null;
    [SerializeField] private AudioClip loseSound = null;
    [SerializeField] private Color correctColor = Color.black;
    [SerializeField] private Color incorrectColor = Color.black;
    [SerializeField] private float waitTime = 0.0f;
    [SerializeField] private GameObject [] buttons;
    [SerializeField] private GameObject winUI,loseUI,quiz;
    [SerializeField] private TextMeshProUGUI count;
    [SerializeField] private TextMeshProUGUI antiCount;
    [SerializeField] private int countInt;
    [SerializeField] private int life;
    private float [] positions;
    [SerializeField] public GameObject bar;
    [SerializeField] public int time;

    [SerializeField] private QuizDB quizDB = null;
    [SerializeField] private QuizUI quizUI = null;
    private AudioSource audioSource = null;
    private bool flag;


    private void Start()
    {
        
        //quizDB = GameObject.FindObjectOfType<QuizDB>();
        //quizUI = GameObject.FindObjectOfType<QuizUI>();
        AnimateBar();
        audioSource = GetComponent<AudioSource>();
        positions = new float[3];
        positions[0] = -40f;
        positions[1] = -140f;
        positions[2] = -240f;
        countInt = 0;
        life = 0;
        count.text = countInt.ToString();
        antiCount.text = life.ToString();
        flag = false;
        NextQuestion();
    }

    private void randomOptions() {

        List<int> aux = new List<int>(){0,1,2};
        int n;
        for(int i=0;i<3;i++){
            n = Random.Range(0,aux.Count); 
            RectTransformExtensions.SetPosY(buttons[i].GetComponent<RectTransform>(),positions[aux[n]]+154);
            aux.RemoveAt(n);
        }
        
    }

    private void NextQuestion() {
        AnimateBar();
        randomOptions();
        quizUI.Construct(quizDB.GetRandom(),GiveAnswer);
    }

    private void GiveAnswer(OptionButton optionButton)
    {
        StartCoroutine(GiveAnswerRoutine(optionButton));
    }

    private IEnumerator GiveAnswerRoutine(OptionButton optionButton) 
    {
        //if (audioSource.isPlaying) 
        //    audioSource.Stop();

        audioSource.clip = optionButton.Option.correct ? correctSound : incorrectSound;
        optionButton.SetColor(optionButton.Option.correct ? correctColor : incorrectColor);

        audioSource.Play();

        yield return new WaitForSeconds(waitTime);

        if (optionButton.Option.correct)
        {
            countInt += 1;
            count.text = countInt.ToString();
            if (countInt == 3)
                WinGame();
            else
                NextQuestion();
        }
        else {
            life += 1;
            antiCount.text = life.ToString();
            if (life == 2)
            {
                LoseGame();
            }
            else {
                NextQuestion();
            }
            
        }
            
    }

    public void GameOver() 
    {
        SceneManager.LoadScene(0);
    }

    private void LoseGame()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = loseSound;
        audioSource.Play();
        quiz.SetActive(false);
        loseUI.SetActive(true);
    }

    private void WinGame()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = winSound;
        audioSource.Play();
        quiz.SetActive(false);
        winUI.SetActive(true);
    }
    public void AnimateBar()
    {
        LeanTween.cancel(bar);
        bar.transform.localScale.Set(0f, 1f, 1f);
        LeanTween.scaleX(bar, 1, time);
            
    }

    public void ResetBar() 
    {
        flag = false;
        LeanTween.cancel(bar);
        bar.transform.localScale.Set(0f, 1f, 1f);
        LeanTween.scaleX(bar, 0, 0f);
    }
    public void LateUpdate()
    {

        if (bar.transform.localScale.x == 1f && flag == false) {
            flag = true;
            life += 1;
            antiCount.text = life.ToString();
            if (life == 2)
            {
                LoseGame();
            }
            else
            {
                ResetBar();
                NextQuestion();
            }
            
        }
    }

}
