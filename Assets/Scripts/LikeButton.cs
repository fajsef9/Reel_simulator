using UnityEngine;
using UnityEngine.UI;

public class LikeButton : MonoBehaviour
{
    [SerializeField] private Image heartImage;

    private bool isLiked = false;

    public void ToggleLike()
    {
        isLiked = !isLiked;

        Debug.Log(isLiked ? "Liked!" : "Unliked!");
    }
}