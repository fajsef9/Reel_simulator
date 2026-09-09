using UnityEngine;
using UnityEngine.Video;



[System.Serializable]
public class ReelData
{
    public VideoClip video;
    [SerializeField] private string webFileName;
    public string WebFileName => webFileName;
    public ReelRarity rarity;
    public int points;
}