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

		#endregion
	}
}
