using UnityEngine;
using DG.Tweening;

public class Ring : MonoBehaviour
{
    public void PopRing()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var _material = child.GetChild(0).GetComponent<Renderer>().material;
            child.DOScale(child.localScale * 1.5f, 1f).SetEase(Ease.OutCirc);
            var color = new Color(_material.color.r, _material.color.g, _material.color.b, 0);
            _material.DOColor(color, 1f).SetEase(Ease.OutCirc);   
        }
    }
}
