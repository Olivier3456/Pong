using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _speedX;
    private float _speedY;

    Vector3 _startBallPosition;

    private GameManager _gameManager;


    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _startBallPosition = transform.position;
    }


    public void StartOrRestartGame()
    {
        transform.position = _startBallPosition;

        _speedX = Random.Range(-_speed, _speed);

        _speedY = _speed;
        int departAleatoireY = Random.Range(-1, 1); if (departAleatoireY == -1) _speedY = -_speedY;
    }

    private void Update()
    {
        if (_gameManager.GameIsActive)
        {
            transform.Translate(_speedX * Time.deltaTime, _speedY * Time.deltaTime, 0);

            if (transform.position.y < -1) _gameManager.ScoreEvolue(2);
            else if (transform.position.y > 6) _gameManager.ScoreEvolue(1);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (_gameManager.GameIsActive)
        {
            if (collision.gameObject.CompareTag("Bordure")) _speedX = -_speedX;

            else if (collision.gameObject.CompareTag("Player"))
            {
                _speedY = -_speedY;

                float distanceToCenterOfPlayer = Vector3.Distance(transform.position, collision.gameObject.transform.position);
                //    Debug.Log("Distance entre la balle et le centre du joueur : " + distanceToCenterOfPlayer + "m");

                if (_speedX > 0) _speedX = distanceToCenterOfPlayer * 5f;
                else _speedX = -1 * distanceToCenterOfPlayer * 5f;


                if (transform.position.y > 2.5f)
                {
                    _speedX += _gameManager.DirectionOfPlayer(2) * _speedX;
                }

                else
                {
                    _speedX += _gameManager.DirectionOfPlayer(1) * _speedX;
                }



                /*
                if (transform.position.y > 2.5f)
                {
                    if (_speedX > 0 && _gameManager.DirectionOfPlayer(2) > 0)
                    {
                        _speedX *= 1.25f;
                        Debug.Log("Accélération");
                    }
                    else if (_speedX > 0 && _gameManager.DirectionOfPlayer(2) < 0)
                    {
                        _speedX *= 0.75f;
                        Debug.Log("Décélération");
                    }
                    else if (_speedX < 0 && _gameManager.DirectionOfPlayer(2) < 0)
                    {
                        _speedX *= 1.25f;
                        Debug.Log("Accélération");
                    }
                    else if (_speedX < 0 && _gameManager.DirectionOfPlayer(2) > 0)
                    {
                        _speedX *= 0.75f;
                        Debug.Log("Décélération");
                    }
                }
                else
                {
                    if (_speedX > 0 && _gameManager.DirectionOfPlayer(1) > 0)
                    {
                        _speedX *= 1.25f;
                        Debug.Log("Accélération");
                    }
                    else if (_speedX > 0 && _gameManager.DirectionOfPlayer(1) < 0)
                    {
                        _speedX *= 0.75f;
                        Debug.Log("Décélération");
                    }
                    else if (_speedX < 0 && _gameManager.DirectionOfPlayer(1) < 0)
                    {
                        _speedX *= 1.25f;
                        Debug.Log("Accélération");
                    }
                    else if (_speedX < 0 && _gameManager.DirectionOfPlayer(1) > 0)
                    {
                        _speedX *= 0.75f;
                        Debug.Log("Décélération");
                    }
                }

                */
            }
        }
    }
}
