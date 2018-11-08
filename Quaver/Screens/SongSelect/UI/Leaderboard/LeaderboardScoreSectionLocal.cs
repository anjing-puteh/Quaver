﻿using System.Collections.Generic;
using Quaver.Database.Maps;
using Quaver.Database.Scores;
using Wobble.Logging;

namespace Quaver.Screens.SongSelect.UI.Leaderboard
{
    public class LeaderboardScoreSectionLocal : LeaderboardScoreSection
    {
        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override LeaderboardType Type { get; } = LeaderboardType.Local;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="leaderboard"></param>
        public LeaderboardScoreSectionLocal(LeaderboardContainer leaderboard) : base(leaderboard)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public override FetchedScoreStore FetchScores()
        {
            var map = MapManager.Selected.Value;

            if (ScoreCache.ContainsKey(map))
                return ScoreCache[map];

            var scores = LocalScoreCache.FetchMapScores(map.Md5Checksum);
            ScoreCache[map] = new FetchedScoreStore(scores);

            return ScoreCache[map];
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public override string GetNoScoresAvailableString(Map map) => "No local scores available. Be the first!";
    }
}