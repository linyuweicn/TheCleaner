using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public question[] questions;
	public List<question> unansweredQuestions;

	private question currentQuestion;

	int totalQuestions = 0;

	[SerializeField]
	private Text factText;

	[SerializeField]
	private float timeBetweenQuestions = 1.0f ;

	[SerializeField]
	private Text trueAnswerText, falseAnswerText;

	[SerializeField]
	private Animator animator;

	AudioSource ClickSound;
	void Start()
	{

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<question>();
        }

        totalQuestions = unansweredQuestions.Count;
		SetCurrentQuestion();

		ClickSound = GetComponent<AudioSource>();

	}

	void SetCurrentQuestion()
	{
		if (unansweredQuestions.Count > 0)
        {
			int randomQuestionsIndex = Random.Range(0, unansweredQuestions.Count);
			currentQuestion = questions[randomQuestionsIndex];
			factText.text = currentQuestion.fact;
		}
        else
        {
			Debug.Log("Out of Questions");
		}
		
	}

	IEnumerator TranstionToNextQuestion()
	{
		unansweredQuestions.Remove (currentQuestion);

		yield return new WaitForSeconds (1.0f );

		//SetCurrentQuestion();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	

	public void userSelectTrue()
	{
		animator.SetTrigger ("True");
		Score.PlayerSatisValue += currentQuestion.PlayerSatisfaction;
		Score.MoneyValue += currentQuestion.Money;
		Score.SafetyValue += currentQuestion.Safety;
		
		StartCoroutine (TranstionToNextQuestion());
		ClickSound.Play();
	}

	public void userSelectFalse()
	{
		animator.SetTrigger ("False");
		Score.PlayerSatisValue += currentQuestion.AntiPlayerSatisfaction;
		Score.MoneyValue += currentQuestion.AntiMoney;
		Score.SafetyValue += currentQuestion.AntiSafety;

		StartCoroutine(TranstionToNextQuestion());
		ClickSound.Play();
	}
}
