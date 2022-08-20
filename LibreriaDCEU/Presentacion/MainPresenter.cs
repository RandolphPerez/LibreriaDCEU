using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaDCEU.Vista;
using LibreriaDCEU.Modelo;
using LibreriaDCEU._Repositorios;
using System.Windows.Forms;

namespace LibreriaDCEU.Presentacion
{
    public class MainPresenter
    {
        private IMainView mainView;
        private readonly string sqlConnectionString;

        public MainPresenter(IMainView mainView, string sqlConnectionString)
        {
            this.mainView = mainView;
            this.sqlConnectionString = sqlConnectionString;
            this.mainView.ShowBookView += ShowBooksView;
        }

        private void ShowBooksView(object sender, EventArgs e)
        {
            IBookView view = BookView.GetInstance((MainView)mainView);
            IBookRepository repository = new BookRepository(sqlConnectionString);
            new BookPresenter(view, repository);
        }
    }
}
