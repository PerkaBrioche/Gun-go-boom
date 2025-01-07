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

    public void ShakeCamera(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }

    private IEnumerator Shake(float intensity, float duration)
    {
        float elapsed = 0.0f;
        Vector3 originalPos = _rectTransform.localPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-intensity, intensity);
            float offsetY = Random.Range(-intensity, intensity);

            _rectTransform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            yield return null;
        }
        _rectTransform.localPosition = originalPos;
    }
}