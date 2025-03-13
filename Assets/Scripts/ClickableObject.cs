using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick " + gameObject.name);
        OnClick.Invoke();
    }
    
    public UnityEvent OnClick;
    
}
