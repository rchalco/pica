using System.Collections;

using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Interop.Main.Cross.Domain.Dispenser.Enumerators;
using Interop.Main.Cross.Domain.Dispenser;

namespace RuntimeDispensador.Core
{
    /// <summary>
    /// Clase que fabiracara el dispensador en base a la prametrizacion relizada en la
    /// aplicacion, mismo que sera inyectado a la clase
    /// </summary>
    public class GetwayDispensador
    {
        public static int IdATM { get; set; }
        public static string Port { get; set; }
        public class CommandRequest
        {
            public Comando command { get; set; }
            public string parameter { get; set; }
            public string AdditionalInformation { get; set; }
        }

        private GetwayDispensador()
        {

        }

        static GetwayDispensador currentInstance { get; set; }

        public class BatchCommand
        {


            public BatchCommand(Comando _initCommand, string _parameters = "")
            {
                initCommand = _initCommand;
                parameters = _parameters;
            }

            private Queue<Func<StatusDispenser, CommandRequest>> MapCommand;
            public Comando initCommand { get; set; }
            public string parameters { get; set; }

            public void AddComand(Func<StatusDispenser, CommandRequest> expEvaluate)
            {
                if (MapCommand == null)
                {
                    MapCommand = new Queue<Func<StatusDispenser, CommandRequest>>();
                }
                MapCommand.Enqueue(expEvaluate);
            }

            public CommandRequest GetNextCommand(StatusDispenser status)
            {
                if (MapCommand.Count == 0)
                {
                    return null;
                }
                CommandRequest req = MapCommand.Dequeue().Invoke(status);
                return req;
            }

        }

        private static Queue<BatchCommand> QueueBatches;

        public void AddBatches(BatchCommand batch)
        {
            QueueBatches.Enqueue(batch);
        }
        public void ClearBatchs()
        {
            QueueBatches.Clear();
        }

        public List<Trace> ExecuteBatch()
        {
            List<Trace> status = new List<Trace>();
            lock (this)
            {
                lock (QueueBatches)
                {
                    if (QueueBatches.Count == 0)
                    {
                        return null;
                    }
                    BatchCommand cmdBatch = QueueBatches.Dequeue();
                    Trace trace = new Trace()
                    {
                        IdCommand = cmdBatch.initCommand,
                        CommandName = cmdBatch.initCommand.ToString(),
                        AdditionalInformation = string.Empty,
                        DateCreate = DateTime.Now,
                        Parameter = cmdBatch.parameters
                    };
                    trace.Result = ExecutorCommand.ejecutar(cmdBatch.initCommand, cmdBatch.parameters);
                    trace.DateExecution = DateTime.Now;
                    status.Add(trace);
                    CommandRequest request = cmdBatch.GetNextCommand(status.First().Result);
                    while (request != null)
                    {
                        if (request.command == Comando.Fin)
                        {
                            break;
                        }
                        trace = new Trace()
                        {
                            IdCommand = request.command,
                            CommandName = request.command.ToString(),
                            AdditionalInformation = request.AdditionalInformation,
                            DateCreate = DateTime.Now,
                            Parameter = request.parameter
                        };
                        trace.Result = ExecutorCommand.ejecutar(request.command, request.parameter);
                        trace.DateExecution = DateTime.Now;
                        status.Add(trace);
                        request = cmdBatch.GetNextCommand(status[status.Count - 1].Result);
                    }
                }
                QueueBatches.Clear();
            }

            return status;
        }

        public static GetwayDispensador GetInstance()
        {


            if (currentInstance == null)
            {
                currentInstance = new GetwayDispensador();
                QueueBatches = new Queue<BatchCommand>();
            }


            return currentInstance;
        }

    }
}