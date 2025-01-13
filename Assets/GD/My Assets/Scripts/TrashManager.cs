using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashManager : MonoBehaviour
{
    public static TrashManager Instance; // Using singleton to share data across characters

    // Trash counts 
    public int plasticCount = 0;
    public int hazardousCount = 0;
    public int organicCount = 0;
    public int paperCount = 0;
    public int oilCount = 0;

    // Total deposited trash - these are used for the progress bars
    public int depositedPlastic = 0;
    public int depositedHazardous = 0;
    public int depositedOrganic = 0;
    public int depositedPaper = 0;
    public int depositedOil = 0;

    // All trash on the level - these are used for the progress bars (full bar)
    public int totalPlastic = 6;
    public int totalHazardous = 5;
    public int totalOrganic = 8;
    public int totalPaper = 7;
    public int totalOil = 7;

    // UI References to find the text elements
    [Header("UI Elements")]
    public TextMeshProUGUI plasticText;
    public TextMeshProUGUI hazardousText;
    public TextMeshProUGUI organicText;
    public TextMeshProUGUI paperText;
    public TextMeshProUGUI oilText;

    // UI References for progress bars
    public Slider plasticProgressBar;
    public Slider hazardousProgressBar;
    public Slider organicProgressBar;
    public Slider paperProgressBar;
    public Slider oilProgressBar;
    public Slider totalProgressBar;

    private void Awake()
    {
        // This makes it so that only one instance of the TrashManager exists at a time - if there is already some instance, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
    }

    private void Start()
    {
        startProgressBar();
        UpdateUI();
    }

    private void Update()
    {
        // Check if progress is complete and quits the game
        if (totalProgressBar.value >= totalProgressBar.maxValue)
        {
            Debug.Log("All trash collected, Exiting game...");
            Application.Quit();
        }
    }

    // This method sets the maximum values for progress bars
    private void startProgressBar()
    {
        plasticProgressBar.maxValue = totalPlastic;
        hazardousProgressBar.maxValue = totalHazardous;
        organicProgressBar.maxValue = totalOrganic;
        paperProgressBar.maxValue = totalPaper;
        oilProgressBar.maxValue = totalOil;

        // Sets the maximum value for total progress bar
        totalProgressBar.maxValue = totalPlastic + totalHazardous + totalOrganic + totalPaper + totalOil;
    }

    // This method updates the UI for trash counts and progress bars
    public void UpdateUI()
    {
        // Update text for trash counts using "$" to make string change
        plasticText.text = $"Plastic: {plasticCount}";
        hazardousText.text = $"Hazardous: {hazardousCount}";
        organicText.text = $"Organic: {organicCount}";
        paperText.text = $"Paper: {paperCount}";
        oilText.text = $"Oil: {oilCount}";

        // Here I update the progress bars based on collected trash
        plasticProgressBar.value = depositedPlastic;
        hazardousProgressBar.value = depositedHazardous;
        organicProgressBar.value = depositedOrganic;
        paperProgressBar.value = depositedPaper;
        oilProgressBar.value = depositedOil;

        // Update total progress bar using sum of all collected trash
        totalProgressBar.value = depositedPlastic + depositedHazardous + depositedOrganic + depositedPaper + depositedOil;
    }
    
}
