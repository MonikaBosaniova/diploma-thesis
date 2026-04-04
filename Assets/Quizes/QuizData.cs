using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    public VisualTreeAsset  AnswerTemplate;
    [Header("[WARNING] The first answer is the correct one.")]
    [Space(5f)]
    public List<QuizQuestion> quizQuestions;
}

[System.Serializable]
public struct QuizQuestion
{
    public LocalizedString question;
    public List<LocalizedString> answers;
    public int value;
}
