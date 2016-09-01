using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
	public class PlayerMotor : MonoBehaviour
	{
		static public Vector3 DIRECTION_LEFT = new Vector3 (1f, 0f, 0f);
		static public Vector3 DIRECTION_RIGHT = new Vector3 (0f, 1f, 0f);

		[SerializeField]
		private float initSpeed = 5f;

		private float currentSpeed = 0f;

		private Vector3 direction = Vector3.zero;

		#region 身上组件

		private Rigidbody2D rigid;

		#endregion

		#region 回调函数

		void Start ()
		{
			rigid = GetComponent<Rigidbody2D> ();

			currentSpeed = initSpeed;
		}

		void Update ()
		{
			rigid.MovePosition (transform.position + direction * currentSpeed * Time.deltaTime);
		}

		#endregion

		#region 自定义函数

		public void Init (Vector3 _initDirection)
		{
			direction = _initDirection;
		}

		/// <summary>
		/// 改变移动方向
		/// </summary>
		public void ChangeDirection ()
		{
			if (direction == DIRECTION_LEFT) {
				direction = DIRECTION_RIGHT;
			} else {
				direction = DIRECTION_LEFT;
			}
		}

		/// <summary>
		/// 增加移动速度
		/// </summary>
		/// <param name="_addValue">Add value.</param>
		public void AddSpeed (float _addValue)
		{
			currentSpeed += _addValue;
		}

		#endregion
	}
}