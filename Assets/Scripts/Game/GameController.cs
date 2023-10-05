using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using DG.Tweening;
public class GameController : MonoBehaviour 
{
	public event Action OnGameFinish;

    [SerializeField]
    private TextMeshProUGUI _timerText;
    [SerializeField]
    private TextMeshProUGUI _movesText;
	[SerializeField]
	private TextMeshProUGUI _playerName;

    private float timer;
    private int moves;
    private bool gameStarted;

    public GameObject[] cells;
	private GameObject[] random_cells;
	private GameObject[] r_cells;
	private int[,] r_grid;
	public static GameObject[,] grid;
	public static Vector3[,] position;
	public int[] checkmas;
	private static bool win=false;

	public float startPosX;
	public float startPosY;
	public float offsetX;
	public float offsetY;

	private int sum = 0;
	private int zero = 0;
	private int h;
	private int v;

	private static GameObject txt;
	private Component[] boxes;
	private Component[] p_script;

    private Tween _bounceTween;
    public float bounceDuration = 0.2f;
    public float bounceScale = 1.2f;

    private SoundManager _soundManager;

    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

		UpdateMovesText();
        _soundManager = FindObjectOfType<SoundManager>();
    }

    private void Start()
    {
        Init();
    }

    void Update() 
	{
        if (gameStarted)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int hours = Mathf.FloorToInt(timer / 3600); // Calculate hours
        int minutes = Mathf.FloorToInt((timer % 3600) / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(timer % 60); // Calculate seconds

        // Use string interpolation to format the time as "00:00:00"
        _timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void UpdateMovesText()
    {
        if (_movesText != null)
        {
            _movesText.text = moves.ToString();
			if (moves > 0)
			{

                if (_bounceTween != null && _bounceTween.IsActive())
                {
                    _bounceTween.Kill(); // Отменяет текущую анимацию.
                }

                _bounceTween = _movesText.transform.DOScale(Vector3.one * bounceScale, bounceDuration)
            .SetLoops(2, LoopType.Yoyo) // Две итерации (возврат к исходному размеру и обратно).
            .SetEase(Ease.OutQuad) // Кривая для плавного эффекта.
            .OnComplete(() =>
            {
                // По завершении анимации, возвращаем масштаб к исходному значению.
                _movesText.transform.localScale = Vector3.one;
            });
            }
        }
    }

    public void Init()
	{
		_playerName.text = GlobalVariables.UserName;

        timer = 0;
        moves = 0;
        UpdateMovesText();
        gameStarted = true;

        r_grid = new int[4, 4];
        txt = GameObject.FindGameObjectWithTag("congratulations");
        boxes = GetComponentsInChildren<BoxCollider2D>();
        p_script = GetComponentsInChildren<Puzzle>();
        checkmas = new int[16];
        random_cells = new GameObject[cells.Length];
        r_cells = new GameObject[cells.Length];
        float posXreset = startPosX;
        position = new Vector3[4, 4];
        for (int y = 0; y < 4; y++)
        {
            startPosY -= offsetY;
            for (int x = 0; x < 4; x++)
            {
                startPosX += offsetX;
                position[x, y] = new Vector3(startPosX, startPosY, 0);
            }
            startPosX = posXreset;

        }
        RandomPuzzle(true);
    }

	public void StartNewGame()
	{
        timer = 0;
		moves = 0;
        UpdateMovesText();
        gameStarted = true;

        win = false;
		//txt.GetComponent<Text> ().color = new Color (0, 0, 0, 0);
		RandomPuzzle (true);
    }

	public void ExitGame()
	{
		Application.Quit ();
	}

	public void Possibility(int[] mas){
		
		//Четное - есть решение, нечетное - нету
		for (int i = 0; i < 16; i++) {
			if (mas [i] == 0) {
				zero = i/4+1;
			}
			else 
				for (int k = i; k < 16; k++)
				{
					if (mas [i] > mas[k]&& mas[k]!=0) 
					{
						sum++;
					}
				}
		}

		if ((zero + sum) % 2 == 0) {
			Debug.Log ("There is a solution");
		} else {			
			Debug.Log ("There is NO solution... Reshuffling... ");
			CreatePuzzle ();
		}
	}

	public void RestartGame(){
		if(transform.childCount > 0)
		{
			// удаление старых объектов, если они есть
			for(int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		grid = new GameObject[4,4];
		GameObject clone = new GameObject();

		int i = 0;
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{		
				int j = checkmas [i];
				if (j>=0) {
					grid [x, y] =	Instantiate (cells [j], position [x, y], Quaternion.identity) as GameObject;
					grid [x, y].name = "ID-" + i;
					grid [x, y].transform.parent = transform;
				}
					i++;
			}
		}
		//Destroy(clone);

	}

	void CreatePuzzle()
	{
		if(transform.childCount > 0)
		{
			// удаление старых объектов, если они есть
			for(int j = 0; j < transform.childCount; j++)
			{
				Destroy(transform.GetChild(j).gameObject);
			}
		}
		int i = 0;
		int ii = 0;
		grid = new GameObject[4,4];
		h = Random.Range(0,3);
		v = Random.Range(0,3);

		GameObject clone = new GameObject();
		grid[h,v] = clone; // размещаем пустой объект в случайную клетку
		float posXreset=startPosX;

		for(int y = 0; y < 4; y++)
		{		

			for(int x = 0; x < 4; x++)
			{
				
				if (grid [x, y] == null) {			
					startPosX += offsetX;
					grid [x, y] = Instantiate (random_cells [i], position [x, y], Quaternion.identity) as GameObject;
					grid [x, y].name = "ID-" + i;
					checkmas [ii] = grid [x, y].GetComponent<Puzzle> ().ID;
					grid [x, y].transform.parent = transform;
					i++;
					ii++;
				} else {
					checkmas [ii] = 0;
					ii++;
				}
			}
		}
		foreach (BoxCollider2D box2d in boxes)
			box2d.enabled = true;
		foreach (Puzzle puz in p_script)
			puz.enabled = true;


		for (i = 0; i < 16; i++) {
			print ("checkmas   "+checkmas[i]);
		}


		Destroy(clone); 
		for(int q = 0; q < cells.Length; q++)
		{
			Destroy(random_cells[q]);
		}
		Possibility (checkmas);
	}

	void RandomPuzzle(bool r_s)
	{
		if (r_s == true) {
		int[] tmp = new int[cells.Length];
		for(int i = 0; i <cells.Length; i++)
		{
			tmp[i] = 1;
		}
		int c = 0;
		while(c < cells.Length)
		{
			int r = Random.Range(0, cells.Length);
			if(tmp[r] == 1)
			{ 
				random_cells[c] = Instantiate(cells[r], new Vector3(0, 10, 0), Quaternion.identity) as GameObject;	
					r_cells [c] = random_cells [c];
				tmp[r] = 0;
				c++;
			}
		}
			CreatePuzzle ();
		} else {
			CreatePuzzle ();
		}
	}

	 public void GameFinish()
	 {

        SoundManager.PlaySfx(_soundManager.soundClick);

        int i = 1;
		for(int y = 0; y < 4; y++)
		{
			for(int x = 0; x < 4; x++)
			{
				if(grid[x,y]) { if(grid[x,y].GetComponent<Puzzle>().ID == i) i++; } else i--;
			}
		}
		
		if(i == 15)
		{
			for(int y = 0; y < 4; y++)
			{
				for(int x = 0; x < 4; x++)
				{
					if(grid[x,y]) Destroy(grid[x,y].GetComponent<Puzzle>());
				}
			}

            if (transform.childCount > 0)
            {
                // удаление старых объектов, если они есть
                for (int j = 0; j < transform.childCount; j++)
                {
                    Destroy(transform.GetChild(j).gameObject);
                }
            }


            win = true;
			txt.GetComponent<Text> ().color = new Color (0, 0, 0, 1);

            gameStarted = false;

            Debug.Log("Finish!");
			OnGameFinish?.Invoke();


        }
		if (gameStarted)
		{
			moves++;
			UpdateMovesText();
		}
    }

	public void ForceGameFinish()
	{
        //      if (transform.childCount > 0)
        //      {
        //          // удаление старых объектов, если они есть
        //          for (int j = 0; j < transform.childCount; j++)
        //          {
        //              Destroy(transform.GetChild(j).gameObject);
        //          }
        //      }

        //      win = true;
        //      txt.GetComponent<Text>().color = new Color(0, 0, 0, 1);

        //gameStarted = false;

        //      Debug.Log("Finish!");
        //      OnGameFinish.Invoke();

        int i = 1;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                if (grid[x, y]) { if (grid[x, y].GetComponent<Puzzle>().ID == i) i++; } else i--;
            }
        }

		i = 15;

        if (i == 15)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (grid[x, y]) Destroy(grid[x, y].GetComponent<Puzzle>());
                }
            }

            if (transform.childCount > 0)
            {
                // удаление старых объектов, если они есть
                for (int j = 0; j < transform.childCount; j++)
                {
                    Destroy(transform.GetChild(j).gameObject);
                }
            }


            win = true;
            //txt.GetComponent<Text>().color = new Color(0, 0, 0, 1);

            gameStarted = false;



            Debug.Log("Finish!");
            OnGameFinish?.Invoke();
        }
        if (gameStarted)
        {
            moves++;
            UpdateMovesText();
        }
    }

}
