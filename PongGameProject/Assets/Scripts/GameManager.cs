using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	public static GameManager Instance {
		get { 
			return _instance;
		}
	}

	private BoxCollider2D upWall;
	private BoxCollider2D downWall;
	private BoxCollider2D leftWall;
	private BoxCollider2D rightWall;

	public Transform player1;
	public Transform player2;

	private int score1 = 0;
	private int score2 = 0;

	public Text score1Text;
	public Text score2Text;

	void Awake () {
		_instance = this;
	}

	void Start () {
		ResetWall ();
		ResetPlayer ();
	}
	
	void Update () {
	
	}

	void ResetWall () {
		upWall = transform.Find ("UpWall").GetComponent<BoxCollider2D> ();
		downWall = transform.Find ("DownWall").GetComponent<BoxCollider2D> ();
		leftWall = transform.Find ("LeftWall").GetComponent<BoxCollider2D> ();
		rightWall = transform.Find ("RightWall").GetComponent<BoxCollider2D> ();

		Vector3 tempPosition = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));

		upWall.transform.position = new Vector3 (0, tempPosition.y + 0.5f, 0);
		upWall.size = new Vector2 (tempPosition.x * 2f, 1);

		downWall.transform.position = new Vector3 (0, - tempPosition.y - 0.5f, 0);
		downWall.size = new Vector2 (tempPosition.x * 2f, 1);

		leftWall.transform.position = new Vector3 (-tempPosition.x - 0.5f, 0);
		leftWall.size = new Vector2 (1, tempPosition.y * 2f);

		rightWall.transform.position = new Vector3 (tempPosition.x + 0.5f, 0);
		rightWall.size = new Vector2 (1, tempPosition.y * 2f);
	}

	void ResetPlayer () {
		Vector3 player1Pos = Camera.main.ScreenToWorldPoint (new Vector3 (100f, Screen.height / 2f, 0));
		player1Pos.z = 0f;
		player1.position = player1Pos;

		Vector3 player2Pos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width - 100f, Screen.height / 2f, 0));
		player2Pos.z = 0f;
		player2.position = player2Pos;
	}

	public void ChangeScore (string wallName) {
		if (wallName.Equals ("LeftWall")) {
			score2++;
		} else if (wallName.Equals ("RightWall")) {
			score1++;
		}

		score1Text.text = score1.ToString ();
		score2Text.text = score2.ToString ();
	}

	public void Reset () {
		score1 = 0;
		score2 = 0;
		score1Text.text = score1.ToString ();
		score2Text.text = score2.ToString ();

		GameObject.Find ("Ball").SendMessage ("Reset");
	}
}
