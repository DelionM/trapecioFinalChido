using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Calculus;

namespace trapecio
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            lblresul.Visible = false;
            lblResultado.Visible = false;
            txtEcuacion.Focus();

        }
        //Aqui poner las variables a ocupar
        //-----------------------------------------------
        double fxa, fxb, a, b, n, h, resultado1, resultadox, resultadoFinal;
        string expresion;
        double j2;
        Calculo AnalizadorDeFunciones = new Calculo();
        //------------------------------------------------

        private void btnCalcula_Click(object sender, EventArgs e)
        {

           

        }



        private void chart1_Click(object sender, EventArgs e)
        {
            //grafica
        }

        private void btnLimpia_Click(object sender, EventArgs e)
        {
            
        }

        private void hacerLimpieza()
        {
            lblresul.Visible = false;
            lblResultado.Visible = false;
            btnCalcula.Enabled = true;
            txtA.Clear();
            txtB.Clear();
            txtEcuacion.Clear();
            txtN.Clear();
            dgvTabla.Visible = false;
            dgvTabla2.Visible = false;
            dgvTabla.Rows.Clear();
            dgvTabla2.Rows.Clear();
            txtA.Enabled = true;
            txtB.Enabled = true;
            txtN.Enabled = true;
            txtEcuacion.Enabled = true;
            //chart1.ResetAutoValues();
            txtEcuacion.Focus();
            chart1.Series["Grafica"].Points.Clear();
            grafica.Visible = true;
        }

        private void txtN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                //MessageBox.Show("Solo puedes ingresar números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtA_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumerosEnteros(e);
        }

        private void txtB_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloNumerosEnteros(e);
        }

        private void lblresul_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEcuacion.Focus();
        }

        private void dgvTabla2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvTabla_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Salir()
        {
            if (MessageBox.Show("¿Desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
            else
            {
                this.txtEcuacion.Focus();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Salir();
        }

        private void calcular()
        {
            lblResultado.Visible = true;
            lblresul.Visible = true;
            convierte();
            //va hacer todo lo del trapesio cuando este bien escrito
            ChecaMayores();
            grafica.Visible = false;
        }
 //------------------------------------------------------------------------------------------------

        private void TodoProceso()
        {
            if (AnalizadorDeFunciones.Sintaxis(expresion, 'x'))
            {

                //es solo va hacer la unica ecuación para la facil
                if (n == 1)
                {
                    dgvTabla.Visible = true;

                    Analiza();
                    formula();
                    ingresaValores();
                    lblresul.Visible = false;
                    lblResultado.Visible = false;

                    //segun hasta aqui ya tengo lo primero si no es así solo cambio la formula en el metodo formula
                }
                //en esta parte se va hacer lo feo la parte más grande del programa
                //Creo que voy a usar un while
                else if (n > 1)

                {
                    dgvTabla2.Visible = true;
                    //MessageBox.Show("Entre a hacer la segunda parte");
                    Analiza();
                    ValorH();
                    //while (i <= n)
                    //{
                    // For para llenar los intervalos de n
                    for (int j = 0; j <= n; j++)
                    {
                        int i = dgvTabla2.Rows.Add();
                        dgvTabla2.Rows[i].Cells[0].Value = j;
                    }

                    resultadox = a;
                    dgvTabla2.Rows[0].Cells[1].Value = a;
                    dgvTabla2.Rows[0].Cells[2].Value = AnalizadorDeFunciones.EvaluaFx(a);

                    resultadoFinal = 0;
                    resultadoFinal = AnalizadorDeFunciones.EvaluaFx(a);

                    //for para Calcular x
                    for (j2 = 1; j2 <= n; j2++)
                    {
                        //dgvTabla2.Rows[j2].Cells[1].Value = a;
                        //con esto lleno los valores de x pero me falta sacar el valor por celda
                        resultadox = resultadox + h;
                        dgvTabla2.Rows[(int)j2].Cells[1].Value = resultadox;
                        dgvTabla2.Rows[(int)j2].Cells[2].Value = AnalizadorDeFunciones.EvaluaFx(resultadox);

                        if (j2 != n)
                            resultadoFinal += (2 * AnalizadorDeFunciones.EvaluaFx(resultadox));
                        else
                            resultadoFinal += AnalizadorDeFunciones.EvaluaFx(b);


                    }
                    resultadoFinal *= h / 2;
                    lblresul.Text = "El resultado es: " + resultadoFinal + "";
                    // }
                }
                else if (n < 1)

                {
                    MessageBox.Show("No se pueden ingresar valores negativos en n");
                }
            }
            else
            {
                //MessageBox.Show("Error de sintaxis");
                MessageBox.Show("Error de sintaxis", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtA.Enabled = true;
                txtB.Enabled = true;
                txtN.Enabled = true;
                txtEcuacion.Enabled = true;
            }
        }

        private void btnCalcula_Click_1(object sender, EventArgs e)
        {
            try
            {
                calcular();
                graficar();
                txtA.Enabled = false;
                txtB.Enabled = false;
                txtN.Enabled = false;
                txtEcuacion.Enabled = false;
                btnCalcula.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Campos Vacios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpia_Click_1(object sender, EventArgs e)
        {
            hacerLimpieza();
        }

        //---------------------------------------------------------------------------------------------------

        private void ChecaMayores()
        {//a es menor
            if (a < b)
            {
                TodoProceso();
               
            }
            else
            {
                MessageBox.Show("El valor de a debe de ser menor que b", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                hacerLimpieza();
            }
        }

//---------------------------------------------------------------------------------------------------
        private void graficar()
        {
            propiedades();

            if (n >= 2)
            {
                for (j2 = Convert.ToDouble(dgvTabla2.Rows[0].Cells[1].Value) - 1; j2 <= Convert.ToDouble(dgvTabla2.Rows[dgvTabla2.RowCount-1].Cells[1].Value) +1; j2++)
                {
                    chart1.Series["Grafica"].Points.AddXY(j2, AnalizadorDeFunciones.EvaluaFx(j2));
                }

            }
            /*else
            {
                for (j2 = 1; j2 <= n; j2++)
                {
                    chart1.Series["Grafica"].Points.AddXY(j2, j2 + 1);
                }
            }*/
        }

        private void propiedades()
        {

            chart1.Series["Grafica"].BorderWidth = 2;
            chart1.Series["Grafica"].Color = Color.Blue;
        }

        private void ValorH()
        {
            h = (b - a) / n;
            lblResultado.Text = "El valor de H: " + h + "";

        }

        private void formula()
        {   
            resultado1 = (fxb - fxa) * ((fxa + fxb) / 2);
        }

        private void Analiza()
        {
            //realiza la ecuacion con a y b 
            fxa = AnalizadorDeFunciones.EvaluaFx(a);
            fxb = AnalizadorDeFunciones.EvaluaFx(b);
        }

        private void ingresaValores()
        {
            // lblResultado.Text = h + "";
            // Columna,fila
            dgvTabla[0, 0].Value = 1;
            dgvTabla[1, 0].Value = fxa;
            dgvTabla[2, 0].Value = fxb;
            dgvTabla[3, 0].Value = resultado1;
        }

        private void convierte()
        {
            //obtener la ecuacion
            expresion = txtEcuacion.Text;
            a = Convert.ToDouble(txtA.Text);
            b = Convert.ToDouble(txtB.Text);
            n = Convert.ToDouble(txtN.Text);
        }
    }
}
    