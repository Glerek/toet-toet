using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Subsystem
{
    [SerializeField]
    [Tooltip("Duration before it fades out (in secondes)")]
    private float _breakFadeOutDelay = 5f;

    [SerializeField]
    [Tooltip("Duration of the fade out (in seconds)")]
    private float _fadeOutDuration = 2f;

    private Car.WheelStructure _parentStructure = null;
    public Car.WheelStructure ParentStructure
    {
        get { return _parentStructure; }
        set { _parentStructure = value; }
    }

    protected override void OnBreak()
    {
        base.OnBreak();

        _renderer.color = Color.red;

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(_breakFadeOutDelay);

        float timer = 0f;
        Color rendererColor = _renderer.color;
        while(timer < _fadeOutDuration)
        {
            timer += Time.deltaTime;
            rendererColor.a = Mathf.Lerp(1f, 0f, timer / _fadeOutDuration);

            _renderer.color = rendererColor;
            yield return null;
        }

        GameObject.Destroy(this.gameObject);
    }
}
