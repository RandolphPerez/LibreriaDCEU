using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDCEU.Vista
{
    public interface IMainView
    {
        event EventHandler ShowBookView;
        event EventHandler ShowAuthorView;
    }
}
