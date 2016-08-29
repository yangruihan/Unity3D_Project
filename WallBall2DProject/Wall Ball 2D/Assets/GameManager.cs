using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ruihanyang.Game
{
	/// <summary>
	/// 游戏管理类
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		static public GameManager Instance = null;

		#region 管理类

		// Tile 管理类
		public Stack<GameObject> tileManager;

		#endregion

		#region 预置体

		[SerializeField]
		private GameObject[] tilePrefabs;

		#endregion

		// 当前场上最多存在的 Tile 数量
		[SerializeField]
		private int currentMaxTileCount = 10;

		// 出生点
		private Vector3 startPosition = Vector3.zero;
		// 当前位置
		private Vector3 actualPosition = Vector3.zero;

		#region 回调函数

		void Awake ()
		{
			// 单例模式
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy (gameObject);
			}

			BuildTile ();
		}

		#endregion


		#region 自定义函数

		// 构造 Tile
		void BuildTile ()
		{
			int idx = Random.Range (0, tilePrefabs.Length);

			GameObject temp = tilePrefabs [idx];
		}

		#endregion
	}
}
