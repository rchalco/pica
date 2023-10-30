using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
namespace ActualizaCAI
{
    internal class cActualiza
    {
        public void actualizaVersion()
        {
            string script =string.Empty;
            cBD bd = new cBD();
            //actualiza versión de CAI
            script = "update CJParametros  set VersionCAI='5.3.12', VersionAdmin='5.3.12'";
            bd.ActualizaEnBD(script);
            bd = null;
        }
        public void actualizaBD()
        {
            string script = string.Empty;
            cBD bd = new cBD();
            //actualiza versión 5.3.11
            script = "if not exists (select * from sys.tables t inner join sys.columns c on t.object_id = c.object_id where t.name='tCJSecuenciaR' and c.name='Operacion') " +
                            "alter table tCJSecuenciaR add Operacion varchar(50) not null default('')";
            bd.ActualizaEnBD(script);
            //Actualiza version 5.3.12
            script = "update CJTransCajero set Procesado=0 where FechaCajero>='20230620'";
            bd.ActualizaEnBD(script);

            bd = null;

        }
        public void StopServiceEmail()
        {
            ServiceController sc = new ServiceController("CAIEnviaCorreos");
            sc.Stop();


        }
        public void Actualizar()
        {
            try
            {
                //mostrar pantalla de actualización
                Form1 form = new Form1("Actualizando CAI a versión 5.3.12");
                form.Show();
                //Detener servicio de correo
                //StopServiceEmail();
                //actualizar versión
                actualizaVersion();
                //actualizar BD
                actualizaBD();
                //borrar archivos de hangar service
                cBD bc=new cBD();
                bc.BorraHangarService();
                bc = null;
                form.Close();
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
                Application.Exit();
            }
            
        }
    }
}
