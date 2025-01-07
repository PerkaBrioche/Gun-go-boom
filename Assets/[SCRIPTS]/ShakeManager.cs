using System.Collections;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager instance;
    [SerializeField] private RectTransform _rectTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // UTILISER CETTE FONCTION POUR JOUER UN SHAKE !!! //
    public void ShakeCamera(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }
    
    
    // PAS TOUCHE ICI EN BAS (*ABOIEMENT* WAF WAF WAF) //

    private IEnumerator Shake(float intensity, float duration)
    {
        float shakeIntensity = intensity;
        float elapsed = 0.0f;
        Vector3 originalPos = _rectTransform.localPosition;


        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            
            if (shakeIntensity < 0)
            {
                shakeIntensity += Time.deltaTime;
            }
            if (shakeIntensity > 0)
            {
                shakeIntensity -= Time.deltaTime;
            }


            
            _rectTransform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            yield return null;
        }
        _rectTransform.localPosition = originalPos;
    }
}