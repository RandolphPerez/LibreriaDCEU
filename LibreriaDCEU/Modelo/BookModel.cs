using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibreriaDCEU.Modelo
{
    public class BookModel
    {
        //Campos
        private int id;
        private string titulo;
        private string autor;
        private string editorial;

        //Propiedades - Validaciones
       [DisplayName("ID")]
       public int Id
       {
            get { return id; }
            set { id = value; }
       }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Se requiere el título del libro")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El título del libro debe ser entre 3 y 50 caracteres")]
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "Se requiere el nombre del autor")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del autor debe ser entre 3 y 50 caracteres")]
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        [DisplayName("Editorial")]
        [Required(ErrorMessage = "Se requiere el nombre de la editorial")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El  nombre de la editorial debe ser entre 3 y 50 caracteres")]
        public string Editorial
        {
            get { return editorial; }
            set { editorial = value; }
        }
    }
}
