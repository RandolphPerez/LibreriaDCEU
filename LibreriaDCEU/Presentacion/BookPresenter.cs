using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibreriaDCEU.Modelo;
using LibreriaDCEU.Vista;

namespace LibreriaDCEU.Presentacion
{
    public class BookPresenter
    {
        //Campos
        private IBookView view;
        private IBookRepository repository;
        private BindingSource booksBindingSource;
        private IEnumerable<BookModel> bookList;

        //Constructor
        public BookPresenter(IBookView view, IBookRepository repository)
        {
            this.booksBindingSource = new BindingSource();
            this.view = view;
            this.repository = repository;

            //Suscribir eventos del controlador a los eventos de vista
            this.view.SearchEvent += SearchBook;
            this.view.AddNewEvent += AddNewBook;
            this.view.EditEvent += LoadSelectedBookToEdit;
            this.view.DeleteEvent += DeleteSelectedBook;
            this.view.SaveEvent += SaveBook;
            this.view.CancelEvent += CancelAction;

            //Establecer fuente vinculante libros
            this.view.SetBookListBindingSource(booksBindingSource);

            //Cargar vist de lista libros
            LoadAllBookList();

            //Mostrar vista
            this.view.Show();
        }

        //Métodos
        private void LoadAllBookList()
        {
            bookList = repository.GetAll();
            booksBindingSource.DataSource = bookList;//Establecer origen de datos
        }

        private void SearchBook(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);

            if (emptyValue == false)
                bookList = repository.GetByValue(this.view.SearchValue);

            else bookList = repository.GetAll();
            booksBindingSource.DataSource = bookList;
        }

        private void CancelAction(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void SaveBook(object sender, EventArgs e)
        {
            var model = new BookModel();
            model.Id = Convert.ToInt32(view.BookId);
            model.Titulo = view.BookTitle;
            model.Autor = view.BookAuthor;
            model.Editorial = view.BookEditorial;

            try
            {
                new Common.ModelDataValidation().Validate(model);

                if (view.IsEdit) //Editar model
                {
                    repository.Edit(model);
                    view.Message = "Datos del libro editados exitosamente";
                }
                else //Agregar new model
                {
                    repository.Add(model);
                    view.Message = "Libro agregado exitosamente";
                }
                view.IsSuccessful = true;
                LoadAllBookList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.BookId = "0";
            view.BookTitle = "";
            view.BookAuthor = "";
            view.BookEditorial = "";
        }

        private void DeleteSelectedBook(object sender, EventArgs e)
        {
            try
            {
                var book = (BookModel)booksBindingSource.Current;
                repository.Delete(book.Id);
                view.IsSuccessful = true;
                view.Message = "Libro eliminado exitosamente";
                LoadAllBookList();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = "Ha ocurrido un error, no se pudo volver eliminar el libro";
            }
        }

        private void LoadSelectedBookToEdit(object sender, EventArgs e)
        {
            var book = (BookModel)booksBindingSource.Current;
            view.BookId = book.Id.ToString();
            view.BookTitle = book.Titulo;
            view.BookAuthor = book.Autor;
            view.BookEditorial = book.Editorial;
            view.IsEdit = true;
        }

        private void AddNewBook(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
    }
}
