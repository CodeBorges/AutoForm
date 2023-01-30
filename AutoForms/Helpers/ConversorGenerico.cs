using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace AutoForms.Helpers
{
    public class ConversorGenerico
    {
        public static List<T> CriarLista<T>(SqlDataReader reader)
        {
            var results = new List<T>();
            Func<SqlDataReader, T> readRow = ProcessarReader<T>(reader);

            while (reader.Read())
                results.Add(readRow(reader));

            return results;
        }

        private static Func<SqlDataReader, T> ProcessarReader<T>(SqlDataReader reader)
        {
            Delegate resDelegate;

            List<string> readerColumns = new List<string>();
            for (int index = 0; index < reader.FieldCount; index++)
                readerColumns.Add(reader.GetName(index));

            // determine the information about the reader
            var readerParam = Expression.Parameter(typeof(SqlDataReader), "reader");
            var readerGetValue = typeof(SqlDataReader).GetMethod("GetValue");

            // create a Constant expression of DBNull.Value to compare values to in reader
            var dbNullExp = Expression.Field(expression: null, type: typeof(DBNull), fieldName: "Value");

            // loop through the properties and create MemberBinding expressions for each property
            List<MemberBinding> memberBindings = new List<MemberBinding>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // determine the default value of the property
                object defaultValue = null;
                if (prop.PropertyType.IsValueType)
                    defaultValue = Activator.CreateInstance(prop.PropertyType);
                else if (prop.PropertyType.Name.ToLower().Equals("string"))
                    defaultValue = String.Empty;

                // cria duas variáveis (uma para a propriedade de classe e outra para a coluna de banco)
                // e atribui o nome da propriedade às duas para garantir o acesso
                // em propriedades que tenham ou não o CustomAttribute "Column"
                string propertyName = prop.Name;
                string columnName = prop.Name;

                // verifica se a propriedade tem "ColumnAttribute"
                if (prop.IsDefined(typeof(ColumnAttribute)))
                {
                    // acessa o atributo "Column" para pegar o nome da coluna no banco
                    columnName = prop.GetCustomAttribute<ColumnAttribute>().Name;
                }

                // verifica se o reader possui alguma coluna com o nome especificado
                if (readerColumns.Contains(columnName))
                {
                    // build the Call expression to retrieve the data value from the reader
                    var indexExpression = Expression.Constant(reader.GetOrdinal(columnName));
                    var getValueExp = Expression.Call(readerParam, readerGetValue, new Expression[] { indexExpression });

                    // create the conditional expression to make sure the reader value != DBNull.Value
                    var testExp = Expression.NotEqual(dbNullExp, getValueExp);
                    var ifTrue = Expression.Convert(getValueExp, prop.PropertyType);
                    var ifFalse = Expression.Convert(Expression.Constant(defaultValue), prop.PropertyType);

                    // create the actual Bind expression to bind the value from the reader to the property value
                    MemberInfo mi = typeof(T).GetMember(propertyName)[0];
                    MemberBinding mb = Expression.Bind(mi, Expression.Condition(testExp, ifTrue, ifFalse));
                    memberBindings.Add(mb);
                }
            }

            // create a MemberInit expression for the item with the member bindings
            var newItem = Expression.New(typeof(T));
            var memberInit = Expression.MemberInit(newItem, memberBindings);

            var lambda = Expression.Lambda<Func<SqlDataReader, T>>(memberInit, new ParameterExpression[] { readerParam });
            resDelegate = lambda.Compile();

            return (Func<SqlDataReader, T>)resDelegate;
        }

    }
}
