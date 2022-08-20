using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibreriaDCEU.Vista
{
    public partial class BookView : Form, IBookView
    {
        //Campos
        private string message;
        private bool isEdit;
        private bool isSuccesful;

        //Constructor
        public BookView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvent();
            tabControl1.TabPages.Remove(tabPageBookDetail);
            btnClose.Click += delegate { this.Close(); };
        }

        private void AssociateAndRaiseViewEvent()
        {
            //Buscar
            btnSearch.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //Agregar
            btnAdd.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageBookList);
                tabControl1.TabPages.Add(tabPageBookDetail);
                tabPageBookDetail.Text = "Agregar Libro";
            };

            //Editar
            btnEdit.Click += delegate 
            { 
                EditEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageBookList);
                tabControl1.TabPages.Add(tabPageBookDetail);
                tabPageBookDetail.Text = "Editar Libro";
            };

            //Guardar Cambios
            btnSave.Click += delegate 
            { 
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (isSuccesful)
                {
                    tabControl1.TabPages.Remove(tabPageBookDetail);
                    tabControl1.TabPages.Add(tabPageBookList);
                }
                MessageBox.Show(Message);
            };

            //Cancelar
            btnCancel.Click += delegate 
            { 
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPageBookDetail);
                tabControl1.TabPages.Add(tabPageBookList);
            };

            //Eliminar
            btnDelete.Click += delegate 
            { 
                var result = MessageBox.Show("¿Estás seguro que deseas eliminar el libro seleccionado?", "Advertencia",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };
        }

        //Propiedades
        public string BookId
        {
            get { return txtID.Text; }
            set { txtID.Text = value; } 
        }
        public string BookTitle 
        {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }
        public string BookAuthor 
        {
            get { return txtAuthor.Text; }
            set { txtAuthor.Text = value; }
        }
        public string BookEditorial 
        {
            get { return txtEditorial.Text; }
            set { txtEditorial.Text = value; }
        }
        public string SearchValue 
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }
        public bool IsEdit 
        {
            get { return isEdit; }
            set { isEdit = value; }
        }
        public bool IsSuccessful 
        {
            get { return isSuccesful; }
            set { isSuccesful = value; }
        }
        public string Message 
        {
            get { return message; }
            set { message = value; }
        }

        //Eventos
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        //Métodos
        public void SetBookListBindingSource(BindingSource bookList)
        {
            dataGridView1.DataSource = bookList;
        }

        //Patrón Singleton
        private static BookView instance;
        public static BookView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new BookView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if(instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }
            return instance;
        }
    }
}
