using System.ComponentModel;
using System.Data;

namespace Dapper.Data.Extensions;

public static class ListExtensions
{
  public static DataTable ToDataTable<T>(this List<T> items)
  {
    var dataTable = new DataTable();
    var propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(T));

    for (var i = 0; i < propertyDescriptorCollection.Count; i++)
    {
      var propertyDescriptor = propertyDescriptorCollection[i];
      var type = propertyDescriptor.PropertyType;

      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        type = Nullable.GetUnderlyingType(type);
      }

      dataTable.Columns.Add(propertyDescriptor.Name, type!);
    }

    var values = new object?[propertyDescriptorCollection.Count];

    foreach (var iListItem in items)
    {
      for (var i = 0; i < values.Length; i++)
      {
        values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
      }
      dataTable.Rows.Add(values);
    }

    return dataTable;
  }
}
