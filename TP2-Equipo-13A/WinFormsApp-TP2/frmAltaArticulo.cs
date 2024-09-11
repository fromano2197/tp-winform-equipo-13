﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace WinFormsApp_TP2
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;

        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;

        }




        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            Articulo nuevo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                nuevo.Codigo = (string)txtCodigo.Text;
                nuevo.Nombre = (string)txtNombre.Text;
                nuevo.Descripcion = (string)txtDescripcion.Text;
                nuevo.UrlImagen = new Imagen();
                nuevo.UrlImagen.ImagenUrl = (string)txtURL.Text;
                nuevo.Categoria = (Categoria)cboCategoria.SelectedItem;
                nuevo.Marca = (Marca)cboMarca.SelectedItem;
                int p = 0;
                if (Int32.TryParse(txtPrecio.Text,out p))
                {
                    nuevo.Precio = p;
                }
                else
                {
                    nuevo.Precio = 0;
                }
           

                negocio.agregar(nuevo);

                MessageBox.Show("Articulo agregado con exito");
                negocio.listar();
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new negocio.MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new negocio.CategoriaNegocio();
            //ImagenNegocio imagenNegocio = new negocio.ImagenNegocio();



            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                //txtURL.Text = imagenNegocio.ToString();

                if (articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtURL.Text = articulo.UrlImagen.ImagenUrl;
                    cargarImagen(articulo.UrlImagen.ImagenUrl);
                    
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtPrecio.Text = articulo.Precio.ToString();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtURL_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtURL.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                ptbImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                ptbImagen.Load("https://img.freepik.com/vector-premium/no-hay-foto-disponible-icono-vector-simbolo-imagen-predeterminado-imagen-proximamente-sitio-web-o-aplicacion-movil_87543-10615.jpg");
            }
        }
    }
}

