using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMDManagement
{
    public partial class FrmNMDStatus : Form
    {
        public FrmNMDStatus()
        {
            InitializeComponent();
            loadData();
        }
        public void loadData()
        {
            services servicios = new services();
            //StatusDispenser statusDispenser26= new StatusDispenser(); //- = ExecutorCommand.ejecutar(Comando.NMDStatus);
            ResponseNMDStatus status = servicios.NMDStatus();


            List<first> dFirts = new List<first>();
            List<second> dSeconds = new List<second>();
            if (status.DiagnosticarResult.State == ResponseType.Success || status.DiagnosticarResult.State == ResponseType.Warning)
            {
                int position = 1;
                for (int index = 0; index <= 8; index++)
                {
                    first dato = new first();
                    dato.hnr = status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 1);
                    dato.casseteId = status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 5, 5);
                    string a = "";
                    if (dato.hnr == "0")
                    {
                        if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "1")
                        {
                            a = "RV not present";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "2")
                        {
                            a = "RV present but closed";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "5")
                        {
                            a = "Opened in Single Note Acceptance position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 1) == "6")
                        {
                            a = "Opened in Bundle Acceptance position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "7")
                        {
                            a = "Opened in Bundle Reject position";
                        }
                    }
                    else
                    {
                        if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "0")
                        {
                            a = "No Feeder at position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "1")
                        {
                            a = "No Cassette Inserted";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "2")
                        {
                            a = "Cassette Inserted";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "3")
                        {
                            a = "Cassette Inserted and Data from Cassette is read";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "4")
                        {
                            a = "Cassette Locked to Frame";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "5")
                        {
                            a = "Cassette opened in operation position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 2, 1) == "9")
                        {
                            a = "Cassette data changed. The cassette needs to be removed and re - inserted.";
                        }
                    }
                    dato.open = a;

                    if (dato.hnr == "0")
                    {
                        if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "01")
                        {
                            a = "The RV is almost full";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "02")
                        {
                            a = "The RV is full";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "03")
                        {
                            a = "There is no RV inserted";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "04")
                        {
                            a = "There was an error when the RV shutter was moved to the stack reject position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "05")
                        {
                            a = "There was an error when closing RV";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "06")
                        {
                            a = "There was an error when the RV shutter was moved to the single accept position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "07")
                        {
                            a = "There was an error when the RV shutter was moved to the stack accept position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "08")
                        {
                            a = "Checksum error in the RV internal data";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "09")
                        {
                            a = "A note has jammed between the NQ and the Reject sensor(Located in the diverter)";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "10")
                        {
                            a = "A note has jammed in the Reject sensor.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "11")
                        {
                            a = "A note that was intended to be single rejected was seen in the Note Stacker inlet.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "12")
                        {
                            a = "Error in communication with the Reject Vault";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "13")
                        {
                            a = "Not Used";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "14")
                        {
                            a = "Not Used";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "00")
                        {
                            a = "The Reject Values is OK";
                        }
                    }
                    else
                    {
                        if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "01")
                        {
                            a = "There is no cassette in the actual feeder position";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "02")
                        {
                            a = "Service requested on the actual feeder.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "03")
                        {
                            a = "There is no RV inserted";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "04")
                        {
                            a = "Empty has been detected without having low level in the cassette";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "05")
                        {
                            a = "Empty has been detected with low level in the cassette";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "06")
                        {
                            a = "Empty has been detected, but the feeding continues from another feeder";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "07")
                        {
                            a = "The cassette is marked as empty.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "08")
                        {
                            a = "The feeder has not being able to feed the notes.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "09")
                        {
                            a = "The feeding is interrupted due to a possible jam between the feeder and the Note Qualifier.A retry is made on this error.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "10")
                        {
                            a = "A sensor is broken, or a note is stuck under the exit sensor";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "11")
                        {
                            a = "The feeding is aborted because of the RV single department is getting full.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "12")
                        {
                            a = "The feeding is aborted, a bundle reject is performed and all notes are fed again.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "13")
                        {
                            a = "It was not possible to open or close the cassette";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "14")
                        {
                            a = "Communication error with the feeder / cassette during Open or Close.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "15")
                        {
                            a = "Communication error with the feeder.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "16")
                        {
                            a = "It was not possible to access the feeders from a task. Another task has the access to the feeders.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "17")
                        {
                            a = "It was not possible to create mailboxes and queues, the task will not start at all.";
                        }
                        else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 3, 2) == "00")
                        {
                            a = "Feeder status es OK";
                        }
                    }

                    dato.intStatusRV = a;

                    if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 1, 1) == "0")
                    {
                        dato.status = "Ok";
                    }
                    else
                    {
                        dato.status = status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position + 1, 1);
                    }
                    dFirts.Add(dato);
                    position = position + 10;
                }
                dgvfirst.DataSource = dFirts;
                second oSecond = new second();
                oSecond.task = "Main Motor";
                string value1 = "";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "Transport clock pulses are missed, probably due to dust on the transport clock sensor.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "The main motor transport did not reach the stipulated speed within a timeout.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "The speed is lower than the speed tolerance, probably due to a jam in the transport path";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "04")
                {
                    oSecond.statuss = "The speed is higher than the speed tolerance";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "It was not possible to access the main motor transport from a task. Another task has the access to main motor transport.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "06")
                {
                    oSecond.statuss = "It was not possible to create mailboxes and queues, the task will not start at all.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00")
                {
                    oSecond.statuss = "The transport is OK";
                }

                dSeconds.Add(oSecond);
                oSecond = new second();
                position = +2;
                oSecond.task = "Note Qualifier";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "An error in the note data table was detected during power on. All notes have to be learned again.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "An error was detected when writing the note data table to the e2prom.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "More than five rejects without any OK notes between.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "04")
                {
                    oSecond.statuss = "A note has left the Note Feeder exit sensor is not seen in the Double detect sensors";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "An error was detected, when an ongoing calibration was made on the Double detect sensors. The error is cleared if the next ongoing calibration is successful.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "06")
                {
                    oSecond.statuss = "An error was detected during the learning note sequence";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "07")
                {
                    oSecond.statuss = "An error was detected, when a calibration from a command was made on the Double detect sensors";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "08")
                {
                    oSecond.statuss = "The lid in the Note Qualifier is detected as opened";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "09")
                {
                    oSecond.statuss = "It was not possible to access the qualifier a task, or it was not possible to post the note data to the Stacker Control task.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "10")
                {
                    oSecond.statuss = "It was not possible to create mailboxes and queues, the task will not start at all";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00")
                {
                    oSecond.statuss = "The Note Qualifier is OK";
                }

                dSeconds.Add(oSecond);
                oSecond = new second();
                position = +2;
                oSecond.task = "Note Diverter";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "It was not possible to calibrate the Note Diverter sensor.The sensor is located in the note path to the single reject area. The reason for this error could be a broken sensor, or that the sensor is covered with a note";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "An It was not possible to post the note data to the Stacker Control task.error was detected when writing the note data table to the e2prom.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "Not used";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00" ^ status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "the Note Diverter is OK";
                }
                dSeconds.Add(oSecond);
                oSecond = new second();
                position = +2;
                oSecond.task = "Note Stacker";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "An unexpected note was seen in the Note Stacker inlet sensor. The note has not passed the Double detect sensors.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "It was not possible to enable the Note Stacker";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "A note has jammed between the Note Qualifier and the Note Stacker inlet sensor";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "04")
                {
                    oSecond.statuss = "A note has jammed under the Note Stacker inlet sensor";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "Not used in NMD 100";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "06")
                {
                    oSecond.statuss = "A note that was intended to be stacked was rejected instead.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "07")
                {
                    oSecond.statuss = "The stacker wheel has not made a proper movement between the notes, or at emptying of the stacker wheels";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "08")
                {
                    oSecond.statuss = "Not used";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "09")
                {
                    oSecond.statuss = "It was not possible to create mailboxes and queues, the task will not start at all.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00")
                {
                    oSecond.statuss = "The Note Stacker is OK";
                }

                dSeconds.Add(oSecond);
                oSecond = new second();
                position = +2;
                oSecond.task = "Stack presenter";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "Internal return code from Retract if there are no notes to retract.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "The calibration value of the throat sensor or empty grip sensor is too high or two BCU sensors are covered at the same time.A broken or dirty sensor could cause this error.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "The BCU has not reached the defined position";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "04")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to home position after a successful delivery.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to hold position, when using move to hold option(see Item 208)";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "06")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to reject position. Retries are made on this warning";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "07")
                {
                    oSecond.statuss = "An error occurred when moving the BCU from reject to home position. Retries are made on this warning";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "08")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to reject position, or from reject to home position";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "09")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to hold position, when the movement is initiated by a delivery command.The notes are not reachable by the customer.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "10")
                {
                    oSecond.statuss = "Not used.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "11")
                {
                    oSecond.statuss = "An error occurs when trying to close the shutter. This error could also occur if one of the switches in the shutter is broken or if someone opens the shutter with force.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "12")
                {
                    oSecond.statuss = "An error occurs when trying to open the shutter";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "13")
                {
                    oSecond.statuss = "An error is detected in the throat, and the notes are reachable from the customer.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00")
                {
                    oSecond.statuss = "The Stack presenter is OK";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "14")
                {
                    oSecond.statuss = "An error is detected in the throat, probably due to folded note.The notes do not leave the inner throat sensor.Deliver will answer with Successful command.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "15")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to delivery position, but the throat sensor is covered and the grip empty sensor is not covered.Deliver will answer with Successful command.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "16")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to delivery position, but the throat sensor is covered.Deliver will answer with Successful command";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "17")
                {
                    oSecond.statuss = "This error occurs when the customer takes the notes and immediately puts them back into the throat";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "18")
                {
                    oSecond.statuss = "An error occurred when moving the BCU to delivery position. The error could be caused by a skewed BCU, BCU motor not working or something else that prevents the BCU to go to delivery position.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "19")
                {
                    oSecond.statuss = "There are no notes in the BCU on a retract command.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "20")
                {
                    oSecond.statuss = "There is an internal error in the SPC";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "21")
                {
                    oSecond.statuss = "Communication error with the SPC or Reject Vault.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "22")
                {
                    oSecond.statuss = "It was not possible to access the presenter from a task. Another task has the access to main motor transport.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "23")
                {
                    oSecond.statuss = "An error occurs when trying to close the shutter. This error could also occur if one of the switches in the shutter is broken or if someone opens the shutter with force";
                }
                dSeconds.Add(oSecond);

                oSecond = new second();
                position = +2;
                oSecond.task = "Note output";
                if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "00")
                {
                    oSecond.statuss = "The Throat Hadler is OK";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "01")
                {
                    oSecond.statuss = "It was not possible to calibrate the Throat sensor. The reason for this error could be a broken sensor, or that the sensor is covered with a note.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "02")
                {
                    oSecond.statuss = "The note has jammed between the Note transport Path sensor and the Throat sensor.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "03")
                {
                    oSecond.statuss = "The note has jammed under the Throat sensor.";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "04")
                {
                    oSecond.statuss = "Not Used";
                }
                else if (status.DiagnosticarResult.ListEntities[0].Result.ResponseOriginal.Substring(position, 2) == "05")
                {
                    oSecond.statuss = "It was not possible to create semaphores; the task will not start at all.";
                }

                dSeconds.Add(oSecond);

                dgvSecond.DataSource = dSeconds;
            }
            else
            {
                MessageBox.Show("Error al obtener información. Error:"+ status.DiagnosticarResult.Message);
            }
        }
    }
    public class first
    {
        public string hnr { get; set; }
        public string casseteId { get; set; }
        public string open { get; set; }
        public string status { get; set; }
        public string intStatusRV { get; set; }
    }
    public class second
    {
        public string task { get; set; }
        public string statuss { get; set; }
    }
}
