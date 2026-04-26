using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// Handles pointer click events on a GameObject and invokes the OnClick UnityEvent
/// </summary>
public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Called when the user clicks on the object, triggers the OnClick event
    /// </summary>
    /// <param name="eventData">Pointer event data from the input system</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("OnPointerClick " + gameObject.name);
        OnClick.Invoke();
    }
    
    public UnityEvent OnClick;
    
}
