﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1008
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace wwwroot.SMSWebServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://microsoft.com/webservices/", ConfigurationName="SMSWebServiceReference.ServiceSoap")]
    public interface ServiceSoap {
        
        // CODEGEN: 命名空间 http://microsoft.com/webservices/ 的元素名称 UserName 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/UserInfo", ReplyAction="*")]
        wwwroot.SMSWebServiceReference.UserInfoResponse UserInfo(wwwroot.SMSWebServiceReference.UserInfoRequest request);
        
        // CODEGEN: 命名空间 http://microsoft.com/webservices/ 的元素名称 username 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://microsoft.com/webservices/SendSMS", ReplyAction="*")]
        wwwroot.SMSWebServiceReference.SendSMSResponse SendSMS(wwwroot.SMSWebServiceReference.SendSMSRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UserInfoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UserInfo", Namespace="http://microsoft.com/webservices/", Order=0)]
        public wwwroot.SMSWebServiceReference.UserInfoRequestBody Body;
        
        public UserInfoRequest() {
        }
        
        public UserInfoRequest(wwwroot.SMSWebServiceReference.UserInfoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class UserInfoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string UserName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string UserPwd;
        
        public UserInfoRequestBody() {
        }
        
        public UserInfoRequestBody(string UserName, string UserPwd) {
            this.UserName = UserName;
            this.UserPwd = UserPwd;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class UserInfoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="UserInfoResponse", Namespace="http://microsoft.com/webservices/", Order=0)]
        public wwwroot.SMSWebServiceReference.UserInfoResponseBody Body;
        
        public UserInfoResponse() {
        }
        
        public UserInfoResponse(wwwroot.SMSWebServiceReference.UserInfoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class UserInfoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string UserInfoResult;
        
        public UserInfoResponseBody() {
        }
        
        public UserInfoResponseBody(string UserInfoResult) {
            this.UserInfoResult = UserInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendSMSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendSMS", Namespace="http://microsoft.com/webservices/", Order=0)]
        public wwwroot.SMSWebServiceReference.SendSMSRequestBody Body;
        
        public SendSMSRequest() {
        }
        
        public SendSMSRequest(wwwroot.SMSWebServiceReference.SendSMSRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class SendSMSRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string username;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userpwd;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string TelNumber;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string sms;
        
        public SendSMSRequestBody() {
        }
        
        public SendSMSRequestBody(string username, string userpwd, string TelNumber, string sms) {
            this.username = username;
            this.userpwd = userpwd;
            this.TelNumber = TelNumber;
            this.sms = sms;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendSMSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendSMSResponse", Namespace="http://microsoft.com/webservices/", Order=0)]
        public wwwroot.SMSWebServiceReference.SendSMSResponseBody Body;
        
        public SendSMSResponse() {
        }
        
        public SendSMSResponse(wwwroot.SMSWebServiceReference.SendSMSResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://microsoft.com/webservices/")]
    public partial class SendSMSResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SendSMSResult;
        
        public SendSMSResponseBody() {
        }
        
        public SendSMSResponseBody(string SendSMSResult) {
            this.SendSMSResult = SendSMSResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceSoapChannel : wwwroot.SMSWebServiceReference.ServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceSoapClient : System.ServiceModel.ClientBase<wwwroot.SMSWebServiceReference.ServiceSoap>, wwwroot.SMSWebServiceReference.ServiceSoap {
        
        public ServiceSoapClient() {
        }
        
        public ServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        wwwroot.SMSWebServiceReference.UserInfoResponse wwwroot.SMSWebServiceReference.ServiceSoap.UserInfo(wwwroot.SMSWebServiceReference.UserInfoRequest request) {
            return base.Channel.UserInfo(request);
        }
        
        public string UserInfo(string UserName, string UserPwd) {
            wwwroot.SMSWebServiceReference.UserInfoRequest inValue = new wwwroot.SMSWebServiceReference.UserInfoRequest();
            inValue.Body = new wwwroot.SMSWebServiceReference.UserInfoRequestBody();
            inValue.Body.UserName = UserName;
            inValue.Body.UserPwd = UserPwd;
            wwwroot.SMSWebServiceReference.UserInfoResponse retVal = ((wwwroot.SMSWebServiceReference.ServiceSoap)(this)).UserInfo(inValue);
            return retVal.Body.UserInfoResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        wwwroot.SMSWebServiceReference.SendSMSResponse wwwroot.SMSWebServiceReference.ServiceSoap.SendSMS(wwwroot.SMSWebServiceReference.SendSMSRequest request) {
            return base.Channel.SendSMS(request);
        }
        
        public string SendSMS(string username, string userpwd, string TelNumber, string sms) {
            wwwroot.SMSWebServiceReference.SendSMSRequest inValue = new wwwroot.SMSWebServiceReference.SendSMSRequest();
            inValue.Body = new wwwroot.SMSWebServiceReference.SendSMSRequestBody();
            inValue.Body.username = username;
            inValue.Body.userpwd = userpwd;
            inValue.Body.TelNumber = TelNumber;
            inValue.Body.sms = sms;
            wwwroot.SMSWebServiceReference.SendSMSResponse retVal = ((wwwroot.SMSWebServiceReference.ServiceSoap)(this)).SendSMS(inValue);
            return retVal.Body.SendSMSResult;
        }
    }
}
