using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform _playerOne;
    private Transform _playerTwo;

    [SerializeField] private float _speed;
    [SerializeField] private float _limitX;

    private int _scoreJ1;
    private int _scoreJ2;

    [SerializeField] private TextMeshProUGUI _centralText;
    [SerializeField] private TextMeshProUGUI _scoreJ1Text;
    [SerializeField] private TextMeshProUGUI _scoreJ2Text;
    [SerializeField] private GameObject _restartButton;

    public bool GameIsActive { get; private set; }

    private Ball _ball;

    public int DirectionPlayerOne { get; private set; }
    public int DirectionPlayerTwo { get; private set; }


    void Start()
    {
        _playerOne = GameObject.Find("PlayerOne").GetComponent<Transform>();
        _playerTwo = GameObject.Find("PlayerTwo").GetComponent<Transform>();
        _ball = GameObject.Find("Ball").GetComponent<Ball>();
        StartGame();
    }


    public void StartGame()
    {
        RefreshScore();
        _restartButton.SetActive(false);
        _centralText.gameObject.SetActive(false);
        GameIsActive = true;
        _ball.StartOrRestartGame();
    }


    void Update()
    {
        if (GameIsActive)
        {
            MovePlayerOne();
            MovePlayerTwo();
        }
    }

    private void MovePlayerOne()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && _playerOne.position.x > -_limitX)
        {
            _playerOne.Translate(-_speed * Time.deltaTime, 0, 0);
            DirectionPlayerOne = -1;
        }

        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && _playerOne.position.x < _limitX)
        {
            _playerOne.Translate(_speed * Time.deltaTime, 0, 0);
            DirectionPlayerOne = 1;
        }

        else DirectionPlayerOne = 0;

    }

    private void MovePlayerTwo()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.X) && _playerTwo.position.x > -_limitX)
        {
            _playerTwo.Translate(-_speed * Time.deltaTime, 0, 0);
            DirectionPlayerTwo = -1;
        }

        else if (Input.GetKey(KeyCode.X) && !Input.GetKey(KeyCode.W) && _playerTwo.position.x < _limitX)
        {
            _playerTwo.Translate(_speed * Time.deltaTime, 0, 0);
            DirectionPlayerTwo = 1;
        }
        else DirectionPlayerTwo = 0;
    }


    public void ScoreEvolue(int joueur)
    {
        _restartButton.SetActive(true);
        _centralText.gameObject.SetActive(true);

        if (GameIsActive && joueur == 1) { _scoreJ1++; RefreshScore(); GameIsActive = false; }
        if (GameIsActive && joueur == 2) { _scoreJ2++; RefreshScore(); GameIsActive = false; }

        if (_scoreJ1 < 5 && _scoreJ2 < 5)
        {
            _centralText.text = "Joueur " + joueur + " marque un point !";
        }
        else
        {
            _centralText.text = "Joueur " + joueur + " gagne la partie !";
            _scoreJ1 = 0;
            _scoreJ2 = 0;
        }
    }
       
    public int DirectionOfPlayer(int joueur)
    {
        if (joueur == 1) return DirectionPlayerOne;
        else if (joueur == 2) return DirectionPlayerTwo;
        else { Debug.LogError("Erreur, il n'y a que deux jouers !"); return 0; }
    }


    private void RefreshScore()
    {
        _scoreJ1Text.text = "Joueur 1 : " + _scoreJ1;
        _scoreJ2Text.text = "Joueur 2 : " + _scoreJ2;
    }
}
