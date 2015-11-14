﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3
{
    class Station
    {
        private ICollection<Port> _ports;
        private ICollection<Terminal> _terminals;
        private ICollection<CallInfo> _connectionCollection;
        private ICollection<CallInfo> _callCollection;
        private IDictionary<Terminal, Port> _portMap; 
        
        public Station(ICollection<Port> ports, ICollection<Terminal> terminals)
        {
            this._ports = ports;
            this._terminals = terminals;
            this._connectionCollection = new List<CallInfo>();
            this._callCollection = new List<CallInfo>();
            this._portMap = new Dictionary<Terminal, Port>();
        }

        public void MapPort(Terminal terminal, Port port)
        {
            if (port == null)
                throw new ArgumentNullException(nameof(port) + " is null");
            if (terminal == null)
                throw new ArgumentNullException(nameof(terminal) + " is null");
            if (this._portMap.Values.Contains(port))
                throw new Exception("This port is already use");
            
            this._portMap.Add(terminal, port);
          
        }
        public void UnmapPort(Port port)
        {
            if (port == null) return;
            if (_portMap.Values.Contains(port))
            {
                var terminal = _portMap.FirstOrDefault(pair => pair.Value == port).Key;
                if (terminal != null)
                    _portMap.Remove(terminal);
            }
        }


        public Port GetPort(Terminal terminal)
        {
            Port port;
            _portMap.TryGetValue(terminal, out port);
            return port;
        }
        public Terminal GetTerminal(PhoneNumber phoneNumber)
        {
            return this._terminals.FirstOrDefault(terminal => terminal.PhoneNumber == phoneNumber);
        }

        public CallInfo GetCallInfo(PhoneNumber source)
        {
            return _callCollection.LastOrDefault(info => (info.Source == source || info.Target == source));
        }
        public CallInfo GetConnectionInfo(PhoneNumber source)
        {
            return _connectionCollection.FirstOrDefault(info => (info.Source == source || info.Target == source));
        }

        public void Add(Port port)
        {
            if (!this._ports.Contains(port))
                this._ports.Add(port);
        }
        public void Add(Terminal terminal)
        {
            var freePort = this._ports.Except(_portMap.Values).FirstOrDefault();
            if (freePort == null) return;

            if (this._terminals.Any(term => term.PhoneNumber == terminal.PhoneNumber))
                throw new Exception("this number alredy used");

            if (!this._terminals.Contains(terminal))
                _terminals.Add(terminal);

            MapPort(terminal, freePort);
            freePort.RegisterEventsForTerminal(terminal);
            terminal.RegisterEventForPort(freePort);
            this.RegisterEventForTerminal(terminal);
            this.RegisterEventForPort(freePort);
            freePort.State = PortState.Free;
        }

        protected void RegisterOutgoingRequest(object sender, Request request)
        {
            Console.WriteLine("Terminal connect to station");
            if (request.Source != default(PhoneNumber) && request.Target != default(PhoneNumber))
            {
                var sourceTerminal = GetTerminal(request.Source);
                var sourcePort = GetPort(sourceTerminal);

                var targetTerminal = GetTerminal(request.Target);
                var targetPort = GetPort(targetTerminal);

               
                if (sourcePort.State == PortState.Free && targetPort.State == PortState.Free)
                {
                    sourcePort.State = PortState.Busy;
                    targetPort.State = PortState.Busy;

                    var callInfo = new CallInfo()
                    {
                        Source = request.Source,
                        Target = request.Target,
                        Started = DateTime.Now,
                        Duration = TimeSpan.Zero
                    };
                    _connectionCollection.Add(callInfo);

                    var incomingRequest = new Request(request.Source, request.Target, Request.IncomingCall);
                    targetTerminal.IncomingRequest(incomingRequest);
                }
                else
                {
                    //user busy
                }
            }

        }
        
        public void OnIncomingCallRespond(object sender, Respond respond)
        {
            Console.WriteLine("Respond ok");
            Console.WriteLine(respond);
            switch (respond.Code)
            {
                case Respond.Accept:
                    Console.WriteLine("Accept");
                    MakeCallActive(null);
                    break;
                case Respond.Drop:
                    Console.WriteLine("Drop");
                    InterruptConnection(null);
                    break;
                default:
                    break;
            }
            //var registeredCallInfo = GetConnectionInfo(respond.Source);
            //if (registeredCallInfo != null)
            //{
            //    switch (respond.State)
            //    {
            //        case Responds.RespondState.Drop:
            //            {
            //                InterruptConnection(registeredCallInfo);
            //                break;
            //            }
            //        case Responds.RespondState.Accept:
            //            {
            //                MakeCallActive(registeredCallInfo);
            //                break;
            //            }
            //    }
            //}
            //else
            //{
            //    CallInfo currentCallInfo = GetCallInfo(respond.Source);
            //    if (currentCallInfo != null)
            //    {
            //        this.InterruptActiveCall(currentCallInfo);
            //    }
            //}
        }

        protected void InterruptConnection(CallInfo connection)
        {
            //this._callCollection.Remove(callInfo);
            SetPortStateWhenConnectionInterrupted(connection.Source, connection.Target);
            //OnCallInfoPrepared(this, callInfo);
        }

        protected void InterruptActiveCall(CallInfo connection)
        {
            connection.Duration = DateTime.Now - connection.Started;
            //this._callCollection.Remove(callInfo);
            SetPortStateWhenConnectionInterrupted(connection.Source, connection.Target);
            //OnCallInfoPrepared(this, callInfo);
        }
        protected void MakeCallActive(CallInfo callInfo)
        {
            // this._connectionCollection.Remove(callInfo);
            callInfo.Started = DateTime.Now;
            // this._callCollection.Add(callInfo);
        }
        protected void SetPortStateWhenConnectionInterrupted(PhoneNumber source, PhoneNumber target)
        {
            var sourcePort = GetPort(GetTerminal(source));
            if (sourcePort != null)
            {
                sourcePort.State = PortState.Free;
            }

            var targetPort = GetPort(GetTerminal(target));
            if (targetPort != null)
            {
                targetPort.State = PortState.Free;
            }
        }




        protected virtual void RegisterEventForTerminal(Terminal terminal)
        {
            terminal.OutConnection += RegisterOutgoingRequest;

            
            terminal.IncomRespond += OnIncomingCallRespond;


        }
        protected virtual void RegisterEventForPort(Port port)
        {
            port.StateChanged += (sender, state) =>
            {
                Console.WriteLine("station: port changed state to " + port.State);
            };
        }

    }
}