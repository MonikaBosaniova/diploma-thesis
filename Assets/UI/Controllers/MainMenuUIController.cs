using System;
using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUIController : MainMenuUIControllerContext
    {
        enum Scenes
        {
            Gates,
        }
        
        private void Start()
        {
            Initialize();
        }

        protected override void Bind()
        {
            base.Bind();
            Debug.Log("MainMenuUIController.Bind");
            CloseTab.tabHeader.style.backgroundColor = Color.red;
            CloseTab.tabHeader.style.display = DisplayStyle.None;

            var last = TabView[TabView.childCount - 1];
            TabView.RemoveAt(TabView.childCount - 1);
            TabView.Insert(0, last);                   

            BindTabHeaderButtons();
            BindLevelButtons();
        }

        private void BindLevelButtons()
        {
            GatesButton.clicked += () => ChangeToScene(Scenes.Gates);
        }

        private void ChangeToScene(Scenes scene)
        {
            int indexOfNextScene = Array.IndexOf(Enum.GetValues(typeof(Scenes)), scene) + 2;
            SceneManager.LoadScene(indexOfNextScene);
        }

        private void BindTabHeaderButtons()
        {
            CloseTab.tabHeader.RegisterCallback<ClickEvent>(HideTabHeader);
            
            PlayTab.tabHeader.RegisterCallback<ClickEvent>(ShowCloseTab);
            QuizTab.tabHeader.RegisterCallback<ClickEvent>(ShowCloseTab);
            SettingsTab.tabHeader.RegisterCallback<ClickEvent>(ShowCloseTab);
        }

        private void HideTabHeader(ClickEvent evt)
        {
            CloseTab.tabHeader.style.display = DisplayStyle.None;
            
            PlayTab.RemoveFromClassList("transitionOpen");
            QuizTab.RemoveFromClassList("transitionOpen");
            SettingsTab.RemoveFromClassList("transitionOpen");
            CloseTab.RemoveFromClassList("transitionOpen");
            
            PlayTab.AddToClassList("transitionClose");
            QuizTab.AddToClassList("transitionClose");
            SettingsTab.AddToClassList("transitionClose");
            CloseTab.AddToClassList("transitionClose");
        }
        
        private void ShowCloseTab(ClickEvent evt)
        {
            CloseTab.tabHeader.style.display = DisplayStyle.Flex;
            
            PlayTab.RemoveFromClassList("transitionClose");
            QuizTab.RemoveFromClassList("transitionClose");
            SettingsTab.RemoveFromClassList("transitionClose");
            CloseTab.RemoveFromClassList("transitionClose");
            
            PlayTab.AddToClassList("transitionOpen");
            QuizTab.AddToClassList("transitionOpen");
            SettingsTab.AddToClassList("transitionOpen");
            CloseTab.AddToClassList("transitionOpen");
        }
    }
    
}
