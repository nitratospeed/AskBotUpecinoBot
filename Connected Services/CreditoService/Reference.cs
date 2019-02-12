﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuisBot.CreditoService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Credito", Namespace="http://schemas.datacontract.org/2004/07/SOAPServices.Dominio")]
    [System.SerializableAttribute()]
    public partial class Credito : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodAlumnoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodCreditoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodCursoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodDescripcionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime FechaIngField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TipoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodAlumno {
            get {
                return this.CodAlumnoField;
            }
            set {
                if ((object.ReferenceEquals(this.CodAlumnoField, value) != true)) {
                    this.CodAlumnoField = value;
                    this.RaisePropertyChanged("CodAlumno");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CodCredito {
            get {
                return this.CodCreditoField;
            }
            set {
                if ((this.CodCreditoField.Equals(value) != true)) {
                    this.CodCreditoField = value;
                    this.RaisePropertyChanged("CodCredito");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodCurso {
            get {
                return this.CodCursoField;
            }
            set {
                if ((object.ReferenceEquals(this.CodCursoField, value) != true)) {
                    this.CodCursoField = value;
                    this.RaisePropertyChanged("CodCurso");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CodDescripcion {
            get {
                return this.CodDescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.CodDescripcionField, value) != true)) {
                    this.CodDescripcionField = value;
                    this.RaisePropertyChanged("CodDescripcion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime FechaIng {
            get {
                return this.FechaIngField;
            }
            set {
                if ((this.FechaIngField.Equals(value) != true)) {
                    this.FechaIngField = value;
                    this.RaisePropertyChanged("FechaIng");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Tipo {
            get {
                return this.TipoField;
            }
            set {
                if ((object.ReferenceEquals(this.TipoField, value) != true)) {
                    this.TipoField = value;
                    this.RaisePropertyChanged("Tipo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CreditoService.ICreditoService")]
    public interface ICreditoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/CrearCredito", ReplyAction="http://tempuri.org/ICreditoService/CrearCreditoResponse")]
        LuisBot.CreditoService.Credito CrearCredito(LuisBot.CreditoService.Credito creditoACrear);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/CrearCredito", ReplyAction="http://tempuri.org/ICreditoService/CrearCreditoResponse")]
        System.Threading.Tasks.Task<LuisBot.CreditoService.Credito> CrearCreditoAsync(LuisBot.CreditoService.Credito creditoACrear);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/EliminarCredito", ReplyAction="http://tempuri.org/ICreditoService/EliminarCreditoResponse")]
        void EliminarCredito(int dni);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/EliminarCredito", ReplyAction="http://tempuri.org/ICreditoService/EliminarCreditoResponse")]
        System.Threading.Tasks.Task EliminarCreditoAsync(int dni);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/ListarCreditos", ReplyAction="http://tempuri.org/ICreditoService/ListarCreditosResponse")]
        LuisBot.CreditoService.Credito[] ListarCreditos();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreditoService/ListarCreditos", ReplyAction="http://tempuri.org/ICreditoService/ListarCreditosResponse")]
        System.Threading.Tasks.Task<LuisBot.CreditoService.Credito[]> ListarCreditosAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICreditoServiceChannel : LuisBot.CreditoService.ICreditoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditoServiceClient : System.ServiceModel.ClientBase<LuisBot.CreditoService.ICreditoService>, LuisBot.CreditoService.ICreditoService {
        
        public CreditoServiceClient() {
        }
        
        public CreditoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreditoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LuisBot.CreditoService.Credito CrearCredito(LuisBot.CreditoService.Credito creditoACrear) {
            return base.Channel.CrearCredito(creditoACrear);
        }
        
        public System.Threading.Tasks.Task<LuisBot.CreditoService.Credito> CrearCreditoAsync(LuisBot.CreditoService.Credito creditoACrear) {
            return base.Channel.CrearCreditoAsync(creditoACrear);
        }
        
        public void EliminarCredito(int dni) {
            base.Channel.EliminarCredito(dni);
        }
        
        public System.Threading.Tasks.Task EliminarCreditoAsync(int dni) {
            return base.Channel.EliminarCreditoAsync(dni);
        }
        
        public LuisBot.CreditoService.Credito[] ListarCreditos() {
            return base.Channel.ListarCreditos();
        }
        
        public System.Threading.Tasks.Task<LuisBot.CreditoService.Credito[]> ListarCreditosAsync() {
            return base.Channel.ListarCreditosAsync();
        }
    }
}
