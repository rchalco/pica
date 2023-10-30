using RegisterLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;
using System.IO;
using System.Dynamic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Net.Http.Headers;

namespace NMDManagement
{
    class services
    {
        #region "URL"
        string url_init = "http://localhost:4000/rest/RuntimeCardReader/InitReader";
        string url_read = "http://localhost:4000/rest/RuntimeCardReader/ReadCard";
        string url_eject = "http://localhost:4000/rest/RuntimeCardReader/EjectCard";
        string url_reset = "http://localhost:4000/rest/RuntimeDispensador/Reset";
        string url_resetLento = "http://localhost:4000/rest/RuntimeDispensador/ResetLento";
        string url_finger_print = "http://localhost:4000/rest/RuntimeFingerPrint/CaptureFinger";
        string url_dispensadorInit = "http://localhost:4000/rest/RuntimeDispensador/Inicializar";
        string url_dispensadorDelivery = "http://localhost:4000/rest/RuntimeDispensador/Dispensar";
        string url_dispensadorDeliveryForce = "http://localhost:4000/rest/RuntimeDispensador/DispensarForce";
        string url_dispensadorDeliveryATM = "http://localhost:4000/rest/RuntimeDispensador/DispensarATM";
        string url_readIdCassette = "http://localhost:4000/rest/RuntimeDispensador/VerificaBanejas";
        string url_releaseCassette = "http://localhost:4000/rest/RuntimeDispensador/LiberarBandeja";
        string url_closeCassette = "http://localhost:4000/rest/RuntimeDispensador/AsegurarBandeja";
        string url_openShutter = "http://localhost:4000/rest/RuntimeDispensador/AbrirShutter";
        string url_closeShutter = "http://localhost:4000/rest/RuntimeDispensador/CerrarShutter";
        string url_stateTable = "http://localhost:4000/rest/RuntimeDispensador/TablaDeEstado";
        string url_clearTable = "http://localhost:4000/rest/RuntimeDispensador/BorrarTabla";
        string url_readIdProgram = "http://localhost:4000/rest/RuntimeDispensador/LeeIdDePrograma";
        string url_readSerieNMD = "http://localhost:4000/rest/RuntimeDispensador/LeeNumeroSerieNMD";
        string url_updateSerieNMD = "http://localhost:4000/rest/RuntimeDispensador/ActualizaNumeroSerieNMD";
        string url_enableCassettes = "http://localhost:4000/rest/RuntimeDispensador/HabilitaDeteccionDeBandeja";
        string url_enableCheckNotes = "http://localhost:4000/rest/RuntimeDispensador/HabilitaValidadorDeEntrega";
        string url_LeeIdBlock = "http://localhost:4000/rest/RuntimeDispensador/LeeIdBlock";
        string url_entregaCMD = "http://localhost:4000/rest/RuntimeDispensador/DispensarCmd";
        string url_stateTableGrl = "http://localhost:4000/rest/RuntimeDispensador/TablaDeEstadosGrl";
        string url_NMDStatus = "http://localhost:4000/rest/RuntimeDispensador/Diagnosticar";
        string url_selfTest0 = "http://localhost:4000/rest/RuntimeDispensador/SelfTest0";
        string url_selfTest1 = "http://localhost:4000/rest/RuntimeDispensador/SelfTest1";
        string url_selfTest9 = "http://localhost:4000/rest/RuntimeDispensador/SelfTest9";
        string url_selfTestA = "http://localhost:4000/rest/RuntimeDispensador/SelfTestA";
        string url_NQInitialisation = "http://localhost:4000/rest/RuntimeDispensador/BorrarNQ";
        string url_InitATM = "http://localhost:4000/rest/Orchestrator/InitATM";
        string url_LimpiarNS = "http://localhost:4000/rest/RuntimeDispensador/LimpiarNSRollers";
        string url_LimpiarNF = "http://localhost:4000/rest/RuntimeDispensador/LimpiarNF";
        string url_ReadStatusShutter = "http://localhost:4000/rest/RuntimeDispensador/LeeEstadoShutter";
        string url_WriteStatusShutter = "http://localhost:4000/rest/RuntimeDispensador/ActualizaEstadoShutter";
        string url_GetHotState= "http://localhost:4000/rest/Orchestrator/GetHotState";

        string utl_validateFinger = GetAppSetting.GetSetting("URLValidate") + "/HangarSafeGate/rest/VerifyUser";

        string utl_AccessClose = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/ATMAccessHistoryClose";
        string url_getCashCounter = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/GetATMInformationToCashCountByIdATM";
        string url_insertCashCounter = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/InsertAtmCashCountDetail";
        string url_loadSolicitationPending = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/ATMLoadSolicitationGetPendingForATM";
        string url_loadSolicitationProccess = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/ATMLoadSolicitationConfirmProcess";
        string url_createTicket = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/CreateTicketForOperationExternalService";
        string url_getATMs = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/GetAllATM";
        string url_getATM = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/GetATMById";
        string url_updateATM = GetAppSetting.GetSetting("URLServices") + "/rest/ATM.Services.IAtmServices/UpdateATM";



        #endregion

        #region "Dispensador"
        public void ExcecuteNMD(int pProcess)
        {
            loggerATM.PsRegisterLogger("CallFunctionNMD", "Ingreso a funcion de llamada a clase de dispensador para el proceso " + pProcess.ToString());

            switch (pProcess)
            {
                case 1:

                    break;
            }
        }

        public ResponseInit initializa_dispenser()
        {
            services servicios = new services();
            ResultConfgATM globalConfigATM = servicios.GetHotState();
            string txtConfigATM = string.Empty;
            Parameter param = new Parameter() { };
            ResponseInit cardResult = new ResponseInit();
            
            GlobalConfigATM configATM = globalConfigATM.GetHotStateResult.Object;
            txtConfigATM = Newtonsoft.Json.JsonConvert.SerializeObject  (configATM);
            try
            {
                string parameterObj = " { \"globalConfigATM\":" + txtConfigATM + " }";
                //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
                var content = new StringContent(parameterObj, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_dispensadorInit);

                    var resultServices = client.PostAsync(url_dispensadorInit, content).Result;
                    loggerATM.PsRegisterLogger("initialize_dispenser", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<ResponseInit>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                cardResult.InicializarResult.State = ResponseType.Error;
                cardResult.InicializarResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public ResponseDelivery DeliveryCash(int pAmount)
        {
            dynamic param = new { pAmount = pAmount };
            ResponseDelivery cardResult = new ResponseDelivery();

            try
            {
                //string parameterObj = " { \"globalConfigATM\":" + File.ReadAllText(pathConfigATM) + " }";
                //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_dispensadorDelivery);

                    var resultServices = client.PostAsync(url_dispensadorDelivery, content).Result;
                    loggerATM.PsRegisterLogger("DeliveryCash", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<ResponseDelivery>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                cardResult.DispensarResult.State = ResponseType.Error;
                cardResult.DispensarResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public ResponseDeliveryForce DeliveryCashForce(int pAmount)
        {
            dynamic param = new { pAmount = pAmount };
            ResponseDeliveryForce cardResult = new ResponseDeliveryForce();

            try
            {
                //string parameterObj = " { \"globalConfigATM\":" + File.ReadAllText(pathConfigATM) + " }";
                //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_dispensadorDeliveryForce);

                    var resultServices = client.PostAsync(url_dispensadorDeliveryForce, content).Result;
                    string resultadoService = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("DeliveryCashForce", "Respuesta Servicio:" + resultadoService);
                    cardResult = JsonConvert.DeserializeObject<ResponseDeliveryForce>(resultadoService);
                }
            }
            catch (Exception error)
            {
                cardResult.DispensarForceResult.State = ResponseType.Error;
                cardResult.DispensarForceResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public ResponseDispensarATM DeliveryCashATM(int pAmount)
        {
            dynamic param = new { pAmount = pAmount };
            ResponseDispensarATM cardResult = new ResponseDispensarATM();

            try
            {
                //string parameterObj = " { \"globalConfigATM\":" + File.ReadAllText(pathConfigATM) + " }";
                //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_dispensadorDeliveryATM);

                    var resultServices = client.PostAsync(url_dispensadorDeliveryATM, content).Result;
                    loggerATM.PsRegisterLogger("DeliveryCashATM", "Respuesta:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<ResponseDispensarATM>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                cardResult.DispensarATMResult.State = ResponseType.Error;
                cardResult.DispensarATMResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }
        public ResponseDispensarCMD DeliveryCashCMD(string pParam)
        {
            dynamic param = new { pParam = pParam };
            ResponseDispensarCMD cardResult = new ResponseDispensarCMD();

            try
            {
                //string parameterObj = " { \"globalConfigATM\":" + File.ReadAllText(pathConfigATM) + " }";
                //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(pathConfigATM));
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_entregaCMD);

                    var resultServices = client.PostAsync(url_entregaCMD, content).Result;
                    loggerATM.PsRegisterLogger("DeliveryCashCMD", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<ResponseDispensarCMD>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                cardResult.DispensarCmdResult.State = ResponseType.Error;
                cardResult.DispensarCmdResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }
        public ResponseReadIdCassette ReadIdCassette()
        {
            dynamic param = new { };
            ResponseReadIdCassette readId = new ResponseReadIdCassette();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_readIdCassette);

                    var resultServices = client.PostAsync(url_readIdCassette, content).Result;
                    loggerATM.PsRegisterLogger("ReadIdCassette", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseReadIdCassette>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.VerificaBanejasResult.State = ResponseType.Error;
                readId.VerificaBanejasResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseReleaseCassette ReleaseCassette()
        {
            dynamic param = new { };
            ResponseReleaseCassette readId = new ResponseReleaseCassette();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_releaseCassette);

                    var resultServices = client.PostAsync(url_releaseCassette, content).Result;
                    loggerATM.PsRegisterLogger("ReleaseCassette", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseReleaseCassette>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.LiberarBandejaResult.State = ResponseType.Error;
                readId.LiberarBandejaResult.Message = error.Message;
                //throw;
            }
            return readId;
        }
        public ResponseCloseCassette CloseCassette()
        {
            dynamic param = new { };
            ResponseCloseCassette readId = new ResponseCloseCassette();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_closeCassette);

                    var resultServices = client.PostAsync(url_closeCassette, content).Result;
                    loggerATM.PsRegisterLogger("CloseCassette", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseCloseCassette>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.AsegurarBandejaResult.State = ResponseType.Error;
                readId.AsegurarBandejaResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseOpenShutter OpenShutter()
        {
            dynamic param = new { };
            ResponseOpenShutter readId = new ResponseOpenShutter();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_openShutter);

                    var resultServices = client.PostAsync(url_openShutter, content).Result;
                    loggerATM.PsRegisterLogger("OpenShutter", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseOpenShutter>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.AbrirShutterResult.State = ResponseType.Error;
                readId.AbrirShutterResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseCloseShutter CloseShutter()
        {
            dynamic param = new { };
            ResponseCloseShutter readId = new ResponseCloseShutter();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_closeShutter);

                    var resultServices = client.PostAsync(url_closeShutter, content).Result;
                    loggerATM.PsRegisterLogger("CloseShutter", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseCloseShutter>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.CerrarShutterResult.State = ResponseType.Error;
                readId.CerrarShutterResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseReadStatusShutter ReadStatusShutter()
        {
            dynamic param = new { };
            ResponseReadStatusShutter readId = new ResponseReadStatusShutter();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_ReadStatusShutter);

                    var resultServices = client.PostAsync(url_ReadStatusShutter, content).Result;
                    loggerATM.PsRegisterLogger("ReadStatusShutter", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseReadStatusShutter>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.LeeEstadoShutterResult.State = ResponseType.Error;
                readId.LeeEstadoShutterResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseWriteStatusShutter WriteStatusShutter(string pStatus)
        {
            dynamic param = new { pEstado = pStatus };
            ResponseWriteStatusShutter readId = new ResponseWriteStatusShutter();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_WriteStatusShutter);

                    var resultServices = client.PostAsync(url_WriteStatusShutter, content).Result;
                    loggerATM.PsRegisterLogger("WriteStatusShutter", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseWriteStatusShutter>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.ActualizaEstadoShutterResult.State = ResponseType.Error;
                readId.ActualizaEstadoShutterResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseStateTable StateTable()
        {
            dynamic param = new { };
            ResponseStateTable readId = new ResponseStateTable();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_stateTable);

                    var resultServices = client.PostAsync(url_stateTable, content).Result;
                    loggerATM.PsRegisterLogger("StateTable", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseStateTable>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.TablaDeEstadoResult.State = ResponseType.Error;
                readId.TablaDeEstadoResult.Message = error.Message;
                //throw;
            }
            return readId;
        }

        public ResponseStateTableGrl StateTableGnl()
        {
            dynamic param = new { };
            ResponseStateTableGrl readId = new ResponseStateTableGrl();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_stateTableGrl);

                    var resultServices = client.PostAsync(url_stateTableGrl, content).Result;
                    loggerATM.PsRegisterLogger("StateTableGnl", "Respuesta Servicio:" + resultServices.ToString());
                    readId = JsonConvert.DeserializeObject<ResponseStateTableGrl>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                readId.TablaDeEstadosGrlResult.State = ResponseType.Error;
                readId.TablaDeEstadosGrlResult.Message = error.Message;
                //throw;
            }
            return readId;
        }
        public ResponseReset Reset()
        {
            dynamic param = new { };
            ResponseReset response = new ResponseReset();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_reset);

                    var resultServices = client.PostAsync(url_reset, content).Result;
                    loggerATM.PsRegisterLogger("Reset", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseReset>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.ResetResult.State = ResponseType.Error;
                response.ResetResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseResetLento ResetLento()
        {
            dynamic param = new { };
            ResponseResetLento response = new ResponseResetLento();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_resetLento);

                    var resultServices = client.PostAsync(url_resetLento, content).Result;
                    loggerATM.PsRegisterLogger("ResetLento", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseResetLento>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.ResetLentoResult.State = ResponseType.Error;
                response.ResetLentoResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseClearTable ClearTable()
        {
            dynamic param = new { };
            ResponseClearTable response = new ResponseClearTable();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_clearTable);

                    var resultServices = client.PostAsync(url_clearTable, content).Result;
                    loggerATM.PsRegisterLogger("ClearTable", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseClearTable>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.BorrarTablaResult.State = ResponseType.Error;
                response.BorrarTablaResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseReadIdProgram ReadIdProgram()
        {
            dynamic param = new { };
            ResponseReadIdProgram response = new ResponseReadIdProgram();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_readIdProgram);

                    var resultServices = client.PostAsync(url_readIdProgram, content).Result;
                    loggerATM.PsRegisterLogger("ReadIdProgram", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseReadIdProgram>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.LeeIdDeProgramaResult.State = ResponseType.Error;
                response.LeeIdDeProgramaResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseReadSerieNMD ReadSerieNMD()
        {
            dynamic param = new { };
            ResponseReadSerieNMD response = new ResponseReadSerieNMD();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_readSerieNMD);

                    var resultServices = client.PostAsync(url_readSerieNMD, content).Result;
                    loggerATM.PsRegisterLogger("ReadSerieNMD", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseReadSerieNMD>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.LeeNumeroSerieNMDResult.State = ResponseType.Error;
                response.LeeNumeroSerieNMDResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseUpdateSerieNMD UpdateSerieNMD(string pSerieNMD)
        {
            dynamic param = new { pSerie = pSerieNMD };
            ResponseUpdateSerieNMD response = new ResponseUpdateSerieNMD();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_updateSerieNMD);

                    var resultServices = client.PostAsync(url_updateSerieNMD, content).Result;
                    loggerATM.PsRegisterLogger("UpdateSerieNMD", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseUpdateSerieNMD>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.ActualizaNumeroSerieNMDResult.State = ResponseType.Error;
                response.ActualizaNumeroSerieNMDResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseEnableCassettes EnableCassettes()
        {
            dynamic param = new { };
            ResponseEnableCassettes response = new ResponseEnableCassettes();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_enableCassettes);

                    var resultServices = client.PostAsync(url_enableCassettes, content).Result;
                    loggerATM.PsRegisterLogger("EnableCassettes", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseEnableCassettes>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.HabilitaDeteccionDeBandejaResult.State = ResponseType.Error;
                response.HabilitaDeteccionDeBandejaResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseEnabledCheckNotes EnableCheckNotes()
        {
            dynamic param = new { };
            ResponseEnabledCheckNotes response = new ResponseEnabledCheckNotes();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_enableCheckNotes);

                    var resultServices = client.PostAsync(url_enableCheckNotes, content).Result;
                    loggerATM.PsRegisterLogger("EnableCheckNotes", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseEnabledCheckNotes>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.HabilitaValidadorDeEntregaResult.State = ResponseType.Error;
                response.HabilitaValidadorDeEntregaResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseReadIdBlock ReadIdBlock()
        {
            dynamic param = new { };
            ResponseReadIdBlock response = new ResponseReadIdBlock();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_LeeIdBlock);

                    var resultServices = client.PostAsync(url_LeeIdBlock, content).Result;
                    loggerATM.PsRegisterLogger("ReadIdBlock", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseReadIdBlock>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.LeeIdBlockResult.State = ResponseType.Error;
                response.LeeIdBlockResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseNMDStatus NMDStatus()
        {
            dynamic param = new { };
            ResponseNMDStatus response = new ResponseNMDStatus();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_NMDStatus);

                    var resultServices = client.PostAsync(url_NMDStatus, content).Result;
                    loggerATM.PsRegisterLogger("NMDStatus", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseNMDStatus>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.DiagnosticarResult.State = ResponseType.Error;
                response.DiagnosticarResult.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseSelfTest0 SelfTest0()
        {
            dynamic param = new { };
            ResponseSelfTest0 response = new ResponseSelfTest0();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_selfTest0);

                    var resultServices = client.PostAsync(url_selfTest0, content).Result;
                    loggerATM.PsRegisterLogger("SelfTest0", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseSelfTest0>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.SelfTest0Result.State = ResponseType.Error;
                response.SelfTest0Result.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseSelfTest1 SelfTest1()
        {
            dynamic param = new { };
            ResponseSelfTest1 response = new ResponseSelfTest1();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_selfTest1);

                    var resultServices = client.PostAsync(url_selfTest1, content).Result;
                    loggerATM.PsRegisterLogger("SelfTest1", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseSelfTest1>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.SelfTest1Result.State = ResponseType.Error;
                response.SelfTest1Result.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseSelfTest9 SelfTest9()
        {
            dynamic param = new { };
            ResponseSelfTest9 response = new ResponseSelfTest9();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_selfTest9);

                    var resultServices = client.PostAsync(url_selfTest9, content).Result;
                    loggerATM.PsRegisterLogger("SelfTest9", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseSelfTest9>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.SelfTest9Result.State = ResponseType.Error;
                response.SelfTest9Result.Message = error.Message;
                //throw;
            }
            return response;
        }

        public ResponseSelfTestA SelfTestA()
        {
            dynamic param = new { };
            ResponseSelfTestA response = new ResponseSelfTestA();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(url_selfTestA);

                    var resultServices = client.PostAsync(url_selfTestA, content).Result;
                    loggerATM.PsRegisterLogger("SelfTestA", "Respuesta Servicio:" + resultServices.ToString());
                    response = JsonConvert.DeserializeObject<ResponseSelfTestA>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.SelfTestAResult.State = ResponseType.Error;
                response.SelfTestAResult.Message = error.Message;
                //throw;
            }
            return response;
        }
        #endregion

        #region "Huella"
        public DataCaptureFinger Capture_FingerPrint(int pTimeOut)
        {
            dynamic dataParam = new { timeOut = pTimeOut };

            DataCaptureFinger cardResult = new DataCaptureFinger();
            string result = "";
            FrmImgFinger frmImgFinger = new FrmImgFinger(2);

            frmImgFinger.fingerPrintNotVisible();
            try
            {
                frmImgFinger.Show();
                var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_finger_print);
                    var resultServices = client.PostAsync(url_finger_print, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    loggerATM.PsRegisterLogger("Capture_FingerPrint", "Respuesta Servicio:" + resultServices.ToString());
                    loggerATM.PsRegisterLogger("Capture_FingerPrint", "Respuesta 2:" + result);
                    cardResult = JsonConvert.DeserializeObject<DataCaptureFinger>(result);

                    if (cardResult.CaptureFingerResult.State == ResponseType.Success)
                    {
                        frmImgFinger.fingerPrintVisible();
                        frmImgFinger.Refresh();
                        Thread.Sleep(500);
                    }

                }
            }
            catch (Exception error)
            {
                cardResult.CaptureFingerResult=new DataCaptureFinger.ReadResult();
                cardResult.CaptureFingerResult.State = ResponseType.Error;
                cardResult.CaptureFingerResult.Message = error.Message;

            }
            frmImgFinger.Close();
            return cardResult;
        }
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public ResponseValidateFinger ValidateUserWithFingerprint(string pUser, DataCaptureFinger pFigerprit, int pIdATM)
        {
            dynamic paramIni = new ExpandoObject();

            loggerATM.PsRegisterLogger("ValidaUser", "Ingresa a validaro");
            dynamic credenciales = new ExpandoObject();

            credenciales.User = pUser;
            credenciales.Password = pFigerprit.CaptureFingerResult.Message;
            credenciales.Channel = 4;
            credenciales.AditionalItems = new[]
            {
                new {Key = "passwordisHashed", Value = "false" },
                new {Key = "isFingerPrint", Value = "true" },
                new {Key = "IdATM", Value = pIdATM.ToString()  }
            };
            paramIni.credential = credenciales;

            string input = Newtonsoft.Json.JsonConvert.SerializeObject(paramIni);

            ResponseValidateFinger response = new ResponseValidateFinger();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(paramIni), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.BaseAddress = new Uri(utl_validateFinger);

                    var resultServices = client.PostAsync(utl_validateFinger, content).Result;
                    loggerATM.PsRegisterLogger("ValidaUser", "Respuesta:" + resultServices.Content.ReadAsStringAsync().Result.ToString());
                    response = JsonConvert.DeserializeObject<ResponseValidateFinger>(resultServices.Content.ReadAsStringAsync().Result);
                    loggerATM.PsRegisterLogger("ValidaUser", "Respuesta correcta de comparación de huella" );
                }
            }
            catch (Exception error)
            {
                loggerATM.PsRegisterLogger("ValidaUser", "Error:" + error.Message);
                response.VerifyUserResult = new ResponseValidateFinger.ReadResult();
                response.VerifyUserResult.State = ResponseType.Error;
                response.VerifyUserResult.Message = error.Message;
                
                //throw;
            }
            return response;
        }



        public ResponseValidateFinger ValidateUserWithPassword(string pUser, string pPass, int pIdATM)
        {
            dynamic paramIni = new ExpandoObject();

            loggerATM.PsRegisterLogger("ValidaUser", "Ingresa a validaro");
            dynamic credenciales = new ExpandoObject();

            credenciales.User = pUser;
            credenciales.Password = pPass;
            credenciales.Channel = 4;
            credenciales.AditionalItems = new[]
            {
                new {Key = "passwordisHashed", Value = "false" },
                new {Key = "isFingerPrint", Value = "false" },
                new {Key = "IdATM", Value = pIdATM.ToString()  }
            };
            paramIni.credential = credenciales;

            string input = Newtonsoft.Json.JsonConvert.SerializeObject(paramIni);

            ResponseValidateFinger response = new ResponseValidateFinger();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(paramIni), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.BaseAddress = new Uri(utl_validateFinger);

                    var resultServices = client.PostAsync(utl_validateFinger, content).Result;
                    loggerATM.PsRegisterLogger("ValidaUser", "Respuesta:" + resultServices.Content.ReadAsStringAsync().Result.ToString());
                    response = JsonConvert.DeserializeObject<ResponseValidateFinger>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                response.VerifyUserResult = new ResponseValidateFinger.ReadResult();
                response.VerifyUserResult.State = ResponseType.Error;
                response.VerifyUserResult.Message = error.Message;
                loggerATM.PsRegisterLogger("ValidaUser", "Error:" + error.Message);
                //throw;
            }
            return response;
        }
        public DataCaptureFinger Capture_FingerPrintTest(int pTimeOut)
        {
            dynamic dataParam = new { timeOut = pTimeOut };
            //BitacoraWriter.RegisterTraceSO("Debug", "1. Iniciando la carptura. URL:" + url_finger_print + ". Tiempo de espera: " + pTimeOut.ToString());
            DataCaptureFinger cardResult = new DataCaptureFinger();
            string result = "";
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_finger_print);
                    var resultServices = client.PostAsync(url_finger_print, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    //BitacoraWriter.RegisterTraceSO("Debug", "2. Resultado del servicio:" + result);

                    cardResult = JsonConvert.DeserializeObject<DataCaptureFinger>(result);
                    //BitacoraWriter.RegisterTraceSO("Debug", "3. Se lo deserealizar");
                }
            }
            catch (Exception error)
            {
                cardResult.CaptureFingerResult.State = ResponseType.Error;
                cardResult.CaptureFingerResult.Message = error.Message;
                //BitacoraWriter.RegisterTraceSO("Error", "Error en la espera del servicio: " + error.Message);
            }
            return cardResult;
        }


        //public ResponseValidateFingerClose ValidateUserAccessClose2(Int64 pIdAccess, string pToken)
        //{

        //}
        public ResponseValidateFingerClose ValidateUserAccessClose(Int64 pIdAccess, string pToken)
        {
            //se debe obtener de otra instancia el numero de ATM
            loggerATM.PsRegisterLogger("ValidateUserAccessClose", "Cerrar Sesion con ID:" + pIdAccess.ToString());
            dynamic dataParam = new { objATMAccessHistory = new { IdATMAccessHistory = pIdAccess } };
            ResponseValidateFingerClose urlResult = new ResponseValidateFingerClose();
            
            //GlobalConfigATM globalConfigATM = Newtonsoft.Json.JsonConvert.DeserializeObject<GlobalConfigATM>(jsonContent);
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(utl_AccessClose);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    //client.DefaultRequestHeaders.Add("NewStateATM", JsonConvert.SerializeObject(globalConfigATM));

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(utl_AccessClose, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("ValidateUserAccessClose", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<ResponseValidateFingerClose>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.ATMAccessHistoryCloseResult = new ResponseValidateFingerClose.ReadResult();
                urlResult.ATMAccessHistoryCloseResult.State = ResponseType.Error;
                urlResult.ATMAccessHistoryCloseResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }
        #endregion

        #region "Tarjeta"
        public CardResultIni CardReader_Init(int CardReaderType)
        {

            Parameter param = new Parameter() { typeReader = CardReaderType };
            CardResultIni cardResult = new CardResultIni();

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_init);
                    var resultServices = client.PostAsync(url_init, content).Result;
                    loggerATM.PsRegisterLogger("CardReader_Init", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<CardResultIni>(resultServices.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception error)
            {
                cardResult.InitReaderResult.State = ResponseType.Error;
                cardResult.InitReaderResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public CardResultRead CardReader_read()
        {

            //Parameter param = new Parameter() { typeReader = CardReaderType };
            dynamic dataParam = new { };
            CardResultRead cardResult = new CardResultRead();
            string result = "";
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_read);
                    var resultServices = client.PostAsync(url_read, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("CardReader_read", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<CardResultRead>(result);

                }
            }
            catch (Exception error)
            {
                cardResult.ReadCardResult.State = ResponseType.Error;
                cardResult.ReadCardResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public CardResultEject CardReader_eject()
        {

            //Parameter param = new Parameter() { typeReader = CardReaderType };
            dynamic dataParam = new { };
            CardResultEject cardResult = new CardResultEject();
            string result = "";
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_eject);
                    var resultServices = client.PostAsync(url_eject, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("CardReader_eject", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<CardResultEject>(result);

                }
            }
            catch (Exception error)
            {
                cardResult.EjectCardResult.State = ResponseType.Error;
                cardResult.EjectCardResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }

        public CardResultReset CardReader_reset()
        {

            //Parameter param = new Parameter() { typeReader = CardReaderType };
            dynamic dataParam = new { };
            CardResultReset cardResult = new CardResultReset();
            string result = "";
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(""), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_reset);
                    var resultServices = client.PostAsync(url_reset, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("CardReader_reset", "Respuesta Servicio:" + resultServices.ToString());
                    cardResult = JsonConvert.DeserializeObject<CardResultReset>(result);

                }
            }
            catch (Exception error)
            {
                cardResult.ResetResult.State = ResponseType.Error;
                cardResult.ResetResult.Message = error.Message;
                //throw;
            }
            return cardResult;
        }
        #endregion

        #region "Operaciones administrativas"
        public ResultRecordForBalance GetRecordForBalance(int pIdATM, string pToken)
        {

            //se debe obtener de otra instancia el numero de ATM
            dynamic dataParam = new { idAtmEntity = pIdATM };
            ResultRecordForBalance urlResult = new ResultRecordForBalance();
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_getCashCounter);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_getCashCounter, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("GetRecordForBalance", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<ResultRecordForBalance>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.GetATMInformationToCashCountByIdATMResult = new ResultRecordForBalance.ReadResult();
                urlResult.GetATMInformationToCashCountByIdATMResult.State = ResponseType.Error;
                urlResult.GetATMInformationToCashCountByIdATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public ResponseObject InsertRecordForBalance(string pToken, ATMCashCountDTO pParam)
        {

            //se debe obtener de otra instancia el numero de ATM
            //dynamic dataParam = new { idAtmEntity = pIdATM };
            dynamic dataParam = JsonConvert.SerializeObject(pParam);
            ResponseObject urlResult = new ResponseObject();
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_insertCashCounter);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(pParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_insertCashCounter, content).Result;
                    loggerATM.PsRegisterLogger("InsertRecordForBalance", "Respuesta Servicio:" + resultServices.ToString());
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ResponseObject>(result);

                }
            }
            catch (Exception error)
            {
                urlResult = new ResponseObject();
                urlResult.InsertAtmCashCountDetailResult.Code = "Error";
                urlResult.InsertAtmCashCountDetailResult.reponseCount.ErrorDescription = error.Message;
                //throw;
            }
            return urlResult;
        }

        /// <summary>
        /// cargas pendientes
        /// </summary>
        /// <param name="pIdATM">id de ATM</param>
        /// <param name="pToken">Token de log in</param>
        /// <returns></returns>
        public ATMLoadSolicitationPending ATMLoadSolicitationGetPending(int pIdATM, string pToken)
        {
            dynamic dataParam = new { idATM = pIdATM };
            ATMLoadSolicitationPending urlResult = new ATMLoadSolicitationPending();
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_loadSolicitationPending);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_loadSolicitationPending, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("ATMLoadSolicitationGetPending", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<ATMLoadSolicitationPending>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.ATMLoadSolicitationGetPendingForATMResult = new ATMLoadSolicitationPending.ReadResult();
                urlResult.ATMLoadSolicitationGetPendingForATMResult.State = ResponseType.Error;
                urlResult.ATMLoadSolicitationGetPendingForATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public ATMLoadSolicitationConfirmProcess ATMLoadSolicitationConfirmProcess(long pIdSolicitation, long pIdPerson, string pToken)
        {
            dynamic dataParam = new { idATMLoadSolicitation = pIdSolicitation, idUser = pIdPerson };
            ATMLoadSolicitationConfirmProcess urlResult = new ATMLoadSolicitationConfirmProcess();
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_loadSolicitationProccess);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_loadSolicitationProccess, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("ATMLoadSolicitationConfirmProcess", "Respuesta Servicio:" + result);
                    urlResult = JsonConvert.DeserializeObject<ATMLoadSolicitationConfirmProcess>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.ATMLoadSolicitationConfirmProcessResult = new ATMLoadSolicitationConfirmProcess.ReadResult();
                urlResult.ATMLoadSolicitationConfirmProcessResult.State = ResponseType.Error;
                urlResult.ATMLoadSolicitationConfirmProcessResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }


        public RecuestRegisterTicket RegisterTicket(ParameterTicket parameterTicket, string pToken)
        {
            //dynamic dataParam = new { idATMLoadSolicitation = pIdSolicitation, idUser = pIdPerson };
            RecuestRegisterTicket urlResult = new RecuestRegisterTicket();
            string result = "";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_createTicket);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(parameterTicket), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_createTicket, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("RegisterTicket", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<RecuestRegisterTicket>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.CreateTicketForOperationExternalServiceResult = new RecuestRegisterTicket.ReadResult();
                urlResult.CreateTicketForOperationExternalServiceResult.State = ResponseType.Error;
                urlResult.CreateTicketForOperationExternalServiceResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        #endregion

        #region "Administration"
        public ConfigurationATM GetATM(string pToken, int pIdATM)
        {
            dynamic dataParam = new { idATM = pIdATM };
            ConfigurationATM urlResult = new ConfigurationATM();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_getATM);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_getATM, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ConfigurationATM>(result);

                }
            }
            catch (Exception error)
            {
                urlResult.GetATMByIdResult = new ConfigurationATM.ReadResult();
                urlResult.GetATMByIdResult.State = ResponseType.Error;
                urlResult.GetATMByIdResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public AllATMResult GetAllATM(string pToken)
        {
            dynamic dataParam = new { };
            AllATMResult urlResult = new AllATMResult();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_getATMs);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_getATMs, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<AllATMResult>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.GetAllATMResult = new AllATMResult.ReadResult();
                urlResult.GetAllATMResult.State = ResponseType.Error;
                urlResult.GetAllATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public ResultUpdateATM UpadateATM(ATMDTO pConfigurationATM, string pToken)
        {
            dynamic dataParam = new { atmDTO = pConfigurationATM };
            ResultUpdateATM urlResult = new ResultUpdateATM();
            string result = "";
            try
            {
                pConfigurationATM.Profile.InactivationDate = null;

                loggerATM.PsRegisterLogger("UpadateATM", "Parametro: " + JsonConvert.SerializeObject(pConfigurationATM));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_updateATM);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");
                    loggerATM.PsRegisterLogger("UpadateATM", "ante de enviar al servicio: " + JsonConvert.SerializeObject(dataParam));
                    var resultServices = client.PostAsync(url_updateATM, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;
                    loggerATM.PsRegisterLogger("UpadateATM", "Respuesta Servicio:" + resultServices.ToString());
                    urlResult = JsonConvert.DeserializeObject<ResultUpdateATM>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.UpdateATMResult = new ResultUpdateATM.ReadResult();
                urlResult.UpdateATMResult.State = ResponseType.Error;
                urlResult.UpdateATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public RestultIniATM InitATM(string pToken)
        {
            dynamic dataParam = new { force = true };
            RestultIniATM urlResult = new RestultIniATM();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_InitATM);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_InitATM, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<RestultIniATM>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.InitATMResult = new RestultIniATM.ReadResult();
                urlResult.InitATMResult.State = ResponseType.Error;
                urlResult.InitATMResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public ResultClearNQ BorraNQ(string pToken)
        {
            dynamic dataParam = new { };
            ResultClearNQ urlResult = new ResultClearNQ();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_NQInitialisation);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    if (!string.IsNullOrEmpty(pToken))
                    {
                        client.DefaultRequestHeaders.Add("HangarAuthentication", pToken);
                    }
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_NQInitialisation, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ResultClearNQ>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.BorrarNQResult = new ResultClearNQ.ReadResult();
                urlResult.BorrarNQResult.State = ResponseType.Error;
                urlResult.BorrarNQResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }

        public ResultCleanNS LImpiarNS()
        {
            dynamic dataParam = new { };
            ResultCleanNS urlResult = new ResultCleanNS();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_LimpiarNS);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_LimpiarNS, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ResultCleanNS>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.LimpiarNSRollersResult = new ResultCleanNS.ReadResult();
                urlResult.LimpiarNSRollersResult.State = ResponseType.Error;
                urlResult.LimpiarNSRollersResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }
        
        public ResultCleanNF LImpiarNF(int pNF)
        {
            dynamic dataParam = new { pNF = pNF };
            ResultCleanNF urlResult = new ResultCleanNF();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_LimpiarNF);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_LimpiarNF, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ResultCleanNF>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.LimpiarNFResult = new ResultCleanNF.ReadResult();
                urlResult.LimpiarNFResult.State = ResponseType.Error;
                urlResult.LimpiarNFResult.Message = error.Message;
                //throw;
            }
            return urlResult;
        }
        #endregion

        public ResultConfgATM GetHotState()
        {
            dynamic dataParam = new { };
            ResultConfgATM urlResult = new ResultConfgATM();
            string result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url_GetHotState);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept", "*/*");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    client.Timeout = TimeSpan.FromSeconds(90);
                    var content = new StringContent(JsonConvert.SerializeObject(dataParam), Encoding.UTF8, "application/json");

                    var resultServices = client.PostAsync(url_GetHotState, content).Result;
                    result = resultServices.Content.ReadAsStringAsync().Result;

                    urlResult = JsonConvert.DeserializeObject<ResultConfgATM>(result);
                }
            }
            catch (Exception error)
            {
                urlResult.GetHotStateResult  = new ResultConfgATM.ReadResult();
                urlResult.GetHotStateResult.State = ResponseType.Error;
                urlResult.GetHotStateResult .Message = error.Message;
                //throw;
            }
            return urlResult;
        }
    }
}


