using System;
using UnityEngine;

public class FeedbackTextAppear : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0.5f;
    [SerializeField] private float _targetSize = 4f;
    [SerializeField] private float _timeBeforeFadeAlpha = 0.3f;
    private SpriteRenderer _spriteRenderer;
    private Transform _transform;
    private float _clock;
    private float _startSize;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();
        Destroy(gameObject, _spriteRenderer == null ? 0 : _lifeTime);
        _clock = 0;
        _startSize = _transform.localScale.x;
    }

    private void Update()
    {
        _clock += Time.deltaTime;
        if (_clock >= _timeBeforeFadeAlpha)
        {
            Color spriteRendererColor = _spriteRenderer.color;
            spriteRendererColor.a = Mathf.Lerp(1, 0, (_clock - _timeBeforeFadeAlpha) / (_lifeTime - _timeBeforeFadeAlpha));
            _spriteRenderer.color = spriteRendererColor;
        }
        _transform.localScale = Vector3.one * Mathf.Lerp(_startSize, _targetSize, _clock / _lifeTime);
    }
}
