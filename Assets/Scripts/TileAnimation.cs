using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class TileAnimation : MonoBehaviour
{
    public enum AnimationState
    {
        Fade,
        Move
    }

    [SerializeField] private Tile tile;
    [SerializeField] private FadeOut fade;

    private void Awake()
    {
        tile.OnAnimate += Animate;
    }

    public void Animate(object sender, Tile.TileAnimationEventArgs tileAnimationEventArgs)
    {
        if (tileAnimationEventArgs.State == AnimationState.Move)
            StartCoroutine(MovementAnimation(tileAnimationEventArgs.To, tileAnimationEventArgs.Merge));
        if (tileAnimationEventArgs.State == AnimationState.Fade)
            StartCoroutine(fade.Fade());

    }
    
    private IEnumerator MovementAnimation(Vector3 to , bool merge)
    {
        float elapsed = 0f;
        float duration = 0.1f;
       
        Vector3 from = transform.position;
       
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
       
        if (merge) {
            Destroy(gameObject);
        }
    }
    
}
