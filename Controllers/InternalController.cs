using System.Text;
using System.Text.Json;
using Kidz.Constants;
using Kidz.DatabaseConnection;
using Kidz.Models;
using Kidz.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kidz.Controllers
{
    [ApiController]
    public class InternalController : BaseController
    {


        string hostUrl = WebAddressConstant.STRING_SCHME_HTTPS + WebAddressConstant.STRING_SCHME_LOCALHOST + WebAddressConstant.STRING_PORT_BANGUNRUANG;

        public InternalController(DatabaseContex databaseContex) : base(databaseContex)
        {

        }

        [HttpPost]
        [Route("[controller]/validateUserToken")]
        public ResponseModel validateUserToken([FromBody] String stringRequest)
        {
            RequestModel _modelRequest = new RequestModel();
            ResponseModel _modelResponse = new ResponseModel();
            bool _boolException = false;
            string _stringRequestDecoded = "";

            try
            {
                _stringRequestDecoded = base64Decode(stringRequest);
            }
            catch (Exception exception)
            {
                _boolException = true;
                _modelResponse.MessageContent = exception.Message;
                _modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                _modelResponse.Data = exception.Message;
            }

            if(!_boolException)
            {
                 if(!(string.IsNullOrEmpty(_stringRequestDecoded)))
                {
                    _modelRequest = JsonSerializer.Deserialize<RequestModel>(_stringRequestDecoded);
                    UserModel modelUserData = JsonSerializer.Deserialize<UserModel>(_modelRequest.Data);
                    UserModel modelUser = _userQuery.selectUserByToken(modelUserData);

                    if( modelUser != null)
                    {
                        _modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                        _modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_SUCCESS;
                        _modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_SUCCESS;
                        _modelResponse.Data = JsonSerializer.Serialize(modelUser);
                    }
                    else
                    {
                        _modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_FAIL;
                        _modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_FAIL;
                        _modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                        _modelResponse.Data = JsonSerializer.Serialize(modelUser);
                    }
                }
                else
                {
                    _modelResponse.ServiceResponseCode = ServiceResponseCodeConstant.STRING_RESPONSECODE_MODULE_SUCCESS;
                    _modelResponse.MessageContent = StringConstant.STRING_MESSAGE_CONTENT_DATAEMPTY;
                    _modelResponse.MessageTitle = StringConstant.STRING_MESSAGE_TITLE_FAIL;
                }
            }

            return _modelResponse;
        }
    }
}