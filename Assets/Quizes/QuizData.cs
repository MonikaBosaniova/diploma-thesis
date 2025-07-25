using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    public VisualTreeAsset  AnswerTemplate;
    [Header("First answer is the correct one.")]
    public List<QuizQuestion> quizQuestions;
}

[System.Serializable]
public struct QuizQuestion
{
    public LocalizedString question;
    public List<LocalizedString> answers;
    public int value;
}
