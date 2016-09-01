using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
	[RequireComponent (typeof(Player))]
	[RequireComponent (typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		private PlayerMotor motor;

		#region 回调函数

		void Awake ()
		{
			motor = GetComponent<PlayerMotor> ();
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Space)) {
				motor.ChangeDirection ();
			}
		}

		#endregion

		#region 自定义函数

		public void Init (Vector3 _initDirection)
		{
			motor.Init (_initDirection);
		}

		public void AddSpeed (float _addValue)
		{
			motor.AddSpeed (_addValue);
		}

		#endregion
	}
}