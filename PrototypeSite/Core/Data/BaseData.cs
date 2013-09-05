using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Core.Ioc;
using Core.Util;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;

namespace Core.Data
{
    [DataAccess]
    public class BaseData
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (BaseData));

        protected int timeout = 30;

        protected Database database;

        protected const string BATCHTABLENAME = "BATCHTABLENAME";

        [Dependency("DbTimeoutSecond")]
        public string Timeout
        {
            set { int.TryParse(value, out timeout); }
        }

        [Dependency]
        public DatabaseDelegator DatabaseDelegator
        {
            set { database = value.CreateDatabase(); }
        }

        protected virtual int ExecuteNonQuery(DbCommand dbCommand)
        {
            int result = 0;

            StopWatch stopWatch = new StopWatch();

            try
            {
                dbCommand.CommandTimeout = timeout;
                Logger.Info("ExecuteNonQuery, StoreProcedureName=" + dbCommand.CommandText + ", Params={" + GetParams(dbCommand) + "}");
                result = database.ExecuteNonQuery(dbCommand);
                return result;
            }
            finally
            {
                stopWatch.End();
                Logger.Info("ExecuteNonQuery Complete, result=" + result + ", elapsed=" + stopWatch.ElapsedMs());
            }
        }

        protected virtual DataSet ExecuteDataSet(DbCommand dbCommand)
        {
            DataSet dataSet = null;
            
            StopWatch stopWatch = new StopWatch();

            try
            {
                dbCommand.CommandTimeout = timeout;
                dataSet = database.ExecuteDataSet(dbCommand);
                Logger.Info("ExecuteDataSet, StoreProcedureName=" + dbCommand.CommandText + ", Params={" + GetParams(dbCommand) + "}, Dataset Summary=" + GetDataSetSummary(dataSet));
                return dataSet;
            }
            finally
            {
                stopWatch.End();
                Logger.Info("ExecuteNonQuery Complete, elapsed=" + stopWatch.ElapsedMs());
            }
        }

        protected virtual T ExecuteScalar<T>(DbCommand dbCommand)
        {
            object result = null;

            StopWatch stopWatch = new StopWatch();

            try
            {
                dbCommand.CommandTimeout = timeout;
                Logger.Info("ExecuteScalar, StoreProcedureName=" + dbCommand.CommandText + ", Params={" + GetParams(dbCommand) + "}");
                result = database.ExecuteScalar(dbCommand);
                return ConversionUtil.To<T>(result);
            }
            finally
            {
                stopWatch.End();
                Logger.Info("ExecuteScalar Complete, result=" + ConversionUtil.To<string>(result) + " elapsed=" + stopWatch.ElapsedMs());
            }
        }

        protected virtual DataSet CreateBatchDataSet(params ColumnInfo[] columnInfos)
        {
            DataSet dataSet = new DataSet();

            DataTable dataTable = new DataTable(BATCHTABLENAME);
            foreach (ColumnInfo columnInfo in columnInfos)
            {
                DataColumn dataColumn = new DataColumn(columnInfo.ColumnName, columnInfo.ColumnType);
                dataTable.Columns.Add(dataColumn);
            }

            dataSet.Tables.Add(dataTable);
            
            return dataSet;
        }

        protected virtual int ExecuteBatchSPCall(DbCommand dbCommand, DataSet dataSet)
        {
            int result = 0;

            StopWatch stopWatch = new StopWatch();

            try
            {
                dbCommand.CommandTimeout = timeout;
                Logger.Info("ExecuteBatchUpdate, StoreProcedureName=" + dbCommand.CommandText);
                result = database.UpdateDataSet(dataSet, BATCHTABLENAME, dbCommand, null, null, UpdateBehavior.Standard);
                return result;
            }
            finally
            {
                stopWatch.End();
                Logger.Info("ExecuteBatchUpdate Complete, result=" + result + " ,elapsed=" + stopWatch.ElapsedMs() + " ms");
            }
        }

        private string GetDataSetSummary(DataSet dataSet)
        {
            StringBuilder builder = new StringBuilder();

            try
            {
                if (dataSet != null)
                {
                    int index = 0;
                    builder.Append("DataTable Count=").Append(dataSet.Tables.Count).Append(" ,Rows={");
                    foreach (DataTable dataTable in dataSet.Tables)
                    {
                        if (index > 0)
                            builder.Append(";");
                        builder.Append("Name=").Append(dataTable.TableName).Append(", Count=").Append(dataTable.Rows.Count);
                        index++;
                    }
                    builder.Append("}");
                }
            }
            catch (Exception ignore)
            {
                Logger.Info("GetDataSetSummary", ignore);
            }

            return builder.ToString();
        }

        private static string GetParams(DbCommand dbCommand)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                int index = 0;
                foreach (DbParameter parameter in dbCommand.Parameters)
                {
                    if (index > 0)
                        builder.Append(";");
                    builder.Append("Name=").Append(parameter.ParameterName).Append(",Value=").Append(parameter.Value);
                    index++;
                }
            }
            catch (Exception ignore)
            {
                Logger.Info("GetParams", ignore);
            }

            return builder.ToString();
        }

        protected string GetObjectString(object obj)
        {
            if (obj != null)
                return obj.ToString();
            return "0";
        }

        public static object GetParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            if (value is DateTime && (DateTime)value == DateTime.MinValue)
            {
                return DBNull.Value;
            }
            if (value is string && string.IsNullOrEmpty((string)value))
            {
                return DBNull.Value;
            }
            return value;
        }

        public static T GetOutputParameter<T>(DbCommand command, string parameterName)
        {
            object value = command.Parameters[parameterName].Value;
            if (value == Convert.DBNull)
            {
                return default(T);
            }
            return (T)value;
        }

        public static T To<T>(Dictionary<string, string> dictionary, string key)
        {
            return To<T>(dictionary[key]);
        }

        public static T To<T>(DataRow dataRow, string columnName)
        {
            return To<T>(dataRow[columnName]);
        }

        public static T To<T>(object value)
        {
            if (Convert.IsDBNull(value))
            {
                return default(T);
            }

            if (typeof(T) == typeof(char) || typeof(T) == typeof(char?))
            {
                value = ((string)value)[0];
            }
            else if (typeof(T) == typeof(bool))
            {
                if (value != null && ("Y".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase) || "TRUE".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase)))
                    value = true;
                else
                    value = false;
            }
            else if (typeof(T) == typeof(string))
            {
                if (value == null)
                {
                    value = string.Empty;
                }
            }
            try
            {
                return (T)value;
            }
            catch
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(DataTable dataTable, string key, string value)
        {
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dictionary[To<TKey>(dataTable.Rows[i], key)] = To<TValue>(dataTable.Rows[i], value);
                    //dictionary.Add(To<TKey>(dataTable.Rows[i], key), To<TValue>(dataTable.Rows[i], value));
                }
            }
            return dictionary;
        }
    }
}
