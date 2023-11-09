using UnityEngine;
using DG.Tweening;
public class Obstacle : MonoBehaviour, IInteractable
{
    private Renderer _renderer;
    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
    void IInteractable.Interact()
    {
        DOTween.To(() => _renderer.material.color, x => _renderer.material.color = x, Color.red, 1f).Play();
        transform.DOShakeScale(1.1f, 0.095f, 10, 90, true)
            .SetDelay(0.5f)
            .Play();

        transform.DOScale(Vector3.zero, 1f)
            .OnKill(() =>
            {
                gameObject.SetActive(false);
            })
            .SetDelay(0.75f)
            .Play();
    }
}
