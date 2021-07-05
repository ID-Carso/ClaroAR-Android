using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameCtrl : MonoBehaviour
{
    public List<GameObject> ObjectsInGame;

    public List<float> LevelsDuration;
    public float[] ObjecstCreationSpeed;
    public Transform[] GameSpots;
    public Animator ScenarioAnimator;

    int CurrentLevel;
    float CurrentLevelDuration;
    float Score;
    bool GameActive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameActive)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                bool isvalid = Physics.Raycast(ray, out hit, 200);
                if (isvalid)
                {
                    InteractableCtrl touchedObject = hit.transform.gameObject.GetComponent<InteractableCtrl>();

                    if (touchedObject != null)
                    {
                        if (touchedObject.CanBeTouched)
                        {
                            touchedObject.animator.SetBool("Animattion", true);
                            touchedObject.CanBeTouched = false;
                            touchedObject.DestroyBox();
                            if (touchedObject.IsNegativePoint)
                            {
                                Score = Score - touchedObject.Point;
                                if (Score <= 0) {
                                    Score = 0;
                                }
                            }
                            else
                            {
                                Score = Score + touchedObject.Point;
                            }
                            var scoretText = GameObject.FindGameObjectWithTag("ScoreText");
                            if (scoretText != null)
                            {
                                scoretText.GetComponent<Text>().text = Score.ToString();
                            }

                            if (Score <= 0)
                            {
                                FinishGame();
                            }
                        }
                    }
                }
            }
        }
    }

    public void StartGame()
    {
        Score = 0;
        var scoretText = GameObject.FindGameObjectWithTag("ScoreText");
        if (scoretText != null)
        {
            scoretText.GetComponent<Text>().text = Score.ToString();
        }
        GameActive = true;
        CurrentLevel = 0;
        CurrentLevelDuration = LevelsDuration[CurrentLevel];
        StartCoroutine(LevelTimer());
        StartCoroutine(CreateBoxes());
    }

    IEnumerator CreateBoxes()
    {
        while (GameActive)
        {
            for (int i = 0; i < GameSpots.Length; i++)
            {
                var rand = new System.Random();
                var index = rand.Next(0, ObjectsInGame.Count);
                ObjectsInGame[index].SetActive(true);

                GameObject gameObject = Instantiate(ObjectsInGame[index], GameSpots[i].position, Quaternion.identity);
                gameObject.transform.SetParent(GameSpots[i], false);
                gameObject.GetComponent<InteractableCtrl>().Point = rand.Next(1, 5);
                Destroy(gameObject, ObjecstCreationSpeed[CurrentLevel]);
                ObjectsInGame[index].SetActive(false);
            }

            yield return new WaitForSeconds(ObjecstCreationSpeed[CurrentLevel]);
        }
    }

    IEnumerator LevelTimer()
    {
        while (GameActive)
        {
            yield return new WaitForSeconds(1.0f);
            CurrentLevelDuration--;
            Mathf.Clamp(CurrentLevelDuration, 0, LevelsDuration[CurrentLevel]);
            if (CurrentLevelDuration == 0)
            {
                GameActive = !HasFinished();
                if (GameActive)
                {
                    CurrentLevel++;
                    CurrentLevelDuration = LevelsDuration[CurrentLevel];
                }
                else
                {
                    FinishGame();
                }
            }
        }
        yield return null;
    }

    private bool HasFinished()
    {
        return CurrentLevel == (LevelsDuration.Count - 1);
    }

    private void FinishGame()
    {
        //Score = 0;
        GameActive = false;
        ScenarioAnimator.SetTrigger("GameOver");
    }

}
