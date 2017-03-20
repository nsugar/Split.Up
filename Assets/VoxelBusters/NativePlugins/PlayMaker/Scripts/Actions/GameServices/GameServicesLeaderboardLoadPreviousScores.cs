﻿using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Tooltip	= HutongGames.PlayMaker.TooltipAttribute;

namespace VoxelBusters.NativePlugins.PlayMaker
{
	[ActionCategory("VB - Cross Platform Native Plugins")]
	[Tooltip("Sends a request to load previous set of scores.")]
	public class GameServicesLeaderboardLoadPreviousScores : GameServicesLeaderboardLoadScoresBase 
	{
		#region FSM Methods
		
		public override void OnEnter ()
		{
#if USES_GAME_SERVICES
			Leaderboard _leaderboard	= CreateLeaderboard();

			// Start request
			_leaderboard.LoadMoreScores(eLeaderboardPageDirection.PREVIOUS, OnScoreLoadFinished);
#endif
		}
		
		#endregion
	}
}