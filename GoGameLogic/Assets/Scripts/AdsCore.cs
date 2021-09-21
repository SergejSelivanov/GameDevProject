using UnityEngine;
using UnityEngine.Advertisements;

public class AdsCore : MonoBehaviour
{
    [SerializeField] private bool _testMode = true;

    private string _gameId = "4371741";

    private string _video = "Interstitial_Android";

    private void Start()
    {
        Advertisement.Initialize(_gameId, _testMode);
    }

    public static void ShowAdsVideo(string placementId)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementId);
        }
    }
}
