using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    //public GameObject Quizpanel;
    public GameObject Quizpanel;


    public Text QuestionTxt;
    //public Text ScoreTxt;

    int totalQuestions = 0;
    // public int score;

    public ChangeCardScale CheckAnswer1;
    public ChangeCardScale CheckAnswer2;

 

    private void Start()
    {
        totalQuestions = QnA.Count;

        generateQuestion();
        


    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OutOfQuestion()
    {
        Quizpanel.SetActive(false);
        CanvasManagement.canOpenQuestionCanvas = false;
        Doortransition.CanClick = true;
        //ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        //when you are right
        //score += 1;

        if (CheckAnswer1.canCheckAnswer)
        {
            Score.PlayerSatisValue += QnA[currentQuestion].PlayerSatisfaction;
            Score.MoneyValue += QnA[currentQuestion].Money;
            Score.SafetyValue += QnA[currentQuestion].Safety;
            Debug.Log("Correct");
            QnA.RemoveAt(currentQuestion);
            StartCoroutine(waitForNext());
            GetComponent<AudioSource>().Play();
        }
        
    }

    public void wrong()
    {
        //when you answer wrong
        if (CheckAnswer2.canCheckAnswer)
        {
            Score.PlayerSatisValue += QnA[currentQuestion].AntiPlayerSatisfaction;
            Score.MoneyValue += QnA[currentQuestion].AntiMoney;
            Score.SafetyValue += QnA[currentQuestion].AntiSafety;
            Debug.Log("inCorrect");
            QnA.RemoveAt(currentQuestion);
            StartCoroutine(waitForNext());
            GetComponent<AudioSource>().Play();
        }
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestion();
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            //options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            //options[i].GetComponent<AnswerScript>().isCorrect = false;
            //options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            options[i].GetComponentInChildren<Text>().text = QnA[currentQuestion].Answers[i];
            /*if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }*/
        }
    }

    void generateQuestion()
    {
        

        if (QnA.Count > 0)
        {
            //randomize question
            //currentQuestion = Random.Range(0, QnA.Count);
            //QuestionTxt.text = QnA[currentQuestion].Question;

            //to generate  according to order 
            for (int i = 0; i < QnA.Count; i++)
            {
                currentQuestion = i;
                QuestionTxt.text = QnA[i].Question;
            }

              
            SetAnswers();
        }
        else
        {
            Debug.Log("Out of Questions");
            OutOfQuestion();
        }


    }
}
