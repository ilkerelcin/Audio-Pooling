using UnityEngine;

public class Example : MonoBehaviour
{
    public AudioClip takeDamageClip;
    public AudioClip attackClip;

    public void PlayTakeDamage()
    {
        AudioPoolManager.Instance.PlaySound(takeDamageClip, transform.position, 1f);
    }
    public void PlayAttack()
    {
        AudioPoolManager.Instance.PlaySound(attackClip, transform.position, 1f);
    }
}
