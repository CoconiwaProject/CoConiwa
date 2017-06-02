using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkSheetManager : MonoBehaviour
{
    [SerializeField]
    Text questionText = null;
    [SerializeField]
    GameObject endButton = null;

    Text nextButton, backButton, titleText;
    

    //どの選択肢が採用されるかわからないので消すのが容易なように分ける
    GameObject choices_five = null;
    GameObject choices_four = null;
    GameObject choices_three = null;
    GameObject choices_two = null;

    //記述式用
    InputField inputField = null;

    [SerializeField]
    WorkSheetData workSheetData = null;

    //何問目か
    int currentQuestionIndex;
    MyQuestion currentQuestion;

    WorkSheetAnswer answer;

    public List<Toggle> toggleList = new List<Toggle>();

    void Awake()
    {
        choices_five = transform.FindChild("Choices_Five").gameObject;
        choices_four = transform.FindChild("Choices_Four").gameObject;
        choices_three = transform.FindChild("Choices_Three").gameObject;
        choices_two = transform.FindChild("Choices_Two").gameObject;
        inputField = transform.FindChild("InputField").GetComponent<InputField>();

        nextButton = transform.FindChild("NextButton").GetComponent<Text>();
        backButton = transform.FindChild("BackButton").GetComponent<Text>();
        titleText = transform.FindChild("TitleText").GetComponent<Text>();
    }

    public void Initialize()
    {
        backButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        titleText.gameObject.SetActive(true);
        endButton.SetActive(false);

        answer = new WorkSheetAnswer(workSheetData);

        for(int i = 0;i< workSheetData.questionList.Count;i++)
        {
            workSheetData.questionList[i].isAnswered = false;
        }

        //最初の質問をセットする
        currentQuestionIndex = 0;
        SetQuestion(workSheetData.questionList[0]);
    }

    public void Next()
    {
        //回答を保存する
        string answerText = GetAnswer();

        if (answerText == "") return;
        currentQuestion.isAnswered = true;
        answer.SetAnswer(currentQuestionIndex, answerText);

        if (currentQuestionIndex + 1 >= workSheetData.questionList.Count)
        {
            //終了
            Stop();
            return;
        }

        //次の問題
        currentQuestionIndex++;
        SetQuestion(workSheetData.questionList[currentQuestionIndex]);
    }

    public void Back()
    {
        //今出ているものをいったん消す
        choices_five.SetActive(false);
        choices_four.SetActive(false);
        choices_three.SetActive(false);
        choices_two.SetActive(false);
        inputField.gameObject.SetActive(false);

        //次の問題
        currentQuestionIndex--;
        SetQuestion(workSheetData.questionList[currentQuestionIndex]);
    }

    void SetQuestion(MyQuestion question)
    {
        currentQuestion = question;
        questionText.text = currentQuestion.question;

        //解答欄を表示する
        ClearChoices();
        ShowChoices();
        titleText.text = "質問" + (currentQuestionIndex + 1).ToString();

        //ボタンのチェック

        //あるべき姿
        backButton.gameObject.SetActive(true);
        nextButton.text = "次へ";

        if (currentQuestionIndex + 1 >= workSheetData.questionList.Count)
        {
            //最後の問題
            nextButton.text = "回答する";
        }
        if (currentQuestionIndex <= 0)
        {
            //最初の問題
            backButton.gameObject.SetActive(false);
        }
    }

    //解答欄を表示する
    void ShowChoices()
    {
        if (currentQuestion.m_type == QuestionType.Writing)
        {
            if (currentQuestion.isAnswered) inputField.text = answer.GetAnswer(currentQuestionIndex);
            else inputField.text = "";
            inputField.gameObject.SetActive(true);
            return;
        }
        if (currentQuestion.m_type == QuestionType.Choices_Five)
            ShowTggle(choices_five);
        if (currentQuestion.m_type == QuestionType.Choices_Four)
            ShowTggle(choices_four);
        if (currentQuestion.m_type == QuestionType.Choices_Three)
            ShowTggle(choices_three);
        if (currentQuestion.m_type == QuestionType.Choices_Two)
            ShowTggle(choices_two);
    }

    void ShowTggle(GameObject choices)
    {
        choices.SetActive(true);
        toggleList.AddRange(choices.GetComponentsInChildren<Toggle>());

        for (int i = 0; i < toggleList.Count; i++)
        {
            //指定がないと解釈する
            if (currentQuestion.choiceArray[i] == "")
            {
                currentQuestion.choiceArray[i] = toggleList[i].GetComponentInChildren<Text>().text;
                continue;
            }
            //トグルのラベルにセットする
            toggleList[i].GetComponentInChildren<Text>().text = currentQuestion.choiceArray[i];
        }

        if (!currentQuestion.isAnswered) return;
        for(int i = 0;i< toggleList.Count;i++)
        {
            if(toggleList[i].GetComponentInChildren<Text>().text == answer.GetAnswer(currentQuestionIndex))
            {
                toggleList[i].isOn = true;
            }
        }
    }

    //解答欄を消す
    void ClearChoices()
    {
        for(int i = 0;i< toggleList.Count;i++)
        {
            toggleList[i].isOn = false;
        }
        toggleList.Clear();

        choices_five.SetActive(false);
        choices_four.SetActive(false);
        choices_three.SetActive(false);
        choices_two.SetActive(false);
        inputField.gameObject.SetActive(false);
    }

    string GetAnswer()
    {
        if (currentQuestion.m_type == QuestionType.Writing)
        {
            return inputField.text;
        }
        else
        {
            for (int i = 0; i < toggleList.Count; i++)
            {
                if (toggleList[i].isOn) return toggleList[i].GetComponentInChildren<Text>().text;
            }
        }

        //回答したことにはならない
        return "";
    }

    //すべて回答されたら保存する
    void Stop()
    {
        backButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        questionText.text = "解答ありがとうございました。";
        ClearChoices();
        answer.SaveAnswer();
        endButton.SetActive(true);
    }
}
