using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using LibreriaDCEU.Modelo;

namespace LibreriaDCEU._Repositorios
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        //Constructor
        public BookRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Métodos
        public void Add(BookModel bookModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO Libros VALUES (@titulo, @autor, @editorial)";
                command.Parameters.Add("@titulo", SqlDbType.NVarChar).Value = bookModel.Titulo;
                command.Parameters.Add("@autor", SqlDbType.NVarChar).Value = bookModel.Autor;
                command.Parameters.Add("@editorial", SqlDbType.NVarChar).Value = bookModel.Editorial;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM Libros WHERE ID = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(BookModel bookModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"UPDATE Libros 
                                        SET Titulo = @titulo, Autor = @autor, Editorial = @editorial
                                        WHERE ID = @id";
                command.Parameters.Add("@titulo", SqlDbType.NVarChar).Value = bookModel.Titulo;
                command.Parameters.Add("@autor", SqlDbType.NVarChar).Value = bookModel.Autor;
                command.Parameters.Add("@editorial", SqlDbType.NVarChar).Value = bookModel.Editorial;
                command.Parameters.Add("@id", SqlDbType.Int).Value = bookModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<BookModel> GetAll()
        {
            var bookList = new List<BookModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Libros ORDER BY ID ASC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bookModel = new BookModel();
                        bookModel.Id = (int)reader[0];
                        bookModel.Titulo = reader[1].ToString();
                        bookModel.Autor = reader[2].ToString();
                        bookModel.Editorial = reader[3].ToString();
                        bookList.Add(bookModel);
                    }
                }
            }
            return bookList;
        }

        public IEnumerable<BookModel> GetByValue(string value)
        {
            var bookList = new List<BookModel>();
            int bookId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string bookTitle = value;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT * FROM Libros
                                        WHERE ID = @id or Titulo LIKE @titulo+ '%'
                                        ORDER BY ID ASC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = bookId;
                command.Parameters.Add("@titulo", SqlDbType.NVarChar).Value = bookTitle;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var bookModel = new BookModel();
                        bookModel.Id = (int)reader[0];
                        bookModel.Titulo = reader[1].ToString();
                        bookModel.Autor = reader[2].ToString();
                        bookModel.Editorial = reader[3].ToString();
                        bookList.Add(bookModel);
                    }
                }
            }
            return bookList;
        }
    }
}
