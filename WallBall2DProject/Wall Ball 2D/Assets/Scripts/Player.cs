using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
	public class Player : MonoBehaviour
	{
		#region 脚本引用

		[HideInInspector]
		public PlayerController controller;

		#endregion

		// 玩家得分
		public int score = 0;

		#region 回调函数

		void Awake ()
		{
			controller = GetComponent<PlayerController> ();
		}

		#endregion

		#region 自定义函数

		public void Init (Vector3 _initDirection)
		{
			Camera.main.GetComponent<CameraFollow> ().SetPlayer (this.gameObject);

			controller.Init (_initDirection);
		}

		public void AddScore (int _score)
		{
			score += _score;

			GameManager.Instance.UpdateScoreText (score);
		}

		#endregion
	}
}
