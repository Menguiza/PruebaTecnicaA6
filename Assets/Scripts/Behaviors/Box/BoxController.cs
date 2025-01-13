using UnityEngine;
using UnityEngine.Events;
using Utility.Audio;

public class BoxController : MonoBehaviour
{
    public delegate void BoxState(bool isOpen);
    public static event BoxState OnBoxStateChanged;

    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Particles")]
    [SerializeField] private ParticleSystem particleSystemPrefab;
    [SerializeField] private Transform particlesParent;

    [Header("Sound Effects")]
    public UnityEvent CastSounds;

    public bool IsOpen { get; private set; }

    public Animator BoxAnimator => _animator;

    public void BoxOpened()
    {
        IsOpen = true;
        OnBoxStateChanged?.Invoke(true);

        GameObject particle = Instantiate(particleSystemPrefab, particlesParent.position, particlesParent.rotation, particlesParent).gameObject;

        CastSounds?.Invoke();
    }

    public void BoxClosed()
    {
        IsOpen = false;
        OnBoxStateChanged?.Invoke(false);
    }
}
