using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eurocopa
{
    public partial class f_Inicial : Form
    {
        public f_Inicial()
        {
            InitializeComponent();
        }

        string[,] aEquipos = new string[20, 2] {{"Austria",""},
                                                {"Bélgica",""},
                                                {"Croacia",""},
                                                {"Inglaterra",""},
                                                {"Francia",""},
                                                {"Alemania",""},
                                                {"Hungría",""},
                                                {"Islandia",""},
                                                {"Italia",""},
                                                {"Polonia",""},
                                                {"Portugal",""},
                                                {"Irlanda",""},
                                                {"Rumania",""},
                                                {"Rusia",""},
                                                {"Eslovaquia",""},
                                                {"España",""},
                                                {"Suecia",""},
                                                {"Suiza",""},
                                                {"Turquía",""},
                                                {"Ucrania",""}};

        int[] aGrupo1 = new int[10];
        int[] aGrupo2 = new int[10];
        int[,] aCalendario = new int[90, 4];

        int npartido = 0;
        int xornada = 1;

        private void f_Inicial_Load(object sender, EventArgs e)
        {
            Cargargrupos();

            btGrupos.Enabled = true;
        }

        private void Cargargrupos()
        {
            for (int i = 0; i < aEquipos.GetLength(0); i++)
            {
                aEquipos[i, 1] = aEquipos[i, 0].Substring(0, 3).ToUpper();
                lbxEquipos.Items.Add(aEquipos[i, 1] + "\t" + aEquipos[i, 0]);
            }
        }

        private void btGrupos_Click(object sender, EventArgs e)
        {
            SortearGrupos();
            CargarGruposSorteados();

            btCalendario.Enabled = true;
            btGrupos.Enabled = false;
        }

        private void SortearGrupos()
        {
            Random r = new Random();
            int nEquipoR;

            for (int i = 0; i < aEquipos.GetLength(0); i++)
            {
                nEquipoR = r.Next(20);
                if (i < 10)
                {
                    if (Array.IndexOf(aGrupo1, nEquipoR, 0, i) >= 0)
                    {
                        i--;
                    }
                    else
                    {
                        aGrupo1[i] = nEquipoR;
                    }

                }
                else
                {
                    if (Array.IndexOf(aGrupo1, nEquipoR, 0, aGrupo1.GetLength(0)) >= 0 || Array.IndexOf(aGrupo2, nEquipoR, 0, i - 10) >= 0)
                    {
                        i--;
                    }
                    else
                    {
                        aGrupo2[i - 10] = nEquipoR;
                    }
                }
            }
        }

        private void CargarGruposSorteados()
        {
            for (int i = 0; i < aGrupo1.GetLength(0); i++)
            {
                lbxGrupo1.Items.Add((aEquipos[aGrupo1[i], 1]) + "\t" + (aEquipos[aGrupo1[i], 0]));
                lbxGrupo2.Items.Add((aEquipos[aGrupo2[i], 1]) + "\t" + (aEquipos[aGrupo2[i], 0]));
            }
        }

        private void btCalendario_Click(object sender, EventArgs e)
        {
            xerarXornada(aGrupo1, 1);
            xerarXornada(aGrupo2, 2);

            GenerarPartidosJornada();

            CargarJornada();
            btCalendario.Enabled = false;
        }

        private void xerarXornada(int[] aGR, int grupo)
        {
            for (int i = 0; i < 5; i++)
            {
                aCalendario[npartido, 0] = xornada;
                aCalendario[npartido, 1] = grupo;
                aCalendario[npartido, 2] = aGR[i];
                aCalendario[npartido, 3] = aGR[9 - i];
                npartido++;
            }
        }

        private void GenerarPartidosJornada()
        {
            int tempGrupo1;
            int tempGrupo2;

            for (xornada = 2; xornada < 10; xornada++)
            {
                tempGrupo1 = aGrupo1[9];
                tempGrupo2 = aGrupo2[9];

                for (int i = 9; i > 1; i--)
                {
                    aGrupo1[i] = aGrupo1[i - 1];
                    aGrupo2[i] = aGrupo2[i - 1];
                }

                aGrupo1[1] = tempGrupo1;
                aGrupo2[1] = tempGrupo2;

                xerarXornada(aGrupo1, 1);
                xerarXornada(aGrupo2, 2);
            }
        }

        private void CargarJornada()
        {
            for (int i = 0; i < aCalendario.GetLength(0); i++)
            {
                if (i % 10 == 0)
                {
                    lbxCalendarioG1.Items.Add("-------------------");
                    lbxCalendarioG1.Items.Add("xornada " + aCalendario[i, 0]);
                    lbxCalendarioG1.Items.Add("-------------------");

                    lbxCalendarioG2.Items.Add("-------------------");
                    lbxCalendarioG2.Items.Add("xornada " + aCalendario[i, 0]);
                    lbxCalendarioG2.Items.Add("-------------------");
                }

                if (aCalendario[i, 1] == 1)
                {
                    lbxCalendarioG1.Items.Add(aEquipos[aCalendario[i, 2], 1] + "\t" + aEquipos[aCalendario[i, 3], 1]);
                }

                else
                {
                    lbxCalendarioG2.Items.Add(aEquipos[aCalendario[i, 2], 1] + "\t" + aEquipos[aCalendario[i, 3], 1]);
                }
            }
        }
    }
}
