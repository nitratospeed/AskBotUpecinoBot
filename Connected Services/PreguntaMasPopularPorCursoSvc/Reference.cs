﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LuisBot.PreguntaMasPopularPorCursoSvc {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Pregunta", Namespace="http://schemas.datacontract.org/2004/07/AskBotUpecino.Dominio")]
    [System.SerializableAttribute()]
    public partial class Pregunta : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PreguntaTxtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idCursoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idPreguntaField;
        
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
        public string PreguntaTxt {
            get {
                return this.PreguntaTxtField;
            }
            set {
                if ((object.ReferenceEquals(this.PreguntaTxtField, value) != true)) {
                    this.PreguntaTxtField = value;
                    this.RaisePropertyChanged("PreguntaTxt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string idCurso {
            get {
                return this.idCursoField;
            }
            set {
                if ((object.ReferenceEquals(this.idCursoField, value) != true)) {
                    this.idCursoField = value;
                    this.RaisePropertyChanged("idCurso");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string idPregunta {
            get {
                return this.idPreguntaField;
            }
            set {
                if ((object.ReferenceEquals(this.idPreguntaField, value) != true)) {
                    this.idPreguntaField = value;
                    this.RaisePropertyChanged("idPregunta");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PreguntaException", Namespace="http://schemas.datacontract.org/2004/07/AskBotUpecino.Errores")]
    [System.SerializableAttribute()]
    public partial class PreguntaException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionField;
        
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
        public string Codigo {
            get {
                return this.CodigoField;
            }
            set {
                if ((object.ReferenceEquals(this.CodigoField, value) != true)) {
                    this.CodigoField = value;
                    this.RaisePropertyChanged("Codigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descripcion {
            get {
                return this.DescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionField, value) != true)) {
                    this.DescripcionField = value;
                    this.RaisePropertyChanged("Descripcion");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PreguntaMasPopularPorCursoSvc.IAskBotUpecinoService")]
    public interface IAskBotUpecinoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskBotUpecinoService/ObtenerPreguntaMasPopularPorCurso", ReplyAction="http://tempuri.org/IAskBotUpecinoService/ObtenerPreguntaMasPopularPorCursoRespons" +
            "e")]
        [System.ServiceModel.FaultContractAttribute(typeof(LuisBot.PreguntaMasPopularPorCursoSvc.PreguntaException), Action="http://tempuri.org/IAskBotUpecinoService/ObtenerPreguntaMasPopularPorCursoPregunt" +
            "aExceptionFault", Name="PreguntaException", Namespace="http://schemas.datacontract.org/2004/07/AskBotUpecino.Errores")]
        LuisBot.PreguntaMasPopularPorCursoSvc.Pregunta ObtenerPreguntaMasPopularPorCurso(string idCurso);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAskBotUpecinoService/ObtenerPreguntaMasPopularPorCurso", ReplyAction="http://tempuri.org/IAskBotUpecinoService/ObtenerPreguntaMasPopularPorCursoRespons" +
            "e")]
        System.Threading.Tasks.Task<LuisBot.PreguntaMasPopularPorCursoSvc.Pregunta> ObtenerPreguntaMasPopularPorCursoAsync(string idCurso);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAskBotUpecinoServiceChannel : LuisBot.PreguntaMasPopularPorCursoSvc.IAskBotUpecinoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AskBotUpecinoServiceClient : System.ServiceModel.ClientBase<LuisBot.PreguntaMasPopularPorCursoSvc.IAskBotUpecinoService>, LuisBot.PreguntaMasPopularPorCursoSvc.IAskBotUpecinoService {
        
        public AskBotUpecinoServiceClient() {
        }
        
        public AskBotUpecinoServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AskBotUpecinoServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AskBotUpecinoServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AskBotUpecinoServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public LuisBot.PreguntaMasPopularPorCursoSvc.Pregunta ObtenerPreguntaMasPopularPorCurso(string idCurso) {
            return base.Channel.ObtenerPreguntaMasPopularPorCurso(idCurso);
        }
        
        public System.Threading.Tasks.Task<LuisBot.PreguntaMasPopularPorCursoSvc.Pregunta> ObtenerPreguntaMasPopularPorCursoAsync(string idCurso) {
            return base.Channel.ObtenerPreguntaMasPopularPorCursoAsync(idCurso);
        }
    }
}
