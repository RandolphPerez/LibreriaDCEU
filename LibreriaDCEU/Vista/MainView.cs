using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibreriaDCEU.Vista
{
    public partial class MainView : Form, IMainView
    {
        //Campos
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChilForm;

        //Constructor
        public MainView()
        {
            InitializeComponent();
            btnBooks.Click += delegate { ShowBookView?.Invoke(this, EventArgs.Empty); };
            btnAuthor.Click += delegate { ShowAuthorView?.Invoke(this, EventArgs.Empty); };
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 50);
            panelMenu.Controls.Add(leftBorderBtn);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        public event EventHandler ShowBookView;
        public event EventHandler ShowAuthorView;

        private void btnExit_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color6);
            Application.Exit();
        }

        //Struct
        private struct RGBColor
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }

        //Métodos
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableBotton();

                //Boton
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Boton Borde Izq
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icono Form Hijo
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void DisableBotton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if(currentChilForm != null)
            {
                currentChilForm.Close();
            }
            currentChilForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            /*panelDekstop.Controls.Add(childForm);
            panelDekstop.Tag = childForm;*/
            childForm.BringToFront();
            childForm.Show();
            lbTitleChildForm.Text = childForm.Text;
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
            OpenChildForm(new BookView());
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
            OpenChildForm(new AuthorsView());
        }

        private void btnEditorial_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
            OpenChildForm(new EditorialView());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
            OpenChildForm(new SettingsView());
        }

        private void btnAcercaDe_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color5);
            OpenChildForm(new AboutView());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChilForm.Close();
            Reset();
        }

        private void Reset()
        {
            DisableBotton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.HouseChimney;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lbTitleChildForm.Text = "Inicio";
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimaze_Click(object sender, EventArgs e)
        {
            WindowState |= FormWindowState.Minimized;
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
