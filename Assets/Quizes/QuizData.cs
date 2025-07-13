using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "QuizData", menuName = "Scriptable Objects/QuizData")]
public class QuizData : ScriptableObject
{
    public VisualTreeAsset  AnswerTemplate;
    public List<QuizQuestion> quizQuestions;
}

[System.Serializable]
public struct QuizQuestion
{
    public LocalizedString question;
    [Header("First answer is the correct one.")]
    public List<LocalizedString> answers;
    public int value;
}
