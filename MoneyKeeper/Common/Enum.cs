﻿using System.Text.Json.Serialization;

namespace MochiApi.Common
{
    public static class Enum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum OrderStatus
        {
            Created,
            Doing,
            Delivered,
            Completed,
            Cancelled,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TargetType
        {
            THIS,
            THIS_AND_FOLLOWING,
            ALL,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum RecurringUnit
        {
            DAY = 1,
            WEEK = 2,
            MONTH = 3,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum NotificationUnit
        {
            MINUTE = 1,
            HOUR = 2,
            DAY = 3,
            WEEK = 4,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum TypeQuery
        {
            EVENT = 1,
            COURSE = 2,
            ALL = 3,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum DayOffAction
        {
            Create = 1,
            Delete = 2,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum EndDateCourseType
        {
            EndDate = 1,
            NumberOfLessons = 2,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum CategoryType
        {
            Standard,
            Custom
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum WalletType
        {
            Personal,
            Group
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum MemberRole
        {
            Admin,
            Member
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Mode
        {
            Light,
            Dark
        }
    }
}
