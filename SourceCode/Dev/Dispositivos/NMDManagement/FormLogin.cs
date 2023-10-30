using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using RegisterLogger;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace NMDManagement
{
    public partial class FormLogin : Form
    {

        string LvStrToken = string.Empty;
        public FormLogin()
        {
            InitializeComponent();
            HangarInit();
        }

        #region Drag Form/ Mover Arrastrar Formulario
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Placeholder or WaterMark
        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "Usuario")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                txtuser.Text = "Usuario";
                txtuser.ForeColor = Color.Silver;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "Contraseña")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "Contraseña";
                txtpass.ForeColor = Color.Silver;
                txtpass.UseSystemPasswordChar = false;
            }
        }

        #endregion 

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnminimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            ValidaAcceso("HUELLA");
        }

        private void ValidaAcceso(string pTipo)
        {
            ResponseValidateFinger resultValidate = new ResponseValidateFinger();
            services servicios = new services();
            string LvStrUsuario = txtuser.Text.Trim();
            loggerATM.PsRegisterLogger("LogIn", "Ingresando al sistema NMDManagement. Usuario:" + txtuser.Text);
            

            //string jsonContent = File.ReadAllText(@"c:\hla\ATM.json");
            ResultConfgATM  globalConfigATM = servicios.GetHotState();

            if (LvStrUsuario == "")
            {
                LvStrUsuario = "Usuario";
            }
            if (LvStrUsuario != "Usuario")
            {
                loggerATM.PsRegisterLogger("LogIn", "llamando a funcion de captura de huella");


                if (pTipo == "HUELLA") //validar con huella
                {
                    loggerATM.PsRegisterLogger("LogIn", "llamando a funcion de captura de huella");

                    DataCaptureFinger finger = servicios.Capture_FingerPrint(30000);
                    if (finger != null)
                    {
                        if (finger.CaptureFingerResult.State == ResponseType.Success)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            loggerATM.PsRegisterLogger("LogIn", "Huella capturada, validar");
                            resultValidate = servicios.ValidateUserWithFingerprint(txtuser.Text, finger, globalConfigATM.GetHotStateResult.Object.Id );
                            Cursor.Current = Cursors.Default;
                            //resultValidate.VerifyUserResult.Object.

                        }
                    }
                }
                else//validar con clave
                {
                    loggerATM.PsRegisterLogger("LogIn", "Validar con clave");
                    resultValidate = servicios.ValidateUserWithPassword(txtuser.Text, txtpass.Text, globalConfigATM.GetHotStateResult.Object.Id );
                }

                UserData userData = new UserData();
                if (resultValidate.VerifyUserResult.State == ResponseType.Success)
                {
                    if (resultValidate.VerifyUserResult.Message == "Acceso válido")
                    {
                        LvStrToken = resultValidate.VerifyUserResult.Object.token;
                        Cursor.Current = Cursors.WaitCursor;
                        if (pTipo == "HUELLA")
                        {
                            userData = JsonConvert.DeserializeObject<UserData>(resultValidate.VerifyUserResult.Object.AditionalItems[1].Value);
                        }
                        else
                        {
                            VerifyUserResultGrl verifyUserResultGrl = JsonConvert.DeserializeObject<VerifyUserResultGrl>(resultValidate.VerifyUserResult.Object.AditionalItems[1].Value);
                            userData.UserCI = verifyUserResultGrl.DI;
                            userData.UserRole = verifyUserResultGrl.UserRole;
                            userData.IdUserRole = verifyUserResultGrl.IdUserRole;
                            userData.IdPerson = verifyUserResultGrl.IdUsuario;
                            userData.IdOffice = verifyUserResultGrl.IdOficina;
                            userData.OfficeName = verifyUserResultGrl.Oficina;
                            userData.UserPersonName = verifyUserResultGrl.NombrePersona;
                            userData.IdATMAccess  = verifyUserResultGrl.IdATMAccess ;
                        }



                        //userData.UserName = txtuser.Text;
                        loggerATM.PsRegisterLogger("LogIn", "Mostrando FRM principal");
                        Cursor.Current = Cursors.Default;
                        if (userData.IdUserRole == 53)//admimistración
                        {
                            Administration.MDIAdmin mDIAdmin = new Administration.MDIAdmin(userData, LvStrToken, txtuser.Text);
                            mDIAdmin.Show();
                            this.Hide();
                        }
                        else if (userData.IdUserRole == 121) //TI
                        {
                            FrmPrincipal frmP = new FrmPrincipal(userData,LvStrToken);//CAMBIAR CAMBIAR 
                            frmP.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Perfil de usuario no autorizado para ingresar al sistema", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Huella no pertenece al usurio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (resultValidate.VerifyUserResult.Message.Contains("Se ha generado"))
                {
                    MessageBox.Show("Usuario no registrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(resultValidate.VerifyUserResult.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            else
            {
                MessageBox.Show("Digite nombre de usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtuser.Text = "";
            }
        }
        private void btnNuevaClave_Click(object sender, EventArgs e)
        {
            if (ValidatePassword(txtNuevaClave1.Text))
            {
                if (txtNuevaClave1.Text == txtNuevaClave2.Text)
                {
                    CifrarVB.Cifra cifra = new CifrarVB.Cifra();
                    cifra.KeyString = txtuser.Text;
                    cifra.Text = txtNuevaClave1.Text;
                    cifra.DoXor();
                    cifra.Stretch();
                    string LvStrClaveCifrada = cifra.Text;
                    //guardar contraseña
                    AccedeBD accesoBD = new AccedeBD();
                    if (accesoBD.UpdatePassword(txtuser.Text, LvStrClaveCifrada))
                    {
                        loggerATM.PsRegisterLogger("LogIn", "Se actualizó clave");
                        MessageBox.Show("Clave actualizada\nSe cerrará la aplicación, vuelva a ingresar con su nueva clave.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("No se actualizó la clave, verifique y vuelva a realizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Las contraseñas son diferentes, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        public bool ValidatePassword(string pwd)
        {
            int minLength = 8;
            int numUpper = 1;
            int numLower = 1;
            int numNumbers = 1;
            int numSpecial = 1;
            // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            System.Text.RegularExpressions.Regex upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            System.Text.RegularExpressions.Regex lower = new System.Text.RegularExpressions.Regex("[a-z]");
            System.Text.RegularExpressions.Regex number = new System.Text.RegularExpressions.Regex("[0-9]");
            // Special is "none of the above".
            System.Text.RegularExpressions.Regex special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            // Check the length.
            if (Strings.Len(pwd) < minLength)
            {
                MessageBox.Show("La contraseña debe ser de almenos 8 caracteres, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Check for minimum number of occurrences.
            if (upper.Matches(pwd).Count < numUpper)
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (lower.Matches(pwd).Count < numLower)
            {
                MessageBox.Show("La contraseña debe contener al menos una letra minúscula, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (number.Matches(pwd).Count < numNumbers)
            {
                MessageBox.Show("La contraseña debe contener al menos un número, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (special.Matches(pwd).Count < numSpecial)
            {
                MessageBox.Show("La contraseña debe contener al menos un caracter especial, verifique y vuelva a intentar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Passed all checks.
            return true;
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            //TextBox txt = (TextBox)sender;
            //txt.Text = txt.Text.ToUpper();
            //txt.Select(txt.Text.Length, 0);
        }

        private void FormLogin_Activated(object sender, EventArgs e)
        {
            btnlogin.Focus();
        }

        private void btnAccedeClave_Click(object sender, EventArgs e)
        {
            ValidaAcceso("CLAVE");
        }

        private void HangarInit()
        {
            //inicioa Hangar
            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                try
                {
                    Process[] processes = Process.GetProcessesByName("Hangar.Desktop");
                    if (processes.Length == 0)
                    {
                        System.Diagnostics.ProcessStartInfo empaquetaShell = new ProcessStartInfo();
                        empaquetaShell.UseShellExecute = true;
                        empaquetaShell.FileName = "Hangar.Desktop.exe";
                        empaquetaShell.WorkingDirectory = @"C:\HLA";
                        empaquetaShell.Arguments = "start";
                        empaquetaShell.WindowStyle = ProcessWindowStyle.Hidden;

                        process.StartInfo = empaquetaShell;

                        process.Start();
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    throw;
                }

            }
        }

        private void FormLogin_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }

}
