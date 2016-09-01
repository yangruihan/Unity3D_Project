using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
	public class CameraFollow : MonoBehaviour
	{
		private GameObject player;

		#region 回调函数

		void Update ()
		{
			if (player != null) {
				Vector3 _pos = player.transform.position;
				_pos.z = -10f;
				transform.position = _pos;
			}
		}

		#endregion


		#region 自定义函数

		public void SetPlayer (GameObject _player)
		{
			player = _player;
		}

		#endregion
	}
}
