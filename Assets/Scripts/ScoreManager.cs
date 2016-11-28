using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text scoreText;
	public Text highScoreText;
    public Text heartText;

	public float scoreCount;
	public float highScoreCount;

	public float pointsPerSecond;

	public bool scoreIncreasing;

	// Use this for initialization
	void Start () {

		if (GameController.getScore () != 0) {
			scoreCount = GameController.getScore ();
		}
		
		if (PlayerPrefs.HasKey("HighScore")) {
			highScoreCount = PlayerPrefs.GetFloat ("HighScore");
		}
		else {
			highScoreCount = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (scoreIncreasing) {
			scoreCount += pointsPerSecond * Time.deltaTime;
		}

		if (scoreCount > highScoreCount) {
			highScoreCount = scoreCount;
			PlayerPrefs.SetFloat ("HighScore", highScoreCount);
		}

        if (PlayerPrefs.HasKey("Hearts"))
        {
            heartText.text = PlayerPrefs.GetInt("Hearts").ToString();
        }

		scoreText.text = "Score: " + Mathf.Round (scoreCount);
		highScoreText.text = "High Score: " + Mathf.Round (highScoreCount);
	}
}
