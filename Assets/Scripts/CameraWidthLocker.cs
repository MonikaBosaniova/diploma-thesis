using UnityEngine;

[ExecuteInEditMode] // Umožní vidieť zmeny priamo v Editore
[RequireComponent(typeof(Camera))]
public class CameraWidthLocker : MonoBehaviour
{
    [Header("Nastavenia cieľa (napr. Full HD)")]
    public float targetWidth = 19.2f;  // Želaná šírka v Unity jednotkách
    public float targetHeight = 10.8f; // Želaná výška v Unity jednotkách

    private Camera _camera;

    void Awake() => _camera = GetComponent<Camera>();

    void LateUpdate() // LateUpdate je lepší pre kameru, aby sa vyhlo traseniu
    {
        Adjust();
    }

    void Adjust()
    {
        float targetAspect = targetWidth / targetHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        // PRÍPAD A: Obrazovka je "vyššia" (MacBook, Portrét)
        if (currentAspect < targetAspect)
        {
            // Fixujeme šírku. 
            // Výška (orthographicSize) sa zväčší, aby vyplnila displej, 
            // ale my ju musíme prepočítať tak, aby sme nevideli "zákulisie".
            _camera.orthographicSize = (targetWidth / currentAspect) * 0.5f;
            
            // VOLITEĽNÉ: Ak chceš radšej orezať BOKY, aby si neuvidiel vrch/spodok:
            // _camera.orthographicSize = targetHeight * 0.5f; 
        }
        // PRÍPAD B: Obrazovka je "širšia" (Ultra-wide monitor)
        else
        {
            // Fixujeme výšku, aby sme nevideli veci nad a pod levelom.
            _camera.orthographicSize = targetHeight * 0.5f;
        }
    }
}