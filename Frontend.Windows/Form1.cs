using System;
using System.Drawing;
using System.Windows.Forms;
using Backend;

namespace Frontend.Windows
{
    public partial class Form1 : Form
    {
        private TextBox txtPantalla;
        private Button btn7, btn8, btn9, btnParentesisAbierto, btnParentesisCerrado, btnDelete;
        private Button btn4, btn5, btn6, btnMultiplicar, btnDividir, btnClear;
        private Button btn1, btn2, btn3, btnSuma, btnResta, btnPotencia;
        private Button btn0, btnPunto, btnIgual;

        public Form1()
        {
            InitializeComponent();
            ConfigurarVentana();
            InicializarComponentesInterfaz();
        }

        private void ConfigurarVentana()
        {
            this.Text = "Functions Evaluator";
            this.Width = 460;
            this.Height = 380;
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InicializarComponentesInterfaz()
        {
            // 1. Pantalla superior (TextBox con fondo verde)
            txtPantalla = new TextBox();
            txtPantalla.Location = new Point(12, 12);
            txtPantalla.Width = 420;
            txtPantalla.Height = 45;
            txtPantalla.Font = new Font("Arial", 18, FontStyle.Bold);
            txtPantalla.BackColor = Color.Green;
            txtPantalla.ForeColor = Color.White;
            txtPantalla.TextAlign = HorizontalAlignment.Left;
            this.Controls.Add(txtPantalla);

            // --- FILA 1: [7] [8] [9] [ ( ] [ ) ] [ Delete ] ---
            btn7 = CrearBoton("7", 12, 70, 60, 60, Color.White, Color.Black);
            btn8 = CrearBoton("8", 78, 70, 60, 60, Color.White, Color.Black);
            btn9 = CrearBoton("9", 144, 70, 60, 60, Color.White, Color.Black);
            btnParentesisAbierto = CrearBoton("(", 210, 70, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnParentesisCerrado = CrearBoton(")", 276, 70, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnDelete = CrearBoton("Delete", 342, 70, 90, 60, Color.FromArgb(235, 125, 20), Color.White);

            // --- FILA 2: [4] [5] [6] [ * ] [ / ] [ Clear ] ---
            btn4 = CrearBoton("4", 12, 136, 60, 60, Color.White, Color.Black);
            btn5 = CrearBoton("5", 78, 136, 60, 60, Color.White, Color.Black);
            btn6 = CrearBoton("6", 144, 136, 60, 60, Color.White, Color.Black);
            btnMultiplicar = CrearBoton("*", 210, 136, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnDividir = CrearBoton("/", 276, 136, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnClear = CrearBoton("Clear", 342, 136, 90, 60, Color.FromArgb(235, 125, 20), Color.White);

            // --- FILA 3: [1] [2] [3] [ + ] [ - ] [ ^ ] ---
            btn1 = CrearBoton("1", 12, 202, 60, 60, Color.White, Color.Black);
            btn2 = CrearBoton("2", 78, 202, 60, 60, Color.White, Color.Black);
            btn3 = CrearBoton("3", 144, 202, 60, 60, Color.White, Color.Black);
            btnSuma = CrearBoton("+", 210, 202, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnResta = CrearBoton("-", 276, 202, 60, 60, Color.FromArgb(235, 125, 20), Color.White);
            btnPotencia = CrearBoton("^", 342, 202, 90, 60, Color.FromArgb(235, 125, 20), Color.White);

            // --- FILA 4: [ 0 ] [ . ] [ = ] ---
            btn0 = CrearBoton("0", 12, 268, 126, 60, Color.White, Color.Black);
            btnPunto = CrearBoton(".", 144, 268, 60, 60, Color.White, Color.Black);
            btnIgual = CrearBoton("=", 210, 268, 222, 60, Color.FromArgb(235, 125, 20), Color.White);
        }

        private Button CrearBoton(string texto, int x, int y, int ancho, int alto, Color fondo, Color letra)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Location = new Point(x, y);
            boton.Width = ancho;
            boton.Height = alto;
            boton.BackColor = fondo;
            boton.ForeColor = letra;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Arial", 14, FontStyle.Bold);
            
            // Evento al hacer clic en los botones
            boton.Click += (sender, e) => {
                string valor = boton.Text;
                if (valor == "Clear")
                {
                    txtPantalla.Clear();
                }
                else if (valor == "Delete")
                {
                    if (txtPantalla.Text.Length > 0)
                        txtPantalla.Text = txtPantalla.Text.Substring(0, txtPantalla.Text.Length - 1);
                }
                else if (valor == "=")
                {
                    try
                    {
                        string expresion = txtPantalla.Text;
                        
                        // Llamada al Backend para evaluar la expresión usando pilas
                        double resultado = ExpressionEvaluator.Evalute(expresion);
                        
                        // Muestra el resultado en pantalla tal como los ejemplos del taller
                        txtPantalla.Text = expresion + "=" + resultado;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al evaluar la expresión: " + ex.Message, "Sintaxis Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    txtPantalla.Text += valor;
                }
            };

            this.Controls.Add(boton);
            return boton;
        }
    }
}