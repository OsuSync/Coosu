﻿using Newtonsoft.Json;
using System;

namespace Coosu.Api.V2.ResponseModels
{
    public partial class User
    {
        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("default_group")]
        public string DefaultGroup { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonProperty("is_deleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("is_online")]
        public bool IsOnline { get; set; }

        [JsonProperty("is_supporter")]
        public bool IsSupporter { get; set; }

        [JsonProperty("last_visit")]
        public DateTimeOffset? LastVisit { get; set; }

        [JsonProperty("pm_friends_only")]
        public bool PmFriendsOnly { get; set; }

        [JsonProperty("profile_colour")]
        public string ProfileColor { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        [JsonProperty("cover_url")]
        public Uri CoverUrl { get; set; }

        [JsonProperty("discord")]
        public string Discord { get; set; }

        [JsonProperty("has_supported")]
        public bool HasSupported { get; set; }

        [JsonProperty("interests")]
        public string Interests { get; set; }

        [JsonProperty("join_date")]
        public DateTimeOffset? JoinDate { get; set; }

        [JsonProperty("kudosu")]
        public Kudosu Kudosu { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("max_blocks")]
        public int MaxBlocks { get; set; }

        [JsonProperty("max_friends")]
        public int MaxFriends { get; set; }

        [JsonProperty("occupation")]
        public string Occupation { get; set; }

        [JsonProperty("playmode")]
        public string Playmode { get; set; }

        [JsonProperty("playstyle")]
        public string[] Playstyle { get; set; }

        [JsonProperty("post_count")]
        public int PostCount { get; set; }

        [JsonProperty("profile_order")]
        public string[] ProfileOrder { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_url")]
        public Uri TitleUrl { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("account_history")]
        public object[] AccountHistory { get; set; }

        [JsonProperty("active_tournament_banner")]
        public object[] ActiveTournamentBanner { get; set; }

        [JsonProperty("badges")]
        public Badge[] Badges { get; set; }

        [JsonProperty("beatmap_playcounts_count")]
        public int BeatmapPlaycountsCount { get; set; }

        [JsonProperty("favourite_beatmapset_count")]
        public int FavouriteBeatmapsetCount { get; set; }

        [JsonProperty("follower_count")]
        public int FollowerCount { get; set; }

        [JsonProperty("graveyard_beatmapset_count")]
        public int GraveyardBeatmapsetCount { get; set; }

        [JsonProperty("groups")]
        public Group[] Groups { get; set; }

        [JsonProperty("loved_beatmapset_count")]
        public int LovedBeatmapsetCount { get; set; }

        [JsonProperty("mapping_follower_count")]
        public int MappingFollowerCount { get; set; }

        [JsonProperty("monthly_playcounts")]
        public CountInfo[] MonthlyPlaycounts { get; set; }

        [JsonProperty("page")]
        public Page Page { get; set; }

        [JsonProperty("previous_usernames")]
        public string[] PreviousUsernames { get; set; }

        [JsonProperty("ranked_and_approved_beatmapset_count")]
        public int RankedAndApprovedBeatmapsetCount { get; set; }

        [JsonProperty("replays_watched_counts")]
        public CountInfo[] ReplaysWatchedCounts { get; set; }

        [JsonProperty("scores_best_count")]
        public int ScoresBestCount { get; set; }

        [JsonProperty("scores_first_count")]
        public int ScoresFirstCount { get; set; }

        [JsonProperty("scores_recent_count")]
        public int ScoresRecentCount { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("support_level")]
        public int SupportLevel { get; set; }

        [JsonProperty("unranked_beatmapset_count")]
        public int UnrankedBeatmapsetCount { get; set; }

        [JsonProperty("user_achievements")]
        public UserAchievement[] UserAchievements { get; set; }

        [JsonProperty("rank_history")]
        public RankHistory RankHistory { get; set; }
    }
}