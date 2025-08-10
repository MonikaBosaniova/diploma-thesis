using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUIControllerContext : UIControllerBaseContext
    {
        protected TabView TabView => _Root.Q<TabView>();
        
        protected Tab CloseTab => _Root.Q<Tab>("Close");
        protected Tab PlayTab => _Root.Q<Tab>("Play");
        protected Tab QuizTab => _Root.Q<Tab>("Quiz");
        protected Tab SettingsTab => _Root.Q<Tab>("Settings");
        
        protected Button GatesButton => _Root.Q<Button>("Gates");
    }
    
}
