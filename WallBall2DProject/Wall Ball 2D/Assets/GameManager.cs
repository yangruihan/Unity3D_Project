﻿using UnityEngine;
using UnityEngine.UI;
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
		public List<GameObject> tileManager;

		public Player player;

		#endregion

		#region 预置体

		[SerializeField]
		private GameObject[] tilePrefabs;

		[SerializeField]
		private GameObject playerPrefab;

		#endregion

		// 当前场上最多存在的 Tile 数量
		[SerializeField]
		private int currentMaxTileCount = 10;

		[SerializeField]
		private Text scoreText;

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

			Init ();
		}

		void Update ()
		{
			if (tileManager.Count < currentMaxTileCount) {
				BuildTile ();
			}
		}

		#endregion

		#region 自定义公有函数

		public void UpdateScoreText (int _score)
		{
			scoreText.text = _score + "";
		}

		#endregion

		#region 自定义私有函数

		/// <summary>
		/// 初始化函数
		/// </summary>
		void Init ()
		{
			actualPosition = startPosition;

			for (int i = 0; i < currentMaxTileCount; i++) {
				BuildTile ();
			}

			GameObject _temp = Instantiate (playerPrefab, startPosition, Quaternion.identity) as GameObject;

			_temp.name = "Player";

			player = _temp.GetComponent<Player> ();

			player.Init (tileManager [1].transform.position);
		}

		/// <summary>
		/// 构造 Tile
		/// </summary>
		void BuildTile ()
		{
			int _idx = Random.Range (0, tilePrefabs.Length);

			GameObject _temp = tilePrefabs [_idx];

			_temp = Instantiate (_temp, actualPosition, Quaternion.identity) as GameObject;

			_temp.name = "Tile";

			tileManager.Add (_temp);

			CalNextTilePos ();
		}

		/// <summary>
		/// 计算下一块 Tile 的位置
		/// </summary>
		void CalNextTilePos ()
		{
			int _rnd = Random.Range (0, 100);

			if (_rnd < 50) {
				actualPosition += new Vector3 (1f, 0, 0);
			} else {
				actualPosition += new Vector3 (0, 1f, 0);
			}
		}

		#endregion
	}
}
