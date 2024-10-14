using CafeSanchez.DataAccess.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess.DAO;
public class ProductDao(string connectionString) : IProductDao
{
    private string _connectionString = connectionString;

    public Product Create(Product entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> Read()
    {
        string selectSql = "SELECT Name, Description, Price FROM Products";

        IDbConnection connection = new SqlConnection(_connectionString);

        return connection.Query<Product>(selectSql);

        // Using ADO
        //SqlCommand selectCommand = new(selectSql, connection);

        //connection.Open();

        //SqlDataReader reader = selectCommand.ExecuteReader();
        //while (reader.Read())
        //{
        //    yield return new Product()
        //    {
        //        Name = reader.GetString("Name"),
        //        Description = reader.GetString("Description"),
        //        Price = reader.GetDecimal("Price")
        //    };
        //}



    }

    public Product Update(Product entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Product entity)
    {
        throw new NotImplementedException();
    }

    public Product ReadByName(string name)
    {
        string selectSql = "SELECT Name, Description, Price FROM Products WHERE Name = @Name";

        IDbConnection connection = new SqlConnection(_connectionString);

        return connection.QuerySingle<Product>(selectSql, new { Name = name });
    }
}
