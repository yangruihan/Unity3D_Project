using UnityEngine;
using System.Collections;

namespace Ruihanyang.Game
{
	/// <summary>
	/// Tile 类
	/// </summary>
	public class Tile : MonoBehaviour
	{
		private Animator animator;

		#region 回调函数

		void Awake ()
		{
			animator = GetComponent<Animator> ();
		}

		void OnTriggerExit2D (Collider2D other)
		{
			if (other.tag == "Player") {
				Player _player = other.GetComponent<Player> ();
				_player.AddScore (1);

				animator.SetBool ("isDisapear", true);
				GameManager.Instance.tileManager.Remove (this.gameObject);
			}
		}

		#endregion

		#region 自定义函数

		public void DestroySelf ()
		{
			Destroy (this.gameObject);
		}

		#endregion
	}
}
