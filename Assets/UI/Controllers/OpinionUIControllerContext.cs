using UnityEngine.UIElements;

namespace UI
{
    /// <summary>
    /// Context class providing UI element bindings for the opinion survey sliders and close button
    /// Used only for testing
    /// </summary>
    public abstract  class OpinionUIControllerContext : UIControllerBaseContext
    {
        /// <summary>
        /// Slider for the enjoyment rating
        /// </summary>
        protected SliderInt enjoySlider => _Root.Q<SliderInt>("Enjoy");
        /// <summary>
        /// Slider for the tutorial helpfulness rating
        /// </summary>
        protected SliderInt tutorialSlider => _Root.Q<SliderInt>("Tutorial");
        /// <summary>
        /// Slider for the knowledge gained rating
        /// </summary>
        protected SliderInt knowledgeSlider => _Root.Q<SliderInt>("Knowledge");
        /// <summary>
        /// Button to close the opinion menu and submit results
        /// </summary>
        protected Button Close => _Root.Q<Button>("Close");

    }
}