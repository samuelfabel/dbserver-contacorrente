using System;
using System.Data;

namespace DbServer.Wallet.Application
{
    public static class IDataReaderExtension
    {
        public static short? GetInt16Nullable(this IDataReader reader, int index, short? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetInt16(index);
        }

        public static short GetInt16NullableOrDefault(this IDataReader reader, int index, short defaultValue = default(short))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetInt16(index);
        }

        public static int? GetInt32Nullable(this IDataReader reader, int index, int? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetInt32(index);
        }

        public static int GetInt32NullableOrDefault(this IDataReader reader, int index, int defaultValue = default(int))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetInt32(index);
        }

        public static double? GetDoubleNullable(this IDataReader reader, int index, double? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDouble(index);
        }

        public static double GetDoubleNullableOrDefault(this IDataReader reader, int index, double defaultValue = default(double))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDouble(index);
        }

        public static float? GetFloatNullable(this IDataReader reader, int index, float? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetFloat(index);
        }

        public static float GetFloatNullableOrDefault(this IDataReader reader, int index, float defaultValue = default(float))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetFloat(index);
        }

        public static decimal? GetDecimalNullable(this IDataReader reader, int index, decimal? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDecimal(index);
        }

        public static decimal GetDecimalNullableOrDefault(this IDataReader reader, int index, decimal defaultValue = default(decimal))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDecimal(index);
        }

        public static DateTime? GetDateTimeNullable(this IDataReader reader, int index, DateTime? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetDateTime(index);
        }

        public static string GetStringNullable(this IDataReader reader, int index, string defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetString(index).Trim();
        }

        public static bool? GetBooleanNullable(this IDataReader reader, int index, bool? defaultValue = null)
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetBoolean(index);
        }

        public static bool GetBooleanNullableOrDefault(this IDataReader reader, int index, bool defaultValue = default(bool))
        {
            if (reader.IsDBNull(index))
                return defaultValue;

            return reader.GetBoolean(index);
        }
    }
}