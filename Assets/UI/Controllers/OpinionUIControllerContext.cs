using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public abstract  class OpinionUIControllerContext : UIControllerBaseContext
    {
        protected SliderInt enjoySlider => _Root.Q<SliderInt>("Enjoy");
        
        protected SliderInt tutorialSlider => _Root.Q<SliderInt>("Tutorial");
        
        protected SliderInt knowledgeSlider => _Root.Q<SliderInt>("Knowledge");
        protected Button Close => _Root.Q<Button>("Close");

    }
}