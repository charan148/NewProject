
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CitizenWeb.DAL
{
    public class EntityCollectionHelper
    {
        private EntityCollectionHelper()
        {
        }

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    // row[prop.Name] = prop.GetValue(item);
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static TDestination ConvertTo<TSource, TDestination>(TSource source, TDestination destination, string[] excludedProperties)
        {
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var type = typeof(TDestination);

            foreach (var prop in props)
            {
                if (excludedProperties != null
                && excludedProperties.Contains(prop.Name))
                    continue;

                object value = prop.GetValue(source, null);

                var prop2 = type.GetProperty(prop.Name);
                if (prop2 == null)
                    continue;

                if (prop.PropertyType != prop2.PropertyType)
                    continue;

                prop2.SetValue(destination, value, null);
            }
            return destination;
        }


        public static List<TDestination> ConvertListTo<TSource, TDestination>(List<TSource> source, List<TDestination> destination)
        {
            var props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var type = typeof(TDestination);


            foreach (TSource item in source)
            {
                var destObj = Activator.CreateInstance<TDestination>();

                foreach (var prop in props)
                {
                    object value = prop.GetValue(item, null);

                    var prop2 = type.GetProperty(prop.Name);
                    if (prop2 == null)
                        continue;

                    if (prop.PropertyType != prop2.PropertyType)
                        continue;

                    prop2.SetValue(destObj, value, null);


                }

                destination.Add(destObj);

            }


            return destination;
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                //list = new List<T>(rows.Count);

                //Parallel.ForEach(rows.OfType<DataRow>().AsEnumerable(), row =>
                //{
                //    T item = CreateItem<T>(row);
                //    list.Add(item);
                //});

                list = new List<T>();
                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }

            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            ////Below Collection must need to init with Count to perform Parallel Library to allocate CPUs.
            //IList<T> listRows = new List<T>(table.Rows.Count);
            //Parallel.ForEach(table.AsEnumerable(), drow =>
            //{
            //    T item = CreateItem<T>(drow);
            //    listRows.Add(item);
            //});
            //return listRows;

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);

        }


        public static IList<T> ConvertTo<T>(DataRowCollection tableRows)
        {
            if (tableRows == null)
            {
                return null;
            }

            //Below Collection must need to init with Count to perform Parallel Library to allocate CPUs.
            IList<T> listRows = new List<T>(tableRows.Count);

            Parallel.ForEach(tableRows.OfType<DataRow>().AsEnumerable(), drow =>
            {
                T item = CreateItem<T>(drow);
                listRows.Add(item);
            });

            return listRows;
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    if (prop != null)
                    {
                        try
                        {

                            object value = row[column.ColumnName];
                            if ((column.DataType.FullName == "System.Int32") && (value == DBNull.Value) && Nullable.GetUnderlyingType(prop.PropertyType) == null)
                                value = 0;
                            else if ((column.DataType.FullName == "System.String") && (value == DBNull.Value))
                                value = "";
                            else if ((column.DataType.FullName == "System.Double") && (value == DBNull.Value))
                                value = 0;
                            else if ((column.DataType.FullName == "System.Decimal") && (value == DBNull.Value))
                                value = default(decimal);
                            else if ((column.DataType.FullName == "System.Boolean") && (value == DBNull.Value))
                                value = false;
                            else if ((column.DataType.FullName == "System.DateTime") && (value == DBNull.Value) && Nullable.GetUnderlyingType(prop.PropertyType) != null)
                                value = null;
                            else if ((column.DataType.FullName == "System.DateTime") && (value == DBNull.Value) && Nullable.GetUnderlyingType(prop.PropertyType) == null)
                                value = default(DateTime?); //DateTime.MaxValue;
                            else if (Nullable.GetUnderlyingType(prop.PropertyType) != null && value == DBNull.Value)
                                value = null;

                            prop.SetValue(obj, value, null);
                        }
                        catch (SqlException sqlEx)
                        {
                            //Logging.LogErrorMessage("Method: EntityCollectionHelper, Layer: Models, Stack Trace: " + sqlEx.ToString());
                            throw;
                        }
                        catch (Exception ex)
                        {
                            //Logging.LogErrorMessage("Method: EntityCollectionHelper, Layer: Models, Stack Trace: " + ex.ToString());
                            throw;
                        }
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                //table.Columns.Add(prop.Name, prop.PropertyType);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            return table;
        }

    }
}
