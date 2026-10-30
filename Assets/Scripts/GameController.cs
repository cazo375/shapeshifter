using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    static GameObject instance;

    //Game Data
    public ProgressionKeeper progressionKeeper;
    public ScoreKeeper scoreKeeper;
    public UserSettings userSettings;
    public OptionsSettings optionsSettings;
    public StoreKeeper storeKeeper;
    
    //Themes
    public AudioClip [] musicThemes;
    public GameObject [] playerPrefabs;
    public GameObject [] wallPrefabs;
    private Animator backgroundAnimator;

    //Coin
    public GameObject coinPrefab;
    
    //Game Logic
    private GameObject obstacleContainer;
    private GameObject spawnLocation;
    
    private bool isWallActive;
    public int currentScore;
    public float currentDropSpeed;
    public float deathHeight = -6.0f;

    public int MaxCoinsOnScreen = 1;
    private bool isGeneratingCoins;
    private int activeCoinCount;

    private bool isGameOver;

    //Player
    private GameObject playerInstance;
    private GameObject playerPrefab;
    private Player player;

    //Wall
    private GameObject wall;
    private GameObject obstaclePrefab;

    //Audio 
    private AudioSource musicPlayer;

    public AudioSource collidePlayer;
    public AudioClip [] collisionSounds;

    public AudioSource wallPassPlayer;
    public AudioClip [] wallPassSounds;
    private int wallPassSoundIndex;
    private int wallPassSoundDirection;

    public AudioSource coinPickupPlayer;
    public AudioClip [] coinPickupSounds;

    //Explosion FXs
    public GameObject explosionPrefab;
    public GameObject wallExplosionPrefab;
    private GameObject explosionInstance;

    //Menu States
    private bool isInMenus;
    private bool isInGameUIHidden;

    //Menu Prefabs
    public GameObject homeMenuPrefab;
    public GameObject instructionsMenuPrefab;
    public GameObject themesMenuPrefab;
    public GameObject storeMenuPrefab;
    public GameObject optionsMenuPrefab;
    public GameObject creditsMenuPrefab;
    public GameObject deathMenuPrefab;
    public GameObject ConfirmWindowPrefab;

    //Menu Containers
    private GameObject currentActiveMenu;
    private GameObject currentActiveWindow;
    public GameObject totalCoinsUI;
    public GameObject inGameUI;   

    //UI Text
    private Text scoreText;
    private Text bestScoreText;
    private Text coinsText;
    private Text bestCoinsText;
    private Text gameOverTextObject;
    

    

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
//    MAIN GAME LOOP
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
    
    void Start()
    {      

        if(instance != null){
		    Destroy(gameObject);
		 	return;
		}
		instance = gameObject;
		GameObject.DontDestroyOnLoad(gameObject);

        scoreKeeper = new ScoreKeeper();
        progressionKeeper = new ProgressionKeeper();
        userSettings = new UserSettings();
        optionsSettings = new OptionsSettings();
        storeKeeper = new StoreKeeper();

        musicPlayer = GetComponent<AudioSource>();
        obstacleContainer = GameObject.Find("ObjectContainer");
        spawnLocation = GameObject.Find("ObjectSpawnPoint");
        GameObject canvas = GameObject.Find("Canvas");
        scoreText = GameObject.Find("CurrentScoreText").GetComponent<Text>();
        bestScoreText = GameObject.Find("BestScoreText").GetComponent<Text>();
        coinsText = GameObject.Find("CurrentCoinsText").GetComponent<Text>();
        bestCoinsText = GameObject.Find("BestCoinsText").GetComponent<Text>();
        gameOverTextObject = GameObject.Find("GameOverText").GetComponent<Text>();
        backgroundAnimator = GameObject.Find("Background").GetComponent<Animator>();

        isGameOver = true;
        isGeneratingCoins = false;
        activeCoinCount = 0;
        isWallActive = true;
        isInMenus = true;
        isInGameUIHidden = true;
        currentActiveMenu = Instantiate(homeMenuPrefab);

        SetMusicTheme(userSettings.GetSelectedMusicTheme());
        SetBackgroundTheme(userSettings.GetSelectedBackgroundTheme());
        SetWallTheme(userSettings.GetSelectedWallTheme());
        SetShapesTheme(userSettings.GetSelectedShapesTheme());

        collidePlayer.volume = optionsSettings.GetEffectsVolume();
        wallPassPlayer.volume = optionsSettings.GetEffectsVolume();
        coinPickupPlayer.volume = optionsSettings.GetEffectsVolume();

        if(PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            //First launch

            PlayerPrefs.SetInt("FirstLaunch", 1);
            UnlockRandomStartingTheme();

        }

    }

///////////////////////////////////////////////////////////////////////////////////

    public void RestartGame()
    {
        if(!isGameOver) return;
        isGameOver = false;
        isWallActive = false;
        isGeneratingCoins = true;
        MaxCoinsOnScreen = 1;
        activeCoinCount = 0;
        currentScore = 0;
        currentDropSpeed = -5;
        wallPassSoundDirection = -1;
        wallPassSoundIndex = 0;

        isInMenus = false;
        isInGameUIHidden = false;
        if(gameOverTextObject != null)
        {
            gameOverTextObject.text = "";
        }
        Destroy(currentActiveMenu);

        SpawnPlayer();
        
    }

///////////////////////////////////////////////////////////////////////////////////

    void Update()
    {
        if(isInGameUIHidden)
        {
            inGameUI.SetActive(false);
            totalCoinsUI.SetActive(true);
        }
        else
        {
            inGameUI.SetActive(true);
            totalCoinsUI.SetActive(false);
            UpdateText();
        }

        if(isGameOver)
        {
            for(int i = 0; i < obstacleContainer.transform.childCount; i++){
                GameObject currentObstacle = obstacleContainer.transform.GetChild(i).gameObject;
                Destroy(currentObstacle);
            }
        }

        if(isGeneratingCoins && activeCoinCount < MaxCoinsOnScreen)
        {
            QueCoinSpawn();
        }

        //spawn Obstacle
        if(!isWallActive)
        {
            SpawnObstacle();
        }

        //Cleanup Obstacles
        for(int i = 0; i < obstacleContainer.transform.childCount; i++)
        {
            GameObject currentObstacle = obstacleContainer.transform.GetChild(i).gameObject;
            if(currentObstacle.transform.position.y < (deathHeight) )
            {
                if(currentObstacle.GetComponent<WallObject>() != null)
                    isWallActive = false;
                if(currentObstacle.GetComponent<Coin>() != null)
                    activeCoinCount--;
                Destroy(currentObstacle);
            }
        }
    }

///////////////////////////////////////////////////////////////////////////////////

    public void GameOver(){
        if(isGameOver) return;

        isGameOver = true;
        isInMenus = true;

        //Cleanup Obstacles
        for(int i = 0; i < obstacleContainer.transform.childCount; i++){
            GameObject currentObstacle = obstacleContainer.transform.GetChild(i).gameObject;
            Destroy(currentObstacle);

        }
        isWallActive = true;
        scoreKeeper.CompareAgainstBestScore(currentScore);
        scoreKeeper.CompareAgainstBestCoins(player.GetCurrentCoins());
        scoreKeeper.AddCoins(player.GetCurrentCoins());
        player.ResetCurrentCoins();

        Destroy(playerInstance);

        //Play an add
        //GameObject.Find("AdvertismentController").GetComponent<AdvertismentController>().ShowAnAdd();


        if(gameOverTextObject != null)
        {
            gameOverTextObject.text = "Game Over!";
        }
        currentActiveMenu = Instantiate(deathMenuPrefab);
    }

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
//    MENU FUNCTIONS
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////

    public void HideMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        isInMenus = false;
        isInGameUIHidden = false;
    }

    public void SwapToHomeMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        if(currentActiveWindow != null)
        {
            Destroy(currentActiveWindow);
        }
        currentActiveMenu = Instantiate(homeMenuPrefab);
        isInGameUIHidden = true;
    }

    public void SwapToInstructionsMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        currentActiveMenu = Instantiate(instructionsMenuPrefab);
        isInGameUIHidden = true;
    }

    public void SwapToThemesMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        currentActiveMenu = Instantiate(themesMenuPrefab);
    }

    public void SwapToStoreMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        currentActiveMenu = Instantiate(storeMenuPrefab);
    }

    public void SwapToOptionsMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        currentActiveMenu = Instantiate(optionsMenuPrefab);
    }

    public void SwapToCreditsMenu()
    {
        if(currentActiveMenu != null)
        {
            Destroy(currentActiveMenu);
        }
        currentActiveMenu = Instantiate(creditsMenuPrefab);
    }

    public void CreateItemConfirmWindow(int theme, string name, int cost, ItemType type, GameObject button){
        if(currentActiveWindow != null)
        {
            Destroy(currentActiveWindow);
        }
        
        currentActiveWindow = Instantiate(ConfirmWindowPrefab);
        currentActiveWindow.GetComponent<ConfirmWindowController>().ItemSetup(theme, name, cost, type, button);
        currentActiveWindow.GetComponent<RectTransform>().SetParent(GameObject.Find("PopupSpawner").GetComponent<RectTransform>(), false);
    }

    public void CreateThemeConfirmWindow(int theme, string name, int cost, GameObject button){
        if(currentActiveWindow != null)
        {
            Destroy(currentActiveWindow);
        }
        currentActiveWindow = Instantiate(ConfirmWindowPrefab);
        currentActiveWindow.GetComponent<ConfirmWindowController>().ThemeSetup(theme, name, cost, button);
        currentActiveWindow.GetComponent<RectTransform>().SetParent(GameObject.Find("PopupSpawner").GetComponent<RectTransform>(), false);
    }

    public void CloseConfimWindow()
    {
        if(currentActiveWindow != null)
        {
            Destroy(currentActiveWindow);
        }
    }

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
//    SETTINGS FUNCTIONS
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


    public int GetItemPrice(int index, ItemType type)
    {
        int itemCost = 0;
        itemCost = storeKeeper.GetItemPrice(index, type, scoreKeeper.GetBestScore());
        //Debug.Log("Get Item Cost: " + itemCost.ToString());
        return itemCost;
    }

    public int GetThemePrice(int index)
    {
        return storeKeeper.GetThemePrice(index, scoreKeeper.GetBestScore());
    }

    public int GetThemeDiscountUnlockScores(int index)
    {
        return storeKeeper.GetThemeDiscountUnlockScores(index);
    }

    public int GetThemeCostPreDiscount(int index)
    {
        return storeKeeper.GetThemeCostPreDiscount(index);
    }

    public bool IsDiscountUnlocked(int index)
    {
        return storeKeeper.IsDiscountUnlocked(index, scoreKeeper.GetBestScore());
    }


    private void UnlockRandomStartingTheme()
    {
        int [] possibleStartingThemes = new int[19];
        possibleStartingThemes[0] = 2;    possibleStartingThemes[1] = 4;    possibleStartingThemes[2] = 6;    possibleStartingThemes[3] = 8;
        possibleStartingThemes[4] = 10;   possibleStartingThemes[5] = 12;   possibleStartingThemes[6] = 14;   possibleStartingThemes[7] = 15;
        possibleStartingThemes[8] = 16;   possibleStartingThemes[9] = 17;   possibleStartingThemes[10] = 19;  possibleStartingThemes[11] = 20;
        possibleStartingThemes[12] = 21;  possibleStartingThemes[13] = 23;  possibleStartingThemes[14] = 24;  possibleStartingThemes[15] = 25;
        possibleStartingThemes[16] = 26;  possibleStartingThemes[17] = 27;  possibleStartingThemes[18] = 28;

        int choice = ((int)Random.Range(0,19));
        progressionKeeper.LockEverything();
        progressionKeeper.UnlockATheme(possibleStartingThemes[choice]);
        //Debug.Log("Random Choice: " + possibleStartingThemes[choice].ToString());

        SetTheme(possibleStartingThemes[choice], ItemType.Music);
        SetTheme(possibleStartingThemes[choice], ItemType.Background);
        SetTheme(possibleStartingThemes[choice], ItemType.Wall);
        SetTheme(possibleStartingThemes[choice], ItemType.Shapes);
        
    }

    public bool UnlockAnItem(int index, ItemType type)
    {
        int itemCost = 0;
        itemCost = GetItemPrice(index, type);
        if(scoreKeeper.GetTotalCoins() >= itemCost)
        {
            scoreKeeper.SpendCoins(itemCost);
            progressionKeeper.UnlockAnItem(index, type);
            return true;
        }

        return false;
    }

    public bool UnlockATheme(int index)
    {
        int themeCost = 0;
        themeCost = GetThemePrice(index);
        if(scoreKeeper.GetTotalCoins() >= themeCost)
        {
            scoreKeeper.SpendCoins(themeCost);
            progressionKeeper.UnlockATheme(index);
            return true;
        }

        return false;
    }

    public void SetTheme(int index, ItemType type)
    {
        switch (type)
        {
            case ItemType.Music:
                SetMusicTheme(index);
                break;

            case ItemType.Background:
                SetBackgroundTheme(index);
                break;

            case ItemType.Wall:
                SetWallTheme(index);
                break;

            case ItemType.Shapes:
                SetShapesTheme(index);
                break;
            
            default:
                break;
        }
    }

    public void SaveCurrentThemeSettings()
    {
        userSettings.SaveCurrentThemeSettings();
    }

    public void SetMusicTheme(int musicThemeIndex)
    {
        if(musicPlayer == null) return;

        musicPlayer.Stop();
        musicPlayer.clip = musicThemes[musicThemeIndex];
        musicPlayer.Play();
        musicPlayer.volume = optionsSettings.GetMusicVolume();

        userSettings.SetMusicTheme(musicThemeIndex);
    }


    public void SetWallTheme(int wallThemeIndex)
    {
  
        obstaclePrefab = wallPrefabs[wallThemeIndex];
        userSettings.SetWallTheme(wallThemeIndex);
    }

    public void SetBackgroundTheme(int backgroundThemeIndex)
    {
        backgroundAnimator.SetInteger("ActiveBackground", backgroundThemeIndex);
        userSettings.SetBackgroundTheme(backgroundThemeIndex);
    }

    public void SetShapesTheme(int shapesThemeIndex)
    {
        playerPrefab = playerPrefabs[shapesThemeIndex];
        userSettings.SetShapeTheme(shapesThemeIndex);
    }

    public bool IsItemActive(int index, ItemType type){
        if(type == ItemType.Music && userSettings.GetSelectedMusicTheme() == index)
        {  
            return true;
        }

        else if(type == ItemType.Background && userSettings.GetSelectedBackgroundTheme() == index)
        {  
            return true;
        }

        else if(type == ItemType.Wall && userSettings.GetSelectedWallTheme() == index)
        {  
            return true;
        }

        else if(type == ItemType.Shapes && userSettings.GetSelectedShapesTheme() == index)
        {  
            return true;
        }

        return false;
    }

///////////////////////////////////////////////////////////////////////////////////

    public void SaveCurrentOptionsSettings()
    {
        optionsSettings.SaveCurrentOptionsSettings();
    }

    public void UpdateMusicVolume(float volume){
        musicPlayer.volume = volume;
		optionsSettings.SetMusicVolume(volume);
		
	}
	
	public void UpdateEffectsVolume(float volume){
        collidePlayer.volume = volume;
        wallPassPlayer.volume = volume;
        coinPickupPlayer.volume = volume;
		optionsSettings.SetEffectsVolume(volume);
	}

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
//    SPAWNING FUNCTIONS
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////

    void SpawnPlayer()
    {
        if(playerInstance != null)
        {
            Destroy(playerInstance);
        }
        playerInstance = Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        player = playerInstance.GetComponent<Player>();
    }

    void SpawnObstacle()
    {
        wall = Instantiate(obstaclePrefab);
        wall.transform.SetParent(obstacleContainer.transform);      
        wall.transform.position = spawnLocation.transform.position;
        wall.GetComponent<WallObject>().dropSpeed = currentDropSpeed;
        isWallActive = true;
    }


    public void SpawnExplosion()
    {
        if(explosionInstance == null)
        {
            explosionInstance = Instantiate(explosionPrefab, playerInstance.transform.position, Quaternion.identity);
            Destroy(explosionInstance, 1.5f);       
        }
    }

    public void SpawnWallExplosion()
    {
        if(explosionInstance == null)
        {
            Vector3 spawnPos = wall.transform.position;
            spawnPos.y = spawnPos.y + 1.0f;
            
            explosionInstance = Instantiate(wallExplosionPrefab, spawnPos, Quaternion.identity);
            Destroy(explosionInstance, 0.5f);       
        }
    }

    void QueCoinSpawn()
    {
        if(!isGeneratingCoins) return;
        activeCoinCount++;
        Invoke("SpawnCoin", Random.Range(0.0f, 2.25f));
    }

    void SpawnCoin()
    {
        if(!isGeneratingCoins) return;

        GameObject coin = Instantiate(coinPrefab);    
        coin.transform.SetParent(obstacleContainer.transform);
        coin.transform.position = spawnLocation.transform.position;
        
    }

    public void DecrementCoinCount()
    {
        activeCoinCount--;
    }

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
//    HELPERS
///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////

    public void IncrementScore(int value)
    {
        currentScore += value;
        currentDropSpeed = (-5.0f) - ((currentScore / 10) * 0.02f);
        MaxCoinsOnScreen = 1 + (currentScore / 25);

        UpdateText();
    }

    public void UpdateText()
    {
        if(scoreText == null || bestScoreText == null || coinsText == null || bestCoinsText == null) return;
        
        scoreText.text = "Score: " + currentScore.ToString();
        bestScoreText.text = "Best: " + scoreKeeper.GetBestScore().ToString();

        coinsText.text = "Coins: " + player.GetCurrentCoins().ToString();
        bestCoinsText.text = "Best: " + scoreKeeper.GetBestCoins().ToString();
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsInMenus()
    {
        return isInMenus;
    }


    //Sound Effect Ques
    public void PlayCollideSound()
    {
        int index = (int)Random.Range(0,3);
        collidePlayer.clip = collisionSounds[index];
        collidePlayer.Play();
    }

    public void PlayWallPassSound()
    {
        MoveWallPassSoundIndex();
        wallPassPlayer.clip = wallPassSounds[wallPassSoundIndex];
        wallPassPlayer.Play();
    }
    private void MoveWallPassSoundIndex()
    {
        wallPassSoundIndex = wallPassSoundIndex + wallPassSoundDirection;
        if(wallPassSoundIndex < 0)
        {
            wallPassSoundIndex = 1;
            wallPassSoundDirection = 1;
        }
        else if(wallPassSoundIndex > 12)
        {
            wallPassSoundIndex = 11;
            wallPassSoundDirection = -1;
        }
    }


    public void PlayCoinPickupSound()
    {
        int index = (int)Random.Range(0,2);
        coinPickupPlayer.clip = coinPickupSounds[index];
        coinPickupPlayer.Play();
    }

}

///////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////
