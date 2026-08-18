using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class ReelManager : MonoBehaviour
{
    [Header("Video")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private ReelData[] reelPool;

    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PhoneController phoneController;

    [Header("Reel UI")]
    [SerializeField] private RectTransform content;
    [SerializeField] private float snapSpeed = 10f;

    [Header("Rarity Chances")]
    [SerializeField] private float commonChance = 55f;
    [SerializeField] private float uncommonChance = 25f;
    [SerializeField] private float rareChance = 12f;
    [SerializeField] private float legendaryChance = 6f;
    [SerializeField] private float mythicalChance = 2f;

    [Header("Points")]
    [SerializeField] private float pointsDelay = 2f;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private RarityPopupAnimator rarityPopupAnimator;

    private int currentReel = 0;
    private float reelHeight;

    private ReelData currentReelData;

    private float pointsTimer = 0f;
    private bool pointsAwarded = false;

    private int currentScore = 0;

    public bool IsVideoPlaying => videoPlayer.isPlaying;

    private void Start()
    {
        reelHeight = content.GetChild(0)
            .GetComponent<RectTransform>()
            .rect.height;
        scoreText.text = "SCORE: 0";

        PlayRandomReel();
    }

    private void Update()
    {
        // Stop everything when the game is over
        if (gameManager.IsGameOver)
            return;

        // Reset the point timer while the phone is hidden
        if (!phoneController.IsPhoneOut)
        {
            pointsTimer = 0f;
        }

        // =========================
        // POINT TIMER
        // =========================

        if (
            phoneController.IsPhoneOut &&
            videoPlayer.isPlaying &&
            !pointsAwarded
        )
        {
            pointsTimer += Time.deltaTime;

            if (pointsTimer >= pointsDelay)
            {
                AwardCurrentReelPoints();
            }
        }

        // =========================
        // SCROLL
        // =========================

        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll < 0)
        {
            GoToNextReel();
        }
    }

    public void PlayRandomReel()
    {
        if (reelPool == null || reelPool.Length == 0)
        {
            Debug.LogWarning("Reel pool is empty!");
            return;
        }

        ReelData selectedReel = SelectRandomReel();

        if (selectedReel == null || selectedReel.video == null)
        {
            Debug.LogWarning("Selected reel has no video!");
            return;
        }

        // Store current reel
        currentReelData = selectedReel;

        // Reset points timer
        pointsTimer = 0f;
        pointsAwarded = false;

        // Load video
        videoPlayer.Stop();

        videoPlayer.clip = selectedReel.video;

        videoPlayer.Play();

        Debug.Log(
            "Playing " +
            selectedReel.rarity +
            " reel | +" +
            selectedReel.points +
            " points"
        );
    }

    private void AwardCurrentReelPoints()
    {
        if (pointsAwarded)
            return;

        pointsAwarded = true;

        currentScore += currentReelData.points;

        scoreText.text = "SCORE: " + currentScore;

        // Debug.Log(
        //     "Current Score: " +
        //     currentScore
        // );

        ShowRarityPopup();
    }
    private void ShowRarityPopup()
    {
        string text =
            currentReelData.rarity.ToString().ToUpper() +
            "\n+" +
            currentReelData.points;

        Color textColor = Color.white;

        switch (currentReelData.rarity)
        {
            case ReelRarity.Common:
                textColor = Color.white;
                break;

            case ReelRarity.Uncommon:
                textColor = Color.green;
                break;

            case ReelRarity.Rare:
                textColor = Color.blue;
                break;

            case ReelRarity.Legendary:
                textColor = Color.yellow;
                break;

            case ReelRarity.Mythical:
                textColor = Color.red;
                break;
        }

        rarityPopupAnimator.Show(
            text,
            textColor
        );
    }



    private ReelData SelectRandomReel()
    {
        float roll = Random.Range(0f, 100f);

        ReelRarity selectedRarity;

        // =========================
        // COMMON — 55%
        // =========================

        if (roll < commonChance)
        {
            selectedRarity = ReelRarity.Common;
        }

        // =========================
        // UNCOMMON — 25%
        // =========================

        else if (roll < commonChance + uncommonChance)
        {
            selectedRarity = ReelRarity.Uncommon;
        }

        // =========================
        // RARE — 12%
        // =========================

        else if (
            roll <
            commonChance +
            uncommonChance +
            rareChance
        )
        {
            selectedRarity = ReelRarity.Rare;
        }

        // =========================
        // LEGENDARY — 6%
        // =========================

        else if (
            roll <
            commonChance +
            uncommonChance +
            rareChance +
            legendaryChance
        )
        {
            selectedRarity = ReelRarity.Legendary;
        }

        // =========================
        // MYTHICAL — 2%
        // =========================

        else
        {
            selectedRarity = ReelRarity.Mythical;
        }

        // Find all reels with this rarity
        ReelData[] matchingReels = System.Array.FindAll(
            reelPool,
            reel =>
                reel != null &&
                reel.rarity == selectedRarity
        );

        // Safety fallback
        if (matchingReels.Length == 0)
        {
            Debug.LogWarning(
                "No reels found for rarity: " +
                selectedRarity
            );

            return reelPool[
                Random.Range(0, reelPool.Length)
            ];
        }

        // Pick a random reel from the selected rarity
        return matchingReels[
            Random.Range(0, matchingReels.Length)
        ];
    }

    private void GoToNextReel()
    {
        currentReel++;

        if (currentReel >= content.childCount)
        {
            currentReel = 0;
        }

        content.anchoredPosition = new Vector2(
            content.anchoredPosition.x,
            currentReel * reelHeight
        );

        PlayRandomReel();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }
}