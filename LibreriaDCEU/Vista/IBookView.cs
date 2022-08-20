using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibreriaDCEU.Vista
{
    public interface IBookView
    {
        //Propiedades - Campos
        string BookId { get; set; }
        string BookTitle { get; set; }
        string BookAuthor { get; set; }
        string BookEditorial { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Eventos
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        //Métodos
        void SetBookListBindingSource(BindingSource bookList);
        void Show(); //Opcional
    }
}
