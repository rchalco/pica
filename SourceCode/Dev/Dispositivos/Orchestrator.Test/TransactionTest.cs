using Interop.Main.Cross.Domain.Orchestrator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OrchestratorDevice.Contracts.SavingAccount;
using OrchestratorDevice.Managers;
using OrchestratorDevice.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Orchestrator.Test
{
    [TestClass]
    public class TransactionTest
    {

        string huellaB24 = "AAEAAAD/////AQAAAAAAAAAEAQAAAH9TeXN0ZW0uQ29sbGVjdGlvbnMuR2VuZXJpYy5MaXN0YDFbW1N5c3RlbS5TdHJpbmcsIG1zY29ybGliLCBWZXJzaW9uPTQuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjc3YTVjNTYxOTM0ZTA4OV1dAwAAAAZfaXRlbXMFX3NpemUIX3ZlcnNpb24GAAAICAkCAAAAAgAAAAIAAAARAgAAAAQAAAAGAwAAAPoDPD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RmlkPjxCeXRlcz5BT2duQWNncDQzTmN3RUUzODFhS3E1VmNaMmJSd2NVS3dyZVFEUEY0dXliK095YXdVOC9hZjRXTTAxMkhUdnZGNGJESTU3aWRYTUhoVjlYZnVaaHlyY29JSGV0eHdXQXUva0d0TjBtVFpPUlZvMCtWTUpnN1dzRXREVnhaOWxhRThiUEkrNy9CVmN5SlZRQ1ZFVTNNR3lBTTRPMmRBdUlTMWRWL1FWMDczZEZ2dUo0RHZLYU5ZenVVZDliaHFaL3M1YnlOYUVEUXQxS0t4S3N3LzdndDJBakJnWXQ3VGVlbTZTWjYzVWI1cDg0a1JGemdObW1naWNGY09nZGxBclBqWDRBbVFzakJQd2FyU3VQZ1dCWXBqeFBENzdpaU92U2wxbEFuRHNaOWg4V1RKR2ZUMTQ0bjQ4em1sOXpyemNCbXNESndmRUc5eTNjY0FJQzdNTXEwNHUrZzgvektENHc5UlFZMXdLNkZRN1oxcVlpZjBMU0o2WTF0dld0RC9kUE1BSlpkUE1xbWF2anprMTV2PC9CeXRlcz48Rm9ybWF0PjI8L0Zvcm1hdD48VmVyc2lvbj4xLjAuMDwvVmVyc2lvbj48L0ZpZD4GBAAAAPEDPD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RmlkPjxCeXRlcz5SazFTQUNBeU1BQUFBQUVnQUFBQlpRR0lBTVVBeFFFQUFBQldLMEJRQU5iSFpFQjJBVVJ0WklCbUFTVmdZa0E1QUt1d1gwQlZBS1MxWDRES0FJNWRYa0RiQUR4bFhrQnVBS1M3WG9ES0FHallYWUJDQU1TN1hJRGZBT05WWElEOEFQOWFXa0JQQUgycldJQ2VBVlp6V0lDY0FHeUZWMEM4QVBaVlYwRHVBTDlTVmtEUEFTdGtWVUNZQVRWb1ZVQ2lBTUxRVTBDNkFDdG1Va0RKQURqbVVrRHpBVUp3VWtFRUFJclhVRUNsQVMxalQwQjZBRzZmVFlENUFJbFZUWUIyQUZ5Y1RVRUdBTWROUklCVEFUeHZRb0F6QU1TN1FJQXVBTUd3UUlDZUFLWFZPMENlQUtyVE9rQXRBTkswT1VFWEFJNVZPRUF5QUpVdE9JRCtBRTNaTmdDY0FKdmtOQUFtQUtvbU13QThBSTJtTUFDV0FKcTBNQUVmQUtCYUtnQUE8L0J5dGVzPjxGb3JtYXQ+MTY4NDI3NTM8L0Zvcm1hdD48VmVyc2lvbj4xLjAuMDwvVmVyc2lvbj48L0ZpZD4NAgs=";

        [TestMethod]
        public void DebitTest()
        {
            IntegratorManager integratorManager = new IntegratorManager();
            var requetAuthentication = new OrchestratorDevice.Contracts.SavingAccount.RequestAuthentication
            {
                credential = new OrchestratorDevice.Contracts.SavingAccount.Credential
                {
                    User = "5265200097242338",
                    Password = huellaB24,
                    Channel = 3,
                    AditionalItems = new System.Collections.Generic.List<OrchestratorDevice.Contracts.SavingAccount.AditionalItem>()
                }
            };
            requetAuthentication.credential.AditionalItems.Add(new OrchestratorDevice.Contracts.SavingAccount.AditionalItem
            {
                Key = "IdATM",
                Value = "1"
            });
            requetAuthentication.credential.AditionalItems.Add(new OrchestratorDevice.Contracts.SavingAccount.AditionalItem
            {
                Key = "TypeAuthentication",
                Value = "MasterCard"
            });
            var resulAuthentication = integratorManager.Authentication(requetAuthentication);
            string strSavingAccount = resulAuthentication.Object.AditionalItems.First(x => x.Key == "SavingAccounts").Value;

            List<Account> resulSavingAccounts = JsonConvert.DeserializeObject<List<Account>>(strSavingAccount);

            var resulGetBalanceAccount = integratorManager.GetAccountBalance(new OrchestratorDevice.Contracts.SavingAccount.RequestGetAccountBalance
            {
                requestAccountBalance = new OrchestratorDevice.Contracts.SavingAccount.StructGetAccountBalance
                {
                    CodeSavingsAccount = resulSavingAccounts.First().CodeAccount,
                    IdPerson = "20259626533372712"
                },
                Token = resulAuthentication.Object.token
            });

            var resulDebitAccount = integratorManager.DebitAccount(new RequestDebitAccount
            {
                ObjParameter = new ParameterDebitAccount
                {
                    AmountTrans = 10.56M,
                    CodeSavingAccount = resulSavingAccounts.First().CodeAccount,
                    IdMoneyTrans = 1
                },
                Token = resulAuthentication.Object.token
            });

            Assert.Equals(Convert.ToInt32(resulAuthentication.State), 1);
        }

        [TestMethod]
        public void testPrint()
        {
            //PrinterUtil.SendToPrinter(@"C:\tmp\290856000.pdf");
            PrinterUtil.PrintVoucher("Nro. Transacción: &20752312393465784|Fecha/Hora: &11/05/2023 12:11:43|Ubicacion: &AVAROA|Cuenta Origen: &120-2-1-09393-7|Débito: &BS 300,00|ITF: &BS 0,00|Comisión: &BS 0,00|Saldo: &BS 91.332,57|Cuenta Destino: &117-2-1-00125-5|Crédito: &BS 300,00", "prueba");
        }

        [TestMethod]
        public void enmarcararCuenta()
        {
            string yy = "Cuenta: 120-2-1-30695-7";
            string cuentaOfuscada = "Cuenta: " + yy.Split(' ')[1].Substring(0, 3) + "XXXXXXXXX" + yy.Split(' ')[1].Substring(11, 4);
        }

        [TestMethod]
        public void inicializa()
        {
            try
            {
                GlobalConfigATM globalConfigATM = JsonConvert.DeserializeObject<GlobalConfigATM>(File.ReadAllText(@"C:\HLA\ATM.json"));
                //var DispenserInterop = MicroService.CreateInstance<IDispenserInterop>();
                //var resulDispenser = DispenserInterop.Inicializar(globalConfigATM, true);
                DeviceManager.InitATM(true);

            }
            catch (Exception)
            {

                throw;
            }



        }

        [TestMethod]
        public void TransferAccountManagerTest()
        {
            //TransferAccountManager transferAccountManager = new TransferAccountManager();
            CommonManager manager =new CommonManager();
            var res = manager.GetFrequentAmounts();
        }
        [TestMethod]
        public void TestDeposito()
        {
            RequestCreditAccount requestCredit = new RequestCreditAccount();
            Orchestrator.Services.HandlerMain  handlerMain = new Orchestrator.Services.HandlerMain();
            requestCredit.ObjParameter = new ParmeterCreditAccount();
            requestCredit.ObjParameter.AmountTrans = 10;
            requestCredit.ObjParameter.CodeSavingAccount = "117-2-1-00125-5";
            requestCredit.ObjParameter.IdMoneyTrans = 1;

            var res = handlerMain.DepositAccount(requestCredit);
        }
    }
}
